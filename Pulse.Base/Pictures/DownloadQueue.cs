using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Pulse.Base.Pictures
{
    public class DownloadQueue : KeyedCollection<string, PictureDownload>
    {
        protected override string GetKeyForItem(PictureDownload item)
        {
            return item.Picture.Url;
        }
    }
}
