using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace Pulse.Base.WinAPI
{
    public class Desktop
    {
        public enum WallpaperStyle
        {
            Tile,
            Center,
            Stretch,
            Fit,
            Fill,
            NotSet
        }

        public static void EnableActiveDesktop()
        {
            IntPtr result = IntPtr.Zero;
            WinAPI.SendMessageTimeout(WinAPI.FindWindow("Progman", null), 0x52c, IntPtr.Zero, IntPtr.Zero, 0, 500, out result);
        }

        public static void SetWallpaperUsingActiveDesktop(string path)
        {
            EnableActiveDesktop();

            ThreadStart threadStarter = () =>
            {
                Pulse.Base.WinAPI.WinAPI.IActiveDesktop _activeDesktop = Pulse.Base.WinAPI.WinAPI.ActiveDesktopWrapper.GetActiveDesktop();
                _activeDesktop.SetWallpaper(path, 0);
                _activeDesktop.ApplyChanges(Pulse.Base.WinAPI.WinAPI.AD_Apply.ALL | Pulse.Base.WinAPI.WinAPI.AD_Apply.FORCE);

                Marshal.ReleaseComObject(_activeDesktop);
            };
            Thread thread = new Thread(threadStarter);
            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA (REQUIRED!!!!)
            thread.Start();
            thread.Join(2000);

        }

        public static void SetWallpaperWithRetry(string path, int retryCount, Action<string> sw)
        {
            //set the wallpaper to the new image
            sw(path);

            //check if really set and retry up to 3 times
            int tryCount = 0;
            do
            {
                //if matching, break
                if (GetWallpaperUsingSystemParameterInfo().ToLower() == path.ToLower()) break;

                sw(path);

                tryCount++;
            } while (tryCount < 3);
        }

        public static void SetWallpaperUsingSystemParameterInfo(string path)
        {
            WinAPI.SystemParametersInfo(WinAPI.SPI_SETDESKWALLPAPER, 0, path, WinAPI.SPIF_UPDATEINIFILE | WinAPI.SPIF_SENDWININICHANGE);
        }

        public static String GetWallpaperUsingSystemParameterInfo()
        {
            var wallpaper = new String('\0', WinAPI.MAX_PATH);
            WinAPI.SystemParametersInfo(WinAPI.SPI_GETDESKWALLPAPER, (UInt32)wallpaper.Length, wallpaper, 0);
            wallpaper = wallpaper.Substring(0, wallpaper.IndexOf('\0'));
            return wallpaper;
        }

        public static void SetDesktopBackgroundColor(Color newColor)
        {
            int[] aiElements = { WinAPI.COLOR_DESKTOP };
            WinAPI.SetSysColors(1, aiElements, new WinAPI.COLORREF(newColor));
        }

        public static void SetAeroColor(System.Drawing.Color newColor)
        {
            if (WinAPI.DwmIsCompositionEnabled())
            {
                WinAPI.DWM_COLORIZATION_PARAMS color;
                //get the current color
                WinAPI.DwmGetColorizationParameters(out color);
                //set new color to transition too
                color.ColorizationColor = (uint)System.Drawing.Color.FromArgb(255, newColor.R, newColor.G, newColor.B).ToArgb();
                //transition
                WinAPI.DwmSetColorizationParameters(ref color, 0);
            }
        }

        public static Color GetCurrentAeroColor()
        {
            if (WinAPI.DwmIsCompositionEnabled())
            {
                WinAPI.DWM_COLORIZATION_PARAMS color;
                //get the current color
                WinAPI.DwmGetColorizationParameters(out color);

                Color c = Color.FromArgb((int)color.ColorizationColor);

                return c;
            }

            return Color.Empty;
        }

        

        //from Microsoft example code:
        //http://code.msdn.microsoft.com/windowsdesktop/CSSetDesktopWallpaper-2107409c/sourcecode?fileId=21700&pathId=734742078
        public static void SetWallpaperType(WallpaperStyle style)
        {
            if (style == WallpaperStyle.NotSet) return;

            // Set the wallpaper style and tile.  
            // Two registry values are set in the Control Panel\Desktop key. 
            // TileWallpaper 
            //  0: The wallpaper picture should not be tiled  
            //  1: The wallpaper picture should be tiled  
            // WallpaperStyle 
            //  0:  The image is centered if TileWallpaper=0 or tiled if TileWallpaper=1 
            //  2:  The image is stretched to fill the screen 
            //  6:  The image is resized to fit the screen while maintaining the aspect  
            //      ratio. (Windows 7 and later) 
            //  10: The image is resized and cropped to fill the screen while  
            //      maintaining the aspect ratio. (Windows 7 and later) 
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);

            switch (style)
            {
                case WallpaperStyle.Tile:
                    key.SetValue(@"WallpaperStyle", "0");
                    key.SetValue(@"TileWallpaper", "1");
                    break;
                case WallpaperStyle.Center:
                    key.SetValue(@"WallpaperStyle", "0");
                    key.SetValue(@"TileWallpaper", "0");
                    break;
                case WallpaperStyle.Stretch:
                    key.SetValue(@"WallpaperStyle", "2");
                    key.SetValue(@"TileWallpaper", "0");
                    break;
                case WallpaperStyle.Fit: // (Windows 7 and later) 
                    key.SetValue(@"WallpaperStyle", "6");
                    key.SetValue(@"TileWallpaper", "0");
                    break;
                case WallpaperStyle.Fill: // (Windows 7 and later) 
                    key.SetValue(@"WallpaperStyle", "10");
                    key.SetValue(@"TileWallpaper", "0");
                    break;
            }

            key.Close();
        }
    }
}
