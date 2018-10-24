using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using Pulse.Base;

namespace Pulse
{
    public class Settings : XmlSerializable<Settings>
    {
        public Settings()
        {
            Provider = "Wallbase";
            Language = CultureInfo.CurrentUICulture.Name;
            DownloadAutomatically = true;
            RefreshInterval = 20;
            ClearOldPics = false;
            ClearInterval = 3;
            PreFetch = false;
            MaxPictureDownloadCount = 100;
            CachePath = Path.Combine(App.Path, "Cache");
            ProviderSettings = new SerializableDictionary<string, ActiveProviderInfo>();
            DownloadOnAppStartup = false;

            //set wallpaper changer as a default provider for output
            ProviderSettings.Add("Desktop Wallpaper", new ActiveProviderInfo()
            {
                Active = true,
                AsyncOK = false,
                ExecutionOrder = 1,
                ProviderConfig = string.Empty,
                ProviderName = "Desktop Wallpaper"
            });

            switch (DateTime.Now.Month)
            {
                case 12:
                case 1:
                case 2:
                    Search = Properties.Resources.Winter;
                    break;
                case 3:
                case 4:
                case 5:
                    Search = Properties.Resources.Spring;
                    break;
                case 6:
                case 7:
                case 8:
                    Search = Properties.Resources.Summer;
                    break;
                case 9:
                case 10:
                case 11:
                    Search = Properties.Resources.Autumn;
                    break;
            }
        }

        public string Provider { get; set; }

        public string Search { get; set; }
        public string Filter { get; set; }
        public List<string> BannedImages { get; set; }
        
        public bool DownloadAutomatically { get; set; }
        public bool DownloadOnAppStartup { get; set; }
        public double RefreshInterval { get; set; }
        public bool PreFetch { get; set; }
        public int MaxPictureDownloadCount { get; set; }
        public string CachePath { get; set; }


        public string Language { get; set; }
        
        public int ClearInterval { get; set; }
        public bool ClearOldPics { get; set; }

        //provider settings
        public SerializableDictionary<string, ActiveProviderInfo> ProviderSettings { get; set; }


        //read-only split filtered words
        public List<string> FilteredWords
        {
            get {
                var result = new List<string>();
                if(string.IsNullOrEmpty(Filter)) return result;

                var s = Filter.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                if (s.Length == 0)
                    return result;

                result.AddRange(s);

                return result;
            }
        }
    }
}
