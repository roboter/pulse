using System;
using System.Collections.Generic;
using Pulse.Base;

namespace wallhaven
{
    public class WallhavenSearchSettings : XmlSerializable<WallhavenSearchSettings>
    {
        public const string BaseApiUrl = "https://wallhaven.cc/api/v1/search";

        public string Query { get; set; }
        public string ApiKey { get; set; }

        // Categories: General (1xx), Anime (x1x), People (xx1)
        public bool General { get; set; }
        public bool Anime { get; set; }
        public bool People { get; set; }

        // Purity: SFW (1xx), Sketchy (x1x), NSFW (xx1 - requires API Key)
        public bool SFW { get; set; }
        public bool Sketchy { get; set; }
        public bool NSFW { get; set; }

        // Sorting: date_added, relevance, random, views, favorites, toplist
        public string Sorting { get; set; }

        // Order: desc, asc
        public string Order { get; set; }

        // TopRange: 1d, 3d, 1w, 1M, 3M, 6M, 1y (only when sorting is toplist)
        public string TopRange { get; set; }

        // Sizing & Resolution
        // ResolutionMode: "AtLeast", "Exact", "None"
        public string ResolutionMode { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }

        // Aspect ratio: e.g. "16x9", "16x10", "21x9", etc., or empty for any
        public string AspectRatio { get; set; }

        // Color filter: 6-char hex code without #, e.g. "660000"
        public string Color { get; set; }

        public WallhavenSearchSettings()
        {
            Query = "nature";
            ApiKey = string.Empty;

            General = true;
            Anime = true;
            People = false;

            SFW = true;
            Sketchy = false;
            NSFW = false;

            Sorting = "relevance";
            Order = "desc";
            TopRange = "1M";

            ResolutionMode = "AtLeast";
            try
            {
                ImageWidth = PictureManager.PrimaryScreenResolution.First;
                ImageHeight = PictureManager.PrimaryScreenResolution.Second;
            }
            catch
            {
                ImageWidth = 1920;
                ImageHeight = 1080;
            }

            AspectRatio = string.Empty;
            Color = string.Empty;
        }

        public string GetCategoriesParam()
        {
            return string.Format("{0}{1}{2}",
                General ? "1" : "0",
                Anime ? "1" : "0",
                People ? "1" : "0");
        }

        public string GetPurityParam()
        {
            return string.Format("{0}{1}{2}",
                SFW ? "1" : "0",
                Sketchy ? "1" : "0",
                NSFW ? "1" : "0");
        }

        public string BuildUrl(int page = 1, string seed = null)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrWhiteSpace(Query))
            {
                queryParams.Add("q=" + Uri.EscapeDataString(Query.Trim()));
            }

            queryParams.Add("categories=" + GetCategoriesParam());
            queryParams.Add("purity=" + GetPurityParam());

            if (!string.IsNullOrWhiteSpace(Sorting))
            {
                queryParams.Add("sorting=" + Sorting.ToLowerInvariant());
            }

            if (!string.IsNullOrWhiteSpace(Order))
            {
                queryParams.Add("order=" + Order.ToLowerInvariant());
            }

            if (string.Equals(Sorting, "toplist", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(TopRange))
            {
                queryParams.Add("topRange=" + TopRange);
            }

            if (string.Equals(ResolutionMode, "AtLeast", StringComparison.OrdinalIgnoreCase) && ImageWidth > 0 && ImageHeight > 0)
            {
                queryParams.Add(string.Format("atleast={0}x{1}", ImageWidth, ImageHeight));
            }
            else if (string.Equals(ResolutionMode, "Exact", StringComparison.OrdinalIgnoreCase) && ImageWidth > 0 && ImageHeight > 0)
            {
                queryParams.Add(string.Format("resolutions={0}x{1}", ImageWidth, ImageHeight));
            }

            if (!string.IsNullOrWhiteSpace(AspectRatio) && !string.Equals(AspectRatio, "any", StringComparison.OrdinalIgnoreCase))
            {
                queryParams.Add("ratios=" + AspectRatio.Trim());
            }

            if (!string.IsNullOrWhiteSpace(Color))
            {
                queryParams.Add("colors=" + Color.Trim().TrimStart('#'));
            }

            if (page > 1)
            {
                queryParams.Add("page=" + page);
            }

            if (!string.IsNullOrWhiteSpace(seed))
            {
                queryParams.Add("seed=" + seed.Trim());
            }

            if (!string.IsNullOrWhiteSpace(ApiKey))
            {
                queryParams.Add("apikey=" + Uri.EscapeDataString(ApiKey.Trim()));
            }

            return BaseApiUrl + "?" + string.Join("&", queryParams.ToArray());
        }
    }
}
