using System.Collections.ObjectModel;

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
