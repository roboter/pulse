using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pulse.Base;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Pulse.Base.Providers;

namespace NationalGeographicWallpapers
{
    [System.ComponentModel.Description("National Geographic")]
    [ProviderIcon(typeof(Properties.Resources),"favicon_cb1274471343")]
    public class NationalGeographicProvider : Pulse.Base.IInputProvider
    {
        private string _baseURL = "http://ngm.nationalgeographic.com";
        public Pulse.Base.PictureList GetPictures(Pulse.Base.PictureSearch ps)
        {
            PictureList pl = new PictureList() { FetchDate = DateTime.Now };

            try
            {
                //general purpose downloader
                WebClient wc = new WebClient();
                wc.Headers.Add(HttpRequestHeader.UserAgent,
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/120 Safari/537.36");

                //download pictures page
                var content = wc.DownloadString(_baseURL + "/wallpaper/download");
                //get paths to the xml files
                var xmlPaths = ParseXMLPaths(content);

                //download and parse each xml file
                foreach (string xmlFile in xmlPaths)
                {
                    try
                    {
                        var pics = ParsePictures(xmlFile);

                        //clear out banned images
                        pics = (from c in pics where !ps.BannedURLs.Contains(c.Url) select c).ToList();

                        pl.Pictures.AddRange(pics);
                    }
                    catch (Exception ex)
                    {
                        Log.Logger.Write(string.Format("Error loading/parsing National Geographic pictures from XML.  XML file URL: '{0}'. Exception details: {1}", _baseURL + xmlFile, ex.ToString()), Log.LoggerLevels.Errors);
                    }

                    if (pl.Pictures.Count >= (ps.MaxPictureCount > 0 ? ps.MaxPictureCount : int.MaxValue))
                        break;
                }
            }
            catch (WebException ex)
            {
                Log.Logger.Write(string.Format("National Geographic provider failed to fetch wallpapers (network error). Exception details: {0}", ex.ToString()), Log.LoggerLevels.Errors);
            }
            catch (Exception ex)
            {
                Log.Logger.Write(string.Format("National Geographic provider encountered an unexpected error. Exception details: {0}", ex.ToString()), Log.LoggerLevels.Errors);
            }

            return pl;
        }

        private List<string> ParseXMLPaths(string content)
        {
            //ex: <option value="/wallpaper/2011/September_2011_Wallpapers.xml">September 2011</option>
            Regex regXMLPaths = new Regex("<option value=\"(?<xmlPath>.*)\">(?<title>.*)</option>");

            var matches = regXMLPaths.Matches(content);

            return (from Match c in matches select c.Groups["xmlPath"].Value).ToList();
        }

        private List<Picture> ParsePictures(string xmlUri)
        {
            XDocument xdoc = XDocument.Load(_baseURL + xmlUri);

            List<Picture> pics = (from c in xdoc.Element("PhotoGallery").Elements("photo")
                                  let wElement = c.Element("wallpaper")
                                  let wCount = wElement.Elements().Count()
                                  let wallpaper = wCount == 0 ? wElement.Value.Replace("\t", "").Replace("\n", "") : wElement.Elements().Last().Value.Replace("\t", "").Replace("\n", "")
                                  select new Picture()
                                  {
                                      Url = _baseURL + wallpaper,
                                      Id = System.IO.Path.GetFileNameWithoutExtension(_baseURL + wallpaper)
                                  }).ToList();

            return pics;
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
