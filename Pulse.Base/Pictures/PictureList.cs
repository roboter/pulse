using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pulse.Base
{
    public class PictureList : XmlSerializable<PictureList>
    {
        public string ProviderName { get; set; }
        public DateTime FetchDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int SearchSettingsHash { get; set; }

        public List<Picture> Pictures { get; set; }

        public PictureList()
        {
            Pictures = new List<Picture>();
            //default to 1 day expiration
            ExpirationDate = DateTime.Now.AddDays(1);
        }
    }
}
