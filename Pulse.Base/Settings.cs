using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Pulse.Base
{
    public class Settings : XmlSerializable<Settings>
    {
        public enum IntervalUnits
        {
            Seconds, 
            Minutes,
            Hours,
            Days
        }

        public static readonly string AppPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static Settings CurrentSettings 
        {
            get
            {
                //load settings from file
                if(_current==null)
                    _current = Settings.LoadFromFile(Path.Combine(AppPath,"settings.conf")) ?? new Settings();

                return _current;
            }
            set {
                _current = value;
            }
        }

        private static Settings _current = null;

        public List<string> BannedImages { get; set; }
        
        public bool ChangeOnTimer { get; set; }
        public bool DownloadOnAppStartup { get; set; }

        public int RefreshInterval { get; set; }
        public IntervalUnits IntervalUnit { get; set; }

        public bool PreFetch { get; set; }
        public int MaxPictureDownloadCount { get; set; }
        public int MaxPreviousPictureDepth { get; set; }
        public string CachePath { get; set; }
        public bool CheckForNewPulseVersions { get; set; }
        public bool SkipChangeIfFullScreen { get; set; }

        public string Language { get; set; }
        
        public int ClearInterval { get; set; }
        public bool ClearOldPics { get; set; }

        public bool RunOnWindowsStartup { get; set; }

        //provider settings
        public SerializableDictionary<Guid, ActiveProviderInfo> ProviderSettings { get; set; }

        public Settings()
        {
            Language = CultureInfo.CurrentUICulture.Name;
            ChangeOnTimer = true;
            RefreshInterval = 20;
            IntervalUnit = IntervalUnits.Minutes;

            ClearOldPics = false;
            ClearInterval = 3;
            PreFetch = false;
            MaxPictureDownloadCount = 100;
            MaxPreviousPictureDepth = 5;
            CheckForNewPulseVersions = true;
            CachePath = System.IO.Path.Combine(AppPath, "Cache");
            ProviderSettings = new SerializableDictionary<Guid, ActiveProviderInfo>();
            DownloadOnAppStartup = false;
            RunOnWindowsStartup = false;
            SkipChangeIfFullScreen = false;

            BannedImages = new List<string>();

            //set wallpaper changer as a default provider for output
            ActiveProviderInfo apiWallpaper = new ActiveProviderInfo("Desktop Wallpaper");
            apiWallpaper.Active = true;
            apiWallpaper.ExecutionOrder = 1;

            ProviderSettings.Add(apiWallpaper.ProviderInstanceID, apiWallpaper);

            //set wallbase as default for inputs
            ActiveProviderInfo apiWallbase = new ActiveProviderInfo("Wallbase");

            ProviderSettings.Add(apiWallbase.ProviderInstanceID, apiWallbase);
            apiWallbase.Active=true;
            apiWallbase.ExecutionOrder =1;
        }

        public string GetProviderSettings(Guid prov) {
            if (!ProviderSettings.ContainsKey(prov)) return string.Empty;

            return ProviderSettings[prov].ProviderConfig;
        }
    }
}
