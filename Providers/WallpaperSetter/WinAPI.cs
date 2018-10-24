using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Pulse.Base
{
    public class WinAPI
    {
        public const UInt32 SPIF_UPDATEINIFILE = 0x1;
        public const int SPIF_SENDWININICHANGE = 0x02;
        public const UInt32 SPI_SETDESKWALLPAPER = 20;
        public static readonly UInt32 SPI_GETDESKWALLPAPER = 0x73;
        public static readonly int MAX_PATH = 260;


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, String pvParam, UInt32 fWinIni);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

    }
}
