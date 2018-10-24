using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pulse.Base
{
    public class PictureSearch : XmlSerializable<PictureSearch>
    {
        //provider to use for search
        [System.Xml.Serialization.XmlIgnore()]
        public ActiveProviderInfo SearchProvider { get; set; }

        //banned images
        public List<string> BannedURLs { get; set; }
        //Maximum number of pictures to download from provider.  0 means all
        public int MaxPictureCount { get; set; }
        //pull only a specific page from the results
        public int PageToRetrieve { get; set; }
        //Where to save the pictures
        public string SaveFolder { get; set; }
        //specifies whether we are just previewing or not
        public bool PreviewOnly { get; set; }

        public PictureSearch()
        {
            BannedURLs = new List<string>();
            PageToRetrieve = 0;
        }

        public int GetSearchHash()
        {
            return (Save() + SearchProvider.ProviderInstanceID.ToString() + SearchProvider.ProviderConfig).GetHashCode();
        }
    }
}
