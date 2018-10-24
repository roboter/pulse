using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pulse.Base;
using System.Drawing;
using Pulse.Base.WinAPI;

namespace Piler
{
    public class PilerProvider : IOutputProvider
    {
        private static Random _rand = new Random();

        public void ProcessPicture(PictureBatch pb, string config)
        {
            List<Picture> pics = pb.GetPictures(5);

            if (!pics.Any()) return;

            //for starters lets use an existing image as the backdrop
            Picture pBackdrop = pics.First();
            string savePath = System.IO.Path.Combine(Settings.CurrentSettings.CachePath,
                                                string.Format("{0}.jpg", Guid.NewGuid()));

            List<Rectangle> existingImages = new List<Rectangle>();

            using (Image bmpBackdrop = Image.FromFile(pBackdrop.LocalPath))
            {
                Graphics g = Graphics.FromImage(bmpBackdrop);

                //now get 4 or 5 other pics and strew them about
                foreach (Picture p in pics.Skip(1))
                {
                    Picture pPile = p;
                    using (Bitmap bmpToRotate = (Bitmap)Bitmap.FromFile(pPile.LocalPath))
                    {
                        //draw a 5px white border around the image
                        using (Bitmap bmpWithBorder = PictureManager.AppendBorder(bmpToRotate, 25, Color.White))
                        {

                            using (Bitmap bmpPile = PictureManager.RotateImage(bmpWithBorder, (float)_rand.Next(-30, 30)))
                            {
                                //pick a random x,y coordinate to draw the image, shrink to 25%
                                Rectangle r = Rectangle.Empty;

                                while (r == Rectangle.Empty)
                                {
                                    Rectangle tmp = new Rectangle(_rand.Next(50, bmpBackdrop.Width - 50),
                                                                    _rand.Next(50, bmpBackdrop.Height - 50),
                                                                    Convert.ToInt32(bmpBackdrop.Width * .1),
                                                                    Convert.ToInt32(bmpBackdrop.Height * .1));

                                    if (existingImages.Where(x => Rectangle.Intersect(x, tmp) != Rectangle.Empty).Count() == 0)
                                    {
                                        r = tmp;
                                        existingImages.Add(r);
                                    }
                                }

                                g.DrawImage(bmpPile, r);
                            }
                        }
                    }
                }
                
                bmpBackdrop.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            Desktop.SetWallpaperUsingActiveDesktop(savePath);
        }

        public void Activate(object args) { }
        public void Deactivate(object args) { }
        public void Initialize(object args) { }
    }
}
