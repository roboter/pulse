using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows;
using System.Drawing;
using System.Security.Cryptography;
using System.Net;
using Pulse.Base;
using System.Collections.Specialized;

namespace wallbase
{
    public class WallbaseImageSearchSettings : Pulse.Base.XmlSerializable<WallbaseImageSearchSettings>
    {
        //                          
        private const string Url = "http://alpha.wallhaven.cc/search?q={0}&categories={1}&purity={2}&resolutions={3}&sorting={4}&order={5}&page={6}";

        public string Query { get; set; }
        public string WallbaseSearchLabel { get; set; }

        //authentication info
        public string Username { get; set; }
        [XmlIgnore()]
        public string Password { get; set; }
        
        [XmlElement("Password")]
        public string xmlPassword {
            get { return Pulse.Base.GeneralHelper.Protect(Password); }
            set {
                //don't mess with empty passwords
                if (string.IsNullOrEmpty(value)) return;
                //if there is a password try to unprotect it
                Password = Pulse.Base.GeneralHelper.Unprotect(value); } 
        }

        //search location
        public string SA { get; set; }

        //categories
        public bool WG { get; set; }
        public bool W { get; set; }
        public bool HR { get; set; }

        //Purity
        public bool SFW { get; set; }
        public bool SKETCHY { get; set; }
        public bool NSFW { get; set; }

        //color, used for color searches (color searching is only available with "Search" type
        [XmlIgnore()]
        public System.Drawing.Color Color { get; set; }

        [XmlElement("Color")]
        public string ClrHtml
        {
            get { return ColorTranslator.ToHtml(Color); }
            set { Color = ColorTranslator.FromHtml(value); }
        }

        //collection ID for collection searches
        public string CollectionID { get; set; }

        //favorites ID for favorite showing
        public string FavoriteID { get; set; }

        //Image sizing information
        public string SO { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public string AR { get; set; }

        //order by
        public string OB { get; set; }
        public string OBD { get; set; }
        
        public WallbaseImageSearchSettings()
        {
            Query = "nature";

            SA = "search";

            WG = true;
            W = true;

            SFW = true;

            SO = "gteq";
            ImageWidth = PictureManager.PrimaryScreenResolution.First;
            ImageHeight = PictureManager.PrimaryScreenResolution.Second;
            AR = "";

            OB = "relevance";
            OBD = "desc";

            Color = System.Drawing.Color.Empty;
        }

        public string BuildPurityString()
        {
            return Convert.ToInt32(SFW).ToString() +
                Convert.ToInt32(SKETCHY).ToString() +
                Convert.ToInt32(NSFW).ToString();
        }

        public string BuildCategoryString()
        {
            return (WG ? "1" : "0") +
                (W ? "1" : "0") +
                (HR ? "1":"0");
        }

        public string BuildResolutionString()
        {
            return ImageHeight > 0 && ImageWidth > 0 ? ImageWidth.ToString() + "x" + ImageHeight.ToString() : "";
        }

        public string BuildURL()
        {
            string areaURL = string.Empty;

            var resolutionString = BuildResolutionString();
            var pageSize = GetPageSize();

            if (SA == "search" || SA == "toplist" || SA == "random")
            {
                //new URL includes {0} placeholder for page number
                areaURL = string.Format(Url, Query, BuildCategoryString(), BuildPurityString(), resolutionString, OB, OBD, "{0}");
                //areaURL = string.Format(Url, SA, "{0}", "wallpapers", BuildCategoryString(), Query, SO, resolutionString, AR, BuildPurityString(), pageSize, TopTimespan, GetColor(), OBD, OB);
            }
            else if (SA == "user/collection")
            {
                areaURL += string.Format("https://alpha.wallhaven.cc/user/Aheres/favorites/{0}", CollectionID, "{0}");
            }
            else if (SA == "user/favorites")
            {
                areaURL += string.Format("https://alpha.wallhaven.cc/favorites/{0}", FavoriteID, "{0}");
            }

            return areaURL;
        }

        public string GetColor()
        {
            return (Color == Color.Empty) ? "" : ClrHtml.Replace("#","");
        }

        public int GetPageSize()
        {
            //override page size, since user collections dont' seem to be changeable from 32
            if (SA == "user/collection" || SA == "user/favorites") return 32;
            //if not user collection then max size
            return 60;
        }

        public int GetMaxImageCount(int userMax)
        {
            if (SA == "user/collection" || SA == "user/favorites" || userMax == 0) return int.MaxValue;
            else return userMax;
        }

        public class SearchArea
        {
            public string Name { get; private set; }
            public string Value { get; private set; }

            public static List<SearchArea> GetSearchAreas()
            {
                List<SearchArea> sa = new List<SearchArea>();

                sa.Add(new SearchArea() { Name = "Search", Value = "search" });
                sa.Add(new SearchArea() { Name = "Top List", Value = "toplist" });
                sa.Add(new SearchArea() { Name = "Random", Value = "random" });
                sa.Add(new SearchArea() { Name = "Collection", Value = "user/collection" });
                sa.Add(new SearchArea() { Name = "Favorite", Value = "user/favorites" });
                return sa;
            }
        }

        public class OrderBy
        {
            public string Name { get; private set; }
            public string Value { get; private set; }

            public static List<OrderBy> GetOrderByList()
            {
                List<OrderBy> sa = new List<OrderBy>();

                sa.Add(new OrderBy() { Name = "Relevancy", Value = "relevance" });
                sa.Add(new OrderBy() { Name = "Date", Value = "date" });
                sa.Add(new OrderBy() { Name = "Views", Value = "views" });
                sa.Add(new OrderBy() { Name = "Favorites", Value = "favs" });
                sa.Add(new OrderBy() { Name = "Random", Value = "random" });

                return sa;
            }
        }

        public class OrderByDirection
        {
            public string Name { get; private set; }
            public string Value { get; private set; }

            public static List<OrderByDirection> GetDirectionList()
            {
                List<OrderByDirection> sa = new List<OrderByDirection>();

                sa.Add(new OrderByDirection() { Name = "Descending", Value = "desc" });
                sa.Add(new OrderByDirection() { Name = "Ascending", Value = "asc" });

                return sa;
            }
        }

        public class SizingOption
        {
            public string Name { get; private set; }
            public string Value { get; private set; }

            public static List<SizingOption> GetDirectionList()
            {
                List<SizingOption> sa = new List<SizingOption>();

                sa.Add(new SizingOption() { Name = "Exactly", Value = "eqeq" });
                sa.Add(new SizingOption() { Name = "At Least", Value = "gteq" });

                return sa;
            }
        }

        public class TopTimeSpan
        {
            public string Name { get; private set; }
            public string Value { get; private set; }

            public static List<TopTimeSpan> GetTimespanList()
            {
                List<TopTimeSpan> tts = new List<TopTimeSpan>();

                tts.Add(new TopTimeSpan() { Name = "1 day (24h)", Value = "1d" });
                tts.Add(new TopTimeSpan() { Name = "3 days", Value = "3d" });
                tts.Add(new TopTimeSpan() { Name = "1 week", Value = "1w" });
                tts.Add(new TopTimeSpan() { Name = "2 weeks", Value = "2w" });
                tts.Add(new TopTimeSpan() { Name = "1 month", Value = "1m" });
                tts.Add(new TopTimeSpan() { Name = "2 months", Value = "2m" });
                tts.Add(new TopTimeSpan() { Name = "3 months", Value = "3m" });
                tts.Add(new TopTimeSpan() { Name = "All time", Value = "1" });

                return tts;
            }
        }

        public class AspectRatio
        {
            public string Name { get; private set; }
            public string Value { get; private set; }

            public static List<AspectRatio> GetAspectRatioList()
            {
                List<AspectRatio> tts = new List<AspectRatio>();

                tts.Add(new AspectRatio() { Name = "All", Value = "" });
                tts.Add(new AspectRatio() { Name = "4:3", Value = "1.33" });
                tts.Add(new AspectRatio() { Name = "5:4", Value = "1.25" });
                tts.Add(new AspectRatio() { Name = "16:9", Value = "1.77" });
                tts.Add(new AspectRatio() { Name = "16:10", Value = "1.60" });
                tts.Add(new AspectRatio() { Name = "Netbook", Value = "1.70" });
                tts.Add(new AspectRatio() { Name = "Dual", Value = "2.50" });
                tts.Add(new AspectRatio() { Name = "Dual wide", Value = "3.20" });
                tts.Add(new AspectRatio() { Name = "Widescreen", Value = "1.01" });
                tts.Add(new AspectRatio() { Name = "Portrait", Value = "0.99" });

                return tts;
            }
        }
    }
}
