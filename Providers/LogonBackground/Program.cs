using System.Linq;

namespace LogonBackground
{
    class Program
    {
        static void Main(string[] args)
        {
            OEMBackgroundManager oembm = new OEMBackgroundManager();

            if (args.Contains("/enableoembg"))
            {
                oembm.EnableOEMBackground();
                oembm.CreateDirs();
            }

            if (args.Contains("/disableoembg"))
            {
                oembm.DisableOEMBackground();
            }
        }
    }
}
