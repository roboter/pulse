using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Pulse.Base
{
    public abstract class XmlSerializable<T>
        where T : class, new()
    {
        public virtual void Save(string path)
        {
            try
            {
                using (FileStream f = File.Create(path))
                {
                    Save(f);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Write(string.Format("Error saving object of type '{0}' to file '{1}'. Error Details: {2}", typeof(T).ToString(), path, ex.ToString()), Log.LoggerLevels.Errors);
            }
        }

        public virtual string Save()
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                Save(sw);
            }

            return sb.ToString();
        }

        public virtual void Save(Stream stream)
        {
            using (var w = new StreamWriter(stream))
            {
                Save(w);
            }
        }

        private void Save(TextWriter w)
        {
            var s = new XmlSerializer(typeof(T));
            s.Serialize(w, this);
        }

        public static string Save(T obj)
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                Save(obj, sw);
            }

            return sb.ToString();
        }

        public static void Save(T obj, Stream stream)
        {
            using (var w = new StreamWriter(stream))
            {
                Save(obj, w);
            }
        }

        private static void Save(T obj, TextWriter w)
        {
            var s = new XmlSerializer(typeof(T));
            s.Serialize(w, obj);
        }

        public static T LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    return Load(sr);
                }
            } catch(Exception ex) {
                Log.Logger.Write(string.Format("Error loading object from file '{0}'. Exception details: {1}", path, ex.ToString()), Log.LoggerLevels.Errors);

                return null;
            }
        }

        public static T LoadFromXML(string sb)
        {
            if (string.IsNullOrEmpty(sb)) return null;

            XmlTextReader xtr = new XmlTextReader(new StringReader(sb));

            return Load(xtr);
        }

        public static T Load(StreamReader sr)
        {
            using (var xr = new XmlTextReader(sr))
            {                
                return Load(xr);
            }
        }

        public static T Load(XmlTextReader xr)
        {
            var xs = new XmlSerializer(typeof(T));
            T result = null;

            try
            {
                result = (T)xs.Deserialize(xr);
            }
            catch (Exception ex)
            {
                Log.Logger.Write(string.Format("Error loading object from XML. Exception details: {0}", ex.ToString()), Log.LoggerLevels.Errors);

            }

            return result;
        }
    }
}
