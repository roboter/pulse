using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;
using Pulse.Base;
using Pulse.Base.WinAPI;
using System.IO;
using Pulse.Base.Providers;

namespace AeroGlassChanger
{
    //Most of this code came from http://aura.codeplex.com/ (Aura project)
    [System.ComponentModel.Description("Aero Glass Color Sync")]
    [ProviderPlatform(PlatformID.Win32NT, 6, 2)] //windows 8
    [ProviderPlatform(PlatformID.Win32NT, 6, 1)] //windows 7
    [ProviderRunsAsyncAttribute(true)]
    public class AeroGlassChangerProvider : Pulse.Base.IOutputProvider
    {
        public void ProcessPicture(Pulse.Base.PictureBatch pb, string config)
        {
            List<Picture> lp = pb.GetPictures(1);
            if (!lp.Any()) return;
            Picture p = lp.First();

            ManualResetEvent mre = new ManualResetEvent(false);

            int stepCount = 13;

            //get color to start with
            Color currentAero = Desktop.GetCurrentAeroColor();
            Color endAeroColor;

            //load file
            using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(p.LocalPath)))
            {
                using (Bitmap bmp = (Bitmap)Bitmap.FromStream(ms))
                {
                    //get final color
                    endAeroColor = PictureManager.CalcAverageColor(bmp);
                }
            }

            //build transition
            Color[] transitionColors = CalcColorTransition(currentAero, endAeroColor, stepCount);

            //build timer
            System.Timers.Timer t = new System.Timers.Timer(100);

            int currentStep = 0;

            t.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e)
            {
                //double check (I've seen cases where timer fires even though currentStep is past {stepCount}
                if (currentStep >= stepCount) { mre.Set(); t.Stop(); return; }

                //set to next color
                Desktop.SetAeroColor(transitionColors[currentStep]);

                //increment steps and check if we should stop the timer
                currentStep++;
                if (currentStep >= stepCount) { mre.Set(); t.Stop(); }
            };

            t.Start();

            mre.WaitOne();
        }

        public static Color[] CalcColorTransition(Color from, Color to, int steps)
        {
            Bitmap img = new Bitmap(1, steps);
            Rectangle rect = new Rectangle(0,0,1,steps);

            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush(
               rect,
               from,
               to,
               LinearGradientMode.Vertical);

            var g = Graphics.FromImage(img);

            g.FillRectangle(myLinearGradientBrush, rect);

            Color[] colors = new Color[steps];

            for (int p = 0; p < steps; p++)
            {
                colors[p] = img.GetPixel(0, p);
            }

            return colors;

        }


        public void Activate(object args) { }

        public void Deactivate(object args) { }

        public void Initialize(object args) { }
    }
}
