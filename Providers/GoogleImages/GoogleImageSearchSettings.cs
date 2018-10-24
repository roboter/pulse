using System;
using System.Collections.Generic;
using System.Linq;
using Pulse.Base;
using System.ComponentModel;

namespace GoogleImages
{
    
    public class GoogleImageSearchSettings : XmlSerializable<GoogleImageSearchSettings>
    {
        public string Query { get; set; }

        //most of these settings are in the tbs section

        //for exact searches with &tbs=isz:ex
        //iszw:{imageWidth}
        [DisplayName("Image Width")]
        public int ImageWidth { get; set; }
        //iszh:{imageHeight}
        [DisplayName("Image Height")]
        public int ImageHeight { get; set; }

        [DisplayName("Safe Search")]
        public GoogleSafeSearchOptions GoogleSafeSearchOption { get; set; }

        //color also goes under "tbs" as well.  See GoogleImageColors
        // for examples of colors and string format
        [TypeConverter(typeof(GoogleImageColors.GoogleColorConverter))]
        public string Color { get; set; }

        public GoogleImageSearchSettings(){
           ImageWidth = PictureManager.PrimaryScreenResolution.First;
           ImageHeight = PictureManager.PrimaryScreenResolution.Second;
           GoogleSafeSearchOption = GoogleSafeSearchOptions.On;
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
                    //This causes issues for me because there are two named gray
                    //_colors.Add(new GoogleImageColors() { Name = "Gray", Value = "gray", Specific = true });
                    _colors.Add(new GoogleImageColors() { Name = "Black", Value = "black", Specific = true });
                    _colors.Add(new GoogleImageColors() { Name = "Brown", Value = "brown", Specific = true });
                }

                return _colors;
            }

            public class GoogleColorConverter : StringConverter
            {
                public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
                {
                    //true means show a combobox
                    return true;
                }

                public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
                {
                    //true will limit to list. false will show the list, 
                    //but allow free-form entry
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
                    return new StandardValuesCollection(GetColors().Select(x=> x.Value).ToList());
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
