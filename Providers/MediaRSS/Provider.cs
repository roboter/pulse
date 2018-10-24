using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pulse.Base;
using Pulse.Base.Providers;
using System.Text.RegularExpressions;
using System.Windows;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;

namespace MediaRSSProvider
{
    [ProviderConfigurationClass(typeof(MediaRSSImageSearchSettings))]
    [System.ComponentModel.Description("MediaRSS Feed")]
    [ProviderIcon(typeof(Properties.Resources),"rss")]
    public class Provider : IInputProvider
    {        
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
            MediaRSSImageSearchSettings mrssiss = string.IsNullOrEmpty(ps.SearchProvider.ProviderConfig) ?
                new MediaRSSImageSearchSettings() : MediaRSSImageSearchSettings.LoadFromXML(ps.SearchProvider.ProviderConfig);

            XDocument feedXML = XDocument.Load(mrssiss.MediaRSSURL);
            XNamespace media = XNamespace.Get("http://search.yahoo.com/mrss/");

            var feeds = from feed in feedXML.Descendants("item")
                        let content = feed.Elements(media + "content")
                        let thumb = feed.Element(media + "thumbnail").Attribute("url").Value
                        let img = content.Where(x => x.Attribute("medium").Value == "image").SingleOrDefault()
                        let url = img!=null?img.Attribute("url").Value:thumb
                        let id = System.IO.Path.GetFileNameWithoutExtension(url)
                        select new Picture()
                        {
                            Url = url,
                            Id = id.Length > 50 ? id.Substring(0, 50) : id,
                            Properties = new SerializableDictionary<string,string>(Picture.StandardProperties.Thumbnail,thumb)
                        };

            //get up to the maximum number of pictures, excluding banned images
            result.Pictures.AddRange(
                    feeds.Where(x=> !ps.BannedURLs.Contains(x.Url))
                    .Take(ps.MaxPictureCount));

            ////handle colors
            //if (!string.IsNullOrEmpty(giss.Color))
            //{
            //    tbs += MediaRSSImageSearchSettings.MediaRSSImageColors.GetColorSearchString((from c in MediaRSSImageSearchSettings.MediaRSSImageColors.GetColors() where c.Value == giss.Color select c).Single()) + ",";
            //}

            ////if we have a filter string then add it and trim off trailing commas
            //if (!string.IsNullOrEmpty(tbs)) tbs = ("&tbs=" + tbs).Trim(new char[]{','});

            //do
            //{
            //    //build URL from query, dimensions and page index
            //    var url = string.Format(baseURL, ps.SearchString, tbs, (pageIndex * 20).ToString());

            //    var response = client.DownloadString(url);

            //    var images = imagesRegex2.Matches(response);

            //    //track number of images found for paging purposes
            //    imgFoundCount = images.Count;

            //    //convert images found into picture entries
            //    foreach (Match item in images)
            //    {
            //        var purl = item.Groups[3].Value;
            //        //get id and trim if necessary (ran into a few cases of rediculously long filenames)
            //        var id = System.IO.Path.GetFileNameWithoutExtension(purl);
            //        if (id.Length > 50) id = id.Substring(0, 50);

            //        result.Pictures.Add(new Picture() { Url = purl, Id = id });
            //    }

            //    //if we have an image ban list check for them
            //    // doing this in the provider instead of picture manager
            //    // ensures that our count does not go down if we have a max
            //    if (ps.BannedURLs != null && ps.BannedURLs.Count > 0)
            //    {
            //        result.Pictures = (from c in result.Pictures where !(ps.BannedURLs.Contains(c.Url)) select c).ToList();
            //    }

            //    //increment page index so we can get the next 20 images if they exist
            //    pageIndex++;
            //    // Max Picture count is defined in search settings passed in, check for it here too
            //} while (imgFoundCount > 0 && result.Pictures.Count < maxPictureCount);

            return result;
        }
    }
}
