using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Drawing.Drawing2D;

namespace Pulse.Base
{
    public class PictureManager
    {
        #region "helpers"
        public static Pair<int, int> PrimaryScreenResolution { 
            get {
                var rect = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                return new Pair<int, int>(rect.Width, rect.Height);
            }
        }

        public static List<Rectangle> ScreenResolutions
        {
            get
            {
                List<Rectangle> rects = new List<Rectangle>();

                rects.AddRange(from c in System.Windows.Forms.Screen.AllScreens select c.Bounds);

                return rects;
            }
        }

        public static Rectangle TotalScreenResolution
        {
            get
            {
                List<Rectangle> rects = ScreenResolutions;

                Rectangle rect = new Rectangle(0,0,0,0);

                foreach (Rectangle r in rects)
                {
                    rect = Rectangle.Union(rect, r);
                }

                return rect;
            }
        }

        public static void ShrinkImage(string imgPath, string outPath, int destWidth, int destHeight, int quality)
        {
            using (Image img = ShrinkImage(imgPath, destWidth, destHeight))
            {
                //-----write out Thumbnail to the output stream------        
                //get jpeg image coded info so we can use it when saving
                ImageCodecInfo ici = ImageCodecInfo.GetImageEncoders().Where(c => c.MimeType == "image/jpeg").First();

                EncoderParameters epParameters = new EncoderParameters(1);
                epParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

                //save image to file
                img.Save(outPath, ici, epParameters);
            }
        }
        
        //Patricker - This code came from a stackoverflow answer I posted a while back.  I converted it to C# from VB.Net and from
        // asp.net to windows.  Link: http://stackoverflow.com/questions/4436209/asp-net-version-of-timthumb-php-class/4506072#4506072
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgPath">Path to image to load</param>
        /// <param name="destWidth">Desired width (0 to base off of aspect ratio and specified height)</param>
        /// <param name="destHeight">Desired Height (0 to base off of aspect ratio and specified width)</param>
        /// <remarks>If both height and width are 0 or non zero if the aspect ratio of the image
        /// does not match the aspect ratio based on width/height specifed then the image will skew. 
        /// (if both height/width are zero then screen resolution is used)</remarks>
        public static Image ShrinkImage(string imgPath, int destWidth, int destHeight)
        {
            //load image from file path
            using (FileStream fs = File.OpenRead(imgPath))
            {
                using (Image img = Bitmap.FromStream(fs))
                {
                    return ShrinkImage(img, destWidth, destHeight);
                }
            }
        }

        public static Image ShrinkImage(Image img, int destWidth, int destHeight)
        {
            double origRatio = (double)Math.Min(img.Width, img.Height) / (double)Math.Max(img.Width, img.Height);

            //---Calculate thumbnail sizes---
            double destRatio = 0;

            //if both width and height are 0 then use defaults (Screen resolution)
            if (destWidth == 0 & destHeight == 0)
            {
                var scResolution = PrimaryScreenResolution;
                destWidth = scResolution.First;
                destHeight = scResolution.Second;

            }
            else if (destWidth > 0 & destHeight > 0)
            {
                //do nothing, we have both sizes already
            }
            else if (destWidth > 0)
            {
                destHeight = (int)Math.Floor((double)img.Height * (destWidth / img.Width));
            }
            else if (destHeight > 0)
            {
                destWidth = (int)Math.Floor((double)img.Width * ((double)destHeight / (double)img.Height));
            }

            destRatio = (double)Math.Min(destWidth, destHeight) / (double)Math.Max(destWidth, destHeight);

            //calculate source image sizes (rectangle) to get pixel data from        
            int sourceWidth = img.Width;
            int sourceHeight = img.Height;

            int sourceX = 0;
            int sourceY = 0;

            double cmpx = (double)img.Width / (double)destWidth;
            double cmpy = (double)img.Height / (double)destHeight;

            //selection is based on the smallest dimension
            if (cmpx > cmpy)
            {
                sourceWidth = (int)((double)img.Width / cmpx * cmpy);
                sourceX = (int)(((double)img.Width - ((double)img.Width / cmpx * cmpy)) / 2);
            }
            else if (cmpy > cmpx)
            {
                sourceHeight = (int)((double)img.Height / cmpy * cmpx);
                sourceY = (int)(((double)img.Height - ((double)img.Height / cmpy * cmpx)) / 2);
            }

            //---create the new image---
            Bitmap bmpThumb = new Bitmap(destWidth, destHeight);
            var g = Graphics.FromImage(bmpThumb);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.DrawImage(img, new Rectangle(0, 0, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            return bmpThumb;
        }

        //From: http://stackoverflow.com/questions/2352804/how-do-i-prevent-clipping-when-rotating-an-image-in-c
        // Rotates the input image by theta degrees around center.
        public static Bitmap RotateImage(Bitmap bmpSrc, float theta)
        {
            Matrix mRotate = new Matrix();
            mRotate.Translate(bmpSrc.Width / -2, bmpSrc.Height / -2, MatrixOrder.Append);
            mRotate.RotateAt(theta, new System.Drawing.Point(0, 0), MatrixOrder.Append);
            using (GraphicsPath gp = new GraphicsPath())
            {  // transform image points by rotation matrix
                gp.AddPolygon(new System.Drawing.Point[] { new System.Drawing.Point(0, 0), new System.Drawing.Point(bmpSrc.Width, 0), new System.Drawing.Point(0, bmpSrc.Height) });
                gp.Transform(mRotate);
                System.Drawing.PointF[] pts = gp.PathPoints;

                // create destination bitmap sized to contain rotated source image
                Rectangle bbox = boundingBox(bmpSrc, mRotate);
                Bitmap bmpDest = new Bitmap(bbox.Width, bbox.Height);

                using (Graphics gDest = Graphics.FromImage(bmpDest))
                {  // draw source into dest
                    Matrix mDest = new Matrix();
                    mDest.Translate(bmpDest.Width / 2, bmpDest.Height / 2, MatrixOrder.Append);
                    gDest.Transform = mDest;
                    gDest.DrawImage(bmpSrc, pts);
                    return bmpDest;
                }
            }
        }

        private static Rectangle boundingBox(Image img, Matrix matrix)
        {
            GraphicsUnit gu = new GraphicsUnit();
            Rectangle rImg = Rectangle.Round(img.GetBounds(ref gu));

            // Transform the four points of the image, to get the resized bounding box.
            System.Drawing.Point topLeft = new System.Drawing.Point(rImg.Left, rImg.Top);
            System.Drawing.Point topRight = new System.Drawing.Point(rImg.Right, rImg.Top);
            System.Drawing.Point bottomRight = new System.Drawing.Point(rImg.Right, rImg.Bottom);
            System.Drawing.Point bottomLeft = new System.Drawing.Point(rImg.Left, rImg.Bottom);
            System.Drawing.Point[] points = new System.Drawing.Point[] { topLeft, topRight, bottomRight, bottomLeft };
            GraphicsPath gp = new GraphicsPath(points,
                                                                new byte[] { (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line });
            gp.Transform(matrix);
            return Rectangle.Round(gp.GetBounds());
        }

        public static Color CalcAverageColor(System.Drawing.Bitmap image)
        {
            var bmp = new System.Drawing.Bitmap(1, 1);
            var orig = image;
            using (var g = System.Drawing.Graphics.FromImage(bmp))
            {
                // the Interpolation mode needs to be set to 
                // HighQualityBilinear or HighQualityBicubic or this method
                // doesn't work at all.  With either setting, the results are
                // slightly different from the averaging method.
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(orig, new System.Drawing.Rectangle(0, 0, 1, 1));
            }
            var pixel = bmp.GetPixel(0, 0);
            orig.Dispose();
            bmp.Dispose();
            // pixel will contain average values for entire orig Bitmap
            return Color.FromArgb(pixel.R, pixel.G, pixel.B);
        }

        //From: http://stackoverflow.com/questions/74100/generate-thumbnail-with-white-border
        public static Bitmap AppendBorder(Bitmap original, int borderWidth, Color borderColor)
        {
            Size newSize = new Size(
                original.Width + borderWidth * 2,
                original.Height + borderWidth * 2);

            Bitmap img = new Bitmap(newSize.Width, newSize.Height);
            Graphics g = Graphics.FromImage(img);

            g.Clear(borderColor);
            g.DrawImage(original, new Point(borderWidth, borderWidth));
            g.Dispose();

            return img;
        }
        
        public static void ReduceQuality(string file, string destFile, int quality)
        {
            // we get the image/jpeg encoder using linq
            ImageCodecInfo iciJpegCodec = (from c in ImageCodecInfo.GetImageEncoders() where c.MimeType == "image/jpeg" select c).SingleOrDefault();

            // Store the quality parameter in the list of encoder parameters
            EncoderParameters epParameters = new EncoderParameters(1);
            epParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            //original image ms
            using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(file)))
            {
                //we use htis to keep track of the current size of the image, default to int.max to make sure we always run
                var fSize = int.MaxValue;

                using (Image newImage = Image.FromStream(ms))
                {
                    //check if the image we generated is larger then 245kb, if it is reduce quality by 10 and try again
                    while (fSize >= 245)
                    {
                        // Save the new file at tshe selected path with the specified encoder parameters, and reuse the same file name
                        newImage.Save(destFile, iciJpegCodec, epParameters);

                        //get output size in kb
                        fSize = (int)(new FileInfo(destFile).Length / 1024);

                        //reduce quality by 10, this will only affect the output if while loop continues
                        quality -= 10;
                        epParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                    }
                }
            }
        }
        #endregion

        public PictureList GetPictureList(PictureSearch ps)
        {
            PictureList Pictures = null;

            if (ps == null || ps.SearchProvider == null || ps.SearchProvider.Instance == null) return Pictures;
            //load any in memory cached results
            Pictures = ps.SearchProvider.SearchResults;

            var loadedFromFile = false;
            var fPath = Path.Combine(ps.SaveFolder, "CACHE_" + ps.GetSearchHash().ToString() + "_" + ps.SearchProvider.Instance.GetType().ToString() + ".xml");

            if (Pictures == null)
            {
                //if nothing in memory then try to load from disc
                Pictures = LoadCachedSearch(ps, fPath);
                loadedFromFile = Pictures != null;
            }
            else
            {
                loadedFromFile = false;
            }
            
            //if we have no pictures to work with, or our cached data has expired, try and get them
            if (Pictures == null || Pictures.Pictures.Count == 0 || Pictures.ExpirationDate < DateTime.Now)
            {
                Pictures = ((IInputProvider)ps.SearchProvider.Instance).GetPictures(ps);
                Pictures.SearchSettingsHash = ps.GetSearchHash();
                loadedFromFile = false;
            }
            
            //cache the picture list to file
            if (!loadedFromFile)
            {
                //make sure the API GuID has been injected into all pictures
                Pictures.Pictures.ForEach(x => x.ProviderInstance = ps.SearchProvider.ProviderInstanceID);

                //save it
                Pictures.Save(fPath);
            }

            //return whatever list of pictures was found
            return Pictures;
        }

        public PictureList LoadCachedSearch(PictureSearch ps, string cachePath)
        {
            PictureList result = null;

            //check if we should load from file
            if (File.Exists(cachePath))
            {
                try
                {
                    result = PictureList.LoadFromFile(cachePath);
                }
                catch (Exception ex)
                {
                    Log.Logger.Write(string.Format("Error loading picture cache from file, cache will not be used. Exception details: {0}", ex.ToString()), Log.LoggerLevels.Errors);
                }
            }

            return result;
        }
    }
}
