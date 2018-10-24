using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using Pulse.Base;
using Pulse.Base.Providers;

namespace NASAAPOD
{
    [System.ComponentModel.Description("NASA Astronomy Picture of the Day")]
    [ProviderIcon(typeof(Properties.Resources),"nasa")]
    public class NASAAPODProviderza:Pulse.Base.IInputProvider
    {
        private static string _url = "http://apod.nasa.gov/apod/archivepix.html";

        public Pulse.Base.PictureList GetPictures(Pulse.Base.PictureSearch ps)
        {
            WebClient wc = new WebClient();

            //download archive webpage
            var pg = wc.DownloadString(_url);

            //regex out the links to the individual pages
            Regex reg = new Regex("<a href=\"(?<picPage>ap.*\\.html)\">");
            Regex regPic = new Regex("<IMG SRC=\"(?<picURL>image.*)\"");
            var matches = reg.Matches(pg);

            var pl = new Pulse.Base.PictureList() { FetchDate = DateTime.Now };

            //if max picture count is 0, then no maximum, else specified max
            var maxPictureCount = ps.MaxPictureCount > 0 ? (ps.MaxPictureCount + ps.BannedURLs.Where(u => u.StartsWith("http://apod.nasa.gov/apod/")).Count()) : int.MaxValue;
            maxPictureCount = Math.Min(matches.Count, maxPictureCount);

            //counts might be a bit off in the event of bannings, but hopefully it won't be too far off.
            var matchesToGet = (from Match c in matches select c)
                .OrderBy(x => Guid.NewGuid())
                .Take(maxPictureCount);

            //build url's, skip banned items, randomly sort the items and only bring back the desired number
            // all in one go
            pl.Pictures.AddRange((from Match c in matchesToGet
                                 let photoPage = new WebClient().DownloadString("http://apod.nasa.gov/apod/" + c.Groups["picPage"].Value)
                                 let photoURL = "http://apod.nasa.gov/apod/" + regPic.Match(photoPage).Groups["picURL"].Value
                                 where !ps.BannedURLs.Contains(photoURL)
                                 select new Picture() {Url = photoURL, Id=System.IO.Path.GetFileNameWithoutExtension(photoURL)}));

            return pl;
        }

        public void Activate(object args)
        {
            
        }

        public void Deactivate(object args)
        {
            
        }

        public void Initialize(object args)
        {
            
        }
    }
}
