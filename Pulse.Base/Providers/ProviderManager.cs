using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Pulse.Base.Providers;

namespace Pulse.Base
{
    public class ProviderManager
    {
        public static ProviderManager Instance
        {
            get
            {
                if (_Instance == null) _Instance = new ProviderManager();

                return _Instance;
            }
        }

        private static ProviderManager _Instance;

        public Dictionary<string, Type> Providers
        {
            get
            {
                if (_Providers == null) { _Providers = FindProviders(); }
                return _Providers;
            }
        }

        private Dictionary<string, Type> _Providers;

        private ProviderManager()
        {

        }

        public Dictionary<string, Type> GetProvidersByType<T>() where T : IProvider
        {
            return Providers.Where(type => typeof(T).IsAssignableFrom(type.Value)).ToDictionary(type => type.Key, type => type.Value);
        }

        private Dictionary<string, Type> FindProviders()
        {
            var result = new Dictionary<string, Type>();
            var workingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var providersDirectory = Path.Combine(workingDirectory, "Providers");

            //if no providers directory, return
            if (!Directory.Exists(providersDirectory)) return result;

            //get dll's in provider directory, if none found return empty results
            var files = from x in Directory.GetFiles(providersDirectory)
                        where x.EndsWith(".dll") || x.EndsWith(".exe")
                        select x;

            if (files.Count() == 0) return result;

            //for each providers dll look through the classes for ones that implement the passed in type
            foreach (var f in files)
            {
                var assembly = Assembly.LoadFrom(f);

                var providerType =
                    assembly.GetTypes().Where(type => typeof(IProvider).IsAssignableFrom(type));

                foreach (Type ipType in providerType)
                {
                    //look for a description attribute on the class to use as the name
                    var strName = GetProviderName(ipType);
                    var attrPlatform = GetSupportedPlatformsForType(ipType); // ipType.GetCustomAttributes(typeof(Pulse.Base.ProviderPlatformAttribute), true);
                    
                    //if this provider has a list of supported platforms
                    if (attrPlatform.Any())
                    {
                        var ppa = from ProviderPlatformAttribute ppaI in attrPlatform 
                                  where ppaI.Platform == Environment.OSVersion.Platform
                                        && (ppaI.MajorVersion == 0 || ppaI.MajorVersion == Environment.OSVersion.Version.Major)
                                        && (ppaI.MinorVersion == 0 || ppaI.MinorVersion == Environment.OSVersion.Version.Minor)
                                  select ppaI;

                        // BUT none of the platforms are supported by this computer, then skip this provider
                        if (!ppa.Any())
                        {
                            continue;
                        }
                    }

                    //if the provider collection doesn't already contain this provider, then add it.
                    // if it is already contained then make sure to Log the fact that a duplicatly named provider exists.
                    if (!result.ContainsKey(strName))
                    {
                        result.Add(strName, ipType);
                    }
                    else
                    {
                        Log.Logger.Write(string.Format("Provider with a duplicate name found while loading providers. Named: '{0}', Type: {1}, in file '{2}'.",
                            strName, ipType.FullName, f), Log.LoggerLevels.Errors);
                    }
                }
            }

            return result;
        }

        public static string GetProviderName(Type ipType)
        {
            string strName = "";

            var attrDescription = ipType.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (attrDescription.Length >= 1)
            {
                strName = (attrDescription[0] as System.ComponentModel.DescriptionAttribute).Description;
            }
            else
            {
                strName = ipType.FullName;
            }

            return strName;
        }

        public static List<Pulse.Base.ProviderPlatformAttribute> GetSupportedPlatformsForType(Type ipType)
        {
            var attrPlatform = ipType.GetCustomAttributes(typeof(Pulse.Base.ProviderPlatformAttribute), true);

            return (from Pulse.Base.ProviderPlatformAttribute c in attrPlatform select c).ToList();
        }

        public static bool GetAsyncStatusForType(Type ipType)
        {
            var attrPlatform = ipType.GetCustomAttributes(typeof(Pulse.Base.Providers.ProviderRunsAsyncAttribute), true);

            return (from Pulse.Base.Providers.ProviderRunsAsyncAttribute c in attrPlatform select c.AsyncOK).FirstOrDefault();
        }

        public Image GetProviderIcon(string provName)
        {
            return GetProviderIcon(Providers[provName]);
        }

        public static Image GetProviderIcon(Type ipType)
        {
            var attrImage = ipType.GetCustomAttributes(typeof(Pulse.Base.Providers.ProviderIconAttribute), true);

            if (attrImage.Length == 0) return null;

            return ((Pulse.Base.Providers.ProviderIconAttribute)attrImage[0]).ProviderIcon;
        }

        public IProvider InitializeProvider(string name, object initArgs)
        {
            return InitializeProvider(name, initArgs, null);
        }

        public IProvider InitializeProvider(string name, object initArgs, params object[] activationArgs)
        {
            if (!Providers.ContainsKey(name)) return null;

            var ipType = Providers[name];

            if (ipType == null) return null;

            return InitializeProvider(ipType, initArgs, activationArgs);
        }

        public static IProvider InitializeProvider(Type ipType, object initArgs)
        {
            return InitializeProvider(ipType, initArgs, null);
        }

        public static IProvider InitializeProvider(Type ipType, object initArgs, params object[] activationArgs)
        {
            if (ipType == null) return null;

            var aProv = Activator.CreateInstance(ipType) as IProvider;
            if (activationArgs != null) aProv.Activate(activationArgs);

            aProv.Initialize(initArgs);

            return aProv;
        }

        public IProviderConfigurationEditor InitializeConfigurationWindow(string name)
        {
            Type ipType = HasConfigurationWindow(name);

            var aProv = Activator.CreateInstance(ipType) as IProviderConfigurationEditor;

            return aProv;
        }

        public static IProviderConfigurationEditor InitializeConfigurationWindow(Type ipType)
        {
            var aProv = Activator.CreateInstance(ipType) as IProviderConfigurationEditor;

            return aProv;
        }

        public Type HasConfigurationWindow(string name)
        {
            if (!Providers.ContainsKey(name)) return null;

            var ipType = Providers[name];

            return HasConfigurationWindow(ipType);
        }

        public static Type HasConfigurationWindow(Type ipType)
        {
            //Find any instances of the user control definition attribute on the class
            var attrConfig = ipType.GetCustomAttributes(typeof(ProviderConfigurationAttribute), false);

            //if none found, return null
            if (attrConfig.Length == 0) return null;

            //for storing editor type
            Type tConfit = null;

            //check if we have a ProviderConfigurationUserControlAttribute, or just a configuration class defined
            var definedEditor = (from ProviderConfigurationAttribute c in attrConfig
                                 where c.GetType() == typeof(ProviderConfigurationUserControlAttribute)
                                 select c);

            if (definedEditor.Any())
            {
                ProviderConfigurationAttribute pcattrType = definedEditor.Single();
                tConfit = pcattrType.Type;
            }
            else
            {
                ProviderConfigurationAttribute pcattrType = attrConfig[0] as ProviderConfigurationAttribute;
                tConfit = pcattrType.Type;

                if (pcattrType.GetType() == typeof(ProviderConfigurationClassAttribute))
                {
                    tConfit = typeof(ProviderConfigurationPropertyGrid<>).MakeGenericType(tConfit);
                }
            }

            return tConfit;
        }

        public object InitializeNewProviderSettings(string pname)
        {
            if (!Providers.ContainsKey(pname)) return null;

            return InitializeNewProviderSettings(Providers[pname]);
        }

        public static object InitializeNewProviderSettings(Type ipType)
        {
            Type t = GetProviderSettingsClass(ipType);

            if (t == null) return null;

            var provSettings = Activator.CreateInstance(t);

            return provSettings;
        }

        public Type GetProviderSettingsClass(string pname)
        {
            if (!Providers.ContainsKey(pname)) return null;

            return GetProviderSettingsClass(Providers[pname]);
        }

        public static Type GetProviderSettingsClass(Type ipType)
        {
            var attrConfig = ipType.GetCustomAttributes(typeof(ProviderConfigurationClassAttribute), false);

            //if none found, return null
            if (attrConfig.Length == 0) return null;

            //return type
            return ((ProviderConfigurationClassAttribute)attrConfig[0]).Type;
        }
    }
}
