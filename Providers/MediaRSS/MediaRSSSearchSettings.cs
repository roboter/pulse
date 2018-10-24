using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows;
using Pulse.Base;

namespace MediaRSSProvider
{
    public class MediaRSSImageSearchSettings : Pulse.Base.XmlSerializable<MediaRSSImageSearchSettings>
    {

        public string MediaRSSURL { get; set; }

        public MediaRSSImageSearchSettings(){
            //sample Media RSS URL
            this.MediaRSSURL = string.Format("http://backend.deviantart.com/rss.xml?q=boost%3Apopular+in%3Acustomization%2Fwallpaper+{0}x{1}&type=deviation",
                Pulse.Base.PictureManager.PrimaryScreenResolution.First.ToString(), Pulse.Base.PictureManager.PrimaryScreenResolution.Second.ToString());
        }
    }
}
