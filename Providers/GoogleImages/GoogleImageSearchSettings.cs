using System;
using System.Collections.Generic;
using System.Linq;
using Pulse.Base;
using System.ComponentModel;

namespace GoogleImages
{
    public class GoogleImageSearchSettings : XmlSerializable<GoogleImageSearchSettings>
    {
        public const string BaseApiUrl = "https://www.googleapis.com/customsearch/v1";

        [DisplayName("Search Query")]
        [Description("Keywords to search for images (e.g. 'nature wallpaper', 'space nebula 4k')")]
        public string Query { get; set; }

        [DisplayName("Google API Key")]
        [Description("Google Cloud API Key with 'Custom Search API' enabled")]
        public string ApiKey { get; set; }

        [DisplayName("Search Engine ID (CX)")]
        [Description("Programmable Search Engine ID (cx) from programmablesearchengine.google.com with 'Image search' enabled")]
        public string SearchEngineId { get; set; }

        [DisplayName("Safe Search")]
        [Description("Filter adult / explicit content from search results")]
        public GoogleSafeSearchOptions GoogleSafeSearchOption { get; set; }

        [DisplayName("Image Size")]
        [Description("Filter images by size: any, large, xlarge, xxlarge, huge")]
        public string ImageSize { get; set; }

        [DisplayName("Color Type")]
        [Description("Filter by color type: any, color, gray, mono, trans")]
        public string ColorType { get; set; }

        [DisplayName("Dominant Color")]
        [Description("Filter by dominant color: any, black, blue, brown, gray, green, orange, pink, purple, red, teal, white, yellow")]
        public string DominantColor { get; set; }

        [DisplayName("Image Type")]
        [Description("Filter by image type: any, photo, clipart, lineart, stock, animated")]
        public string ImageType { get; set; }

        // Legacy sizing properties
        [DisplayName("Image Width")]
        public int ImageWidth { get; set; }

        [DisplayName("Image Height")]
        public int ImageHeight { get; set; }

        // Legacy color property
        [TypeConverter(typeof(GoogleImageColors.GoogleColorConverter))]
        public string Color { get; set; }

        public GoogleImageSearchSettings()
        {
            Query = "nature wallpaper";
            ApiKey = string.Empty;
            SearchEngineId = string.Empty;
            ImageSize = "large";
            ColorType = "any";
            DominantColor = "any";
            ImageType = "photo";
            ImageWidth = PictureManager.PrimaryScreenResolution.First;
            ImageHeight = PictureManager.PrimaryScreenResolution.Second;
            GoogleSafeSearchOption = GoogleSafeSearchOptions.On;
        }

        public string BuildUrl(int startIndex = 1, int count = 10)
        {
            var queryParams = new List<string>
            {
                "searchType=image",
                string.Format("q={0}", Uri.EscapeDataString(Query ?? string.Empty)),
                string.Format("start={0}", Math.Max(1, startIndex)),
                string.Format("num={0}", Math.Min(10, Math.Max(1, count)))
            };

            if (!string.IsNullOrEmpty(ApiKey))
            {
                queryParams.Add(string.Format("key={0}", ApiKey.Trim()));
            }

            if (!string.IsNullOrEmpty(SearchEngineId))
            {
                queryParams.Add(string.Format("cx={0}", SearchEngineId.Trim()));
            }

            // Safe search
            queryParams.Add(GoogleSafeSearchOption == GoogleSafeSearchOptions.Off ? "safe=off" : "safe=active");

            // Optional filters
            if (!string.IsNullOrEmpty(ImageSize) && !ImageSize.Equals("any", StringComparison.OrdinalIgnoreCase))
            {
                queryParams.Add(string.Format("imgSize={0}", ImageSize.ToLowerInvariant()));
            }

            if (!string.IsNullOrEmpty(ColorType) && !ColorType.Equals("any", StringComparison.OrdinalIgnoreCase))
            {
                queryParams.Add(string.Format("imgColorType={0}", ColorType.ToLowerInvariant()));
            }

            if (!string.IsNullOrEmpty(DominantColor) && !DominantColor.Equals("any", StringComparison.OrdinalIgnoreCase))
            {
                queryParams.Add(string.Format("imgDominantColor={0}", DominantColor.ToLowerInvariant()));
            }

            if (!string.IsNullOrEmpty(ImageType) && !ImageType.Equals("any", StringComparison.OrdinalIgnoreCase))
            {
                queryParams.Add(string.Format("imgType={0}", ImageType.ToLowerInvariant()));
            }

            return BaseApiUrl + "?" + string.Join("&", queryParams.ToArray());
        }

        public class GoogleImageColors
        {
            public string Name { get; private set; }
            public string Value { get; private set; }
            public bool Specific { get; private set; }

            private static List<GoogleImageColors> _colors = null;

            public static string GetColorSearchString(GoogleImageColors color)
            {
                if (string.IsNullOrEmpty(color.Value)) return string.Empty;

                if (!color.Specific)
                {
                    return string.Format("ic:{0}", color.Value);
                }

                return string.Format("ic:specific,isc:{0}", color.Value);
            }

            public static List<GoogleImageColors> GetColors()
            {
                if (_colors == null)
                {
                    _colors = new List<GoogleImageColors>();
                    //default option, no restriction based on color
                    _colors.Add(new GoogleImageColors() { Name = "Any Color", Value = "", Specific = false });
                    //Full Color and Black & White are ic:{color,gray}
                    _colors.Add(new GoogleImageColors() { Name = "Full Color", Value = "color", Specific = false });
                    _colors.Add(new GoogleImageColors() { Name = "Black & White", Value = "gray", Specific = false });
                    //all specific colors are ic:specific,isc:{red,green,etc}
                    _colors.Add(new GoogleImageColors() { Name = "Red", Value = "red", Specific = true });
                    _colors.Add(new GoogleImageColors() { Name = "Orange", Value = "orange", Specific = true });
                    _colors.Add(new GoogleImageColors() { Name = "Yellow", Value = "yellow", Specific = true });
                    _colors.Add(new GoogleImageColors() { Name = "Green", Value = "green", Specific = true });
                    _colors.Add(new GoogleImageColors() { Name = "Teal", Value = "teal", Specific = true });
                    _colors.Add(new GoogleImageColors() { Name = "Blue", Value = "blue", Specific = true });
                    _colors.Add(new GoogleImageColors() { Name = "Purple", Value = "purple", Specific = true });
                    _colors.Add(new GoogleImageColors() { Name = "Pink", Value = "pink", Specific = true });
                    _colors.Add(new GoogleImageColors() { Name = "White", Value = "white", Specific = true });
                    _colors.Add(new GoogleImageColors() { Name = "Black", Value = "black", Specific = true });
                    _colors.Add(new GoogleImageColors() { Name = "Brown", Value = "brown", Specific = true });
                }

                return _colors;
            }

            public class GoogleColorConverter : StringConverter
            {
                public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
                {
                    return true;
                }

                public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
                {
                    return true;
                }

                public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
                {
                    return GetColors().Where(x => value != null && x.Name == value.ToString()).Select(x => x.Value).First();
                }

                public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
                {
                    if (value == null)
                    {
                        return base.ConvertTo(context, culture, null, destinationType);
                    }
                    return GetColors().Where(x => x.Value == value.ToString()).Select(x => x.Name).First();
                }

                public override System.ComponentModel.TypeConverter.StandardValuesCollection
                       GetStandardValues(ITypeDescriptorContext context)
                {
                    return new StandardValuesCollection(GetColors().Select(x => x.Value).ToList());
                }
            }
        }

        public enum GoogleSafeSearchOptions
        {
            Off,
            On
        }
    }
}
