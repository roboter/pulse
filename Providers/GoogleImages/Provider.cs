using System;
using System.Linq;
using Pulse.Base;
using Pulse.Base.Providers;
using System.Text.RegularExpressions;
using System.Net;

namespace GoogleImages
{
    //[ProviderConfigurationUserControl(typeof(GoogleImageProviderPreferences))]
    [ProviderConfigurationClass(typeof(GoogleImageSearchSettings))]
    [System.ComponentModel.Description("Google Images")]
    [ProviderIcon(typeof(Properties.Resources),"googleImages")]
    public class Provider : IInputProvider
    {
        private readonly Regex _imagesRegex2 = new Regex(@"imgurl=(?<imgurlgrp>http.*?)&amp;.*?imgrefurl=(?<imgrefgrp>http.*?)&amp;.*?src=[""'](?<thumbURL>.*?)[""'].*?>",RegexOptions.Singleline);//"(imgurl=)(?<imgurl>http.*?)[^&>]*([>&]{1})");
        private const string baseURL = "http://images.google.com/search?tbm=isch&hl=en&source=hp&biw=&bih=&gbv=1&q={0}{1}&start={2}";
        private readonly CookieContainer _cookies = new CookieContainer();

        public Provider()
        {
        }

        public void Initialize(object args)
        {
            //nothing to do here
        }

        public void Activate(object args) { }
        public void Deactivate(object args) { }


        public PictureList GetPictures(PictureSearch ps)
        {
            var result = new PictureList() { FetchDate = DateTime.Now };

            //load provider search settings
            GoogleImageSearchSettings giss = GoogleImageSearchSettings.LoadFromXML(ps.SearchProvider.ProviderConfig) ??
                                             new GoogleImageSearchSettings();

            //if search is empty, return now since we can't search without it
            if (string.IsNullOrEmpty(giss.Query)) return result;

            var pageIndex = ps.PageToRetrieve; //set page to retrieve if one specified
            var imgFoundCount = 0;
            
            //if max picture count is 0, then no maximum, else specified max
            var maxPictureCount = ps.MaxPictureCount > 0?ps.MaxPictureCount : int.MaxValue;

            //build tbs strring
            var tbs = "";//isz:ex,iszw:{1},iszh:{2}

            //handle sizeing
            if (giss.ImageHeight > 0 && giss.ImageWidth > 0)
            {
                tbs += string.Format("isz:ex,iszw:{0},iszh:{1},", giss.ImageWidth, giss.ImageHeight);
            }

            //handle colors
            if (!string.IsNullOrEmpty(giss.Color))
            {
                tbs += GoogleImageSearchSettings.GoogleImageColors.GetColorSearchString((from c in GoogleImageSearchSettings.GoogleImageColors.GetColors() where c.Value == giss.Color select c).Single()) + ",";
            }

            //if we have a filter string then add it and trim off trailing commas
            if (!string.IsNullOrEmpty(tbs)) tbs = ("&tbs=" + tbs).Trim(new char[]{','});

            //do safe search setup (off/strict/moderate) this is part of the session and tracked via cookies
            //SetSafeSearchSetting(giss.GoogleSafeSearchOption);

            do
            {
                //build URL from query, dimensions and page index
                var url = string.Format(baseURL, giss.Query, tbs, (pageIndex * 20));

                var response = string.Empty;
                using (var client = new HttpUtility.CookieAwareWebClient(_cookies))
                {
                    response = client.DownloadString(url);
                }

                var images = _imagesRegex2.Matches(response);

                //track number of images found for paging purposes
                imgFoundCount = images.Count;

                //convert images found into picture entries
                foreach (Match item in images)
                {
                    var purl = item.Groups["imgurlgrp"].Value;
                    var referrer = item.Groups["imgrefgrp"].Value;
                    var thumbnail = item.Groups["thumbURL"].Value;
                    //get id and trim if necessary (ran into a few cases of rediculously long filenames)
                    var id = System.IO.Path.GetFileNameWithoutExtension(purl);
                    if (id.Length > 50) id = id.Substring(0, 50);
                    //because google images come from so many sites it's not uncommon to have duplicate file names. (we fix this)
                    id = string.Format("{0}_{1}", id, purl.GetHashCode());

                    var p = new Picture() { Url = purl, Id = id };
                    p.Properties.Add(Picture.StandardProperties.Thumbnail, thumbnail);
                    p.Properties.Add(Picture.StandardProperties.Referrer, referrer);

                    result.Pictures.Add(p);
                }

                //if we have an image ban list check for them
                // doing this in the provider instead of picture manager
                // ensures that our count does not go down if we have a max
                if (ps.BannedURLs != null && ps.BannedURLs.Count > 0)
                {
                    result.Pictures = (from c in result.Pictures where !(ps.BannedURLs.Contains(c.Url)) select c).ToList();
                }

                //increment page index so we can get the next 20 images if they exist
                pageIndex++;
                // Max Picture count is defined in search settings passed in, check for it here too
            } while (imgFoundCount > 0 && result.Pictures.Count < maxPictureCount && ps.PageToRetrieve == 0);

            result.Pictures = result.Pictures.Take(maxPictureCount).ToList();

            return result;
        }

        private void SetSafeSearchSetting(GoogleImageSearchSettings.GoogleSafeSearchOptions gsso)
        {
            using (var client = new HttpUtility.CookieAwareWebClient(_cookies))
            {
                //First we need to access the preferences page so we can get the special ID
                var response = client.DownloadString("http://images.google.com/preferences?hl=en");
                //parse out signature
                var specialID = Regex.Match(response, "<input type=\"hidden\" name=\"sig\" value=\"(?<sig>.*?)\">").Groups["sig"].Value;

                //options are "on", "images", "off"
                var safeUIOption = "";
                switch(gsso) {
                    case GoogleImageSearchSettings.GoogleSafeSearchOptions.Off:
                        safeUIOption = "off";
                        break;
                    case GoogleImageSearchSettings.GoogleSafeSearchOptions.On:
                        safeUIOption = "on";
                        break;
                }
                //set prefs
                string url = string.Format("http://images.google.com/setprefs?sig={0}&hl=en&lr=lang_en&uulo=1&muul=4_20&luul=&safeui={1}&suggon=1&newwindow=0&q=",
                                specialID.Replace("=", "%3D"), safeUIOption);

                var finalResponse = client.DownloadString(url);
            }
        }
    }
}
