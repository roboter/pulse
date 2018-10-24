using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pulse.Base;
using Pulse.Base.Providers;
using Microsoft.Win32;
using Pulse.Base.WinAPI;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace WallpaperSetter
{
    [System.ComponentModel.Description("Desktop Wallpaper")]
    [ProviderPlatform(PlatformID.Win32NT, 6, 0)]
    [ProviderConfigurationClass(typeof(WallpaperSetterSettings))]
    public class WallpaperSetterProvider : IOutputProvider
    {
        public void ProcessPicture(PictureBatch pb, string config)
        {
            List<Picture> lp = pb.GetPictures(1);
            if (!lp.Any()) return;
            Picture p = lp.First();

            //deserialize configuration
            WallpaperSetterSettings wss = null;

            if (!string.IsNullOrEmpty(config)) { wss = WallpaperSetterSettings.LoadFromXML(config); }
            else wss = new WallpaperSetterSettings();


            //set wallpaper style (tiled, centered, etc...)
            //SetWallpaperType(wss.Position);

            //set desktop background color
            //Code came roughly form http://www.tek-tips.com/viewthread.cfm?qid=1449619
            if (wss.BackgroundColorMode == WallpaperSetterSettings.BackgroundColorModes.Specific)
            {
                Desktop.SetDesktopBackgroundColor(wss.Color);
            }
            else if (wss.BackgroundColorMode == WallpaperSetterSettings.BackgroundColorModes.Computed)
            {
                using(Bitmap bmp = (Bitmap)Image.FromFile(p.LocalPath)) {
                    Desktop.SetDesktopBackgroundColor(PictureManager.CalcAverageColor(bmp));
                }
            }
           
            Desktop.SetWallpaperUsingSystemParameterInfo(p.LocalPath);            
        }
        
        public void Initialize(object args) { }
        public void Activate(object args) { }
        public void Deactivate(object args) { }
    }
}
