using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using Pulse.Base;
using Pulse.Base.Pictures;

namespace Pulse.Base
{
    public class DownloadManager : IDisposable
    {
        public static DownloadManager Current
        {
            get { return _current; }
        }

        private static DownloadManager _current = new DownloadManager();

        /// <summary>
        /// location where images are saved to
        /// </summary>
        public string SaveFolder { get; set; }

        public bool Active { get { return _queuePollingTimer.Enabled; } set { _queuePollingTimer.Enabled = value; } }

        public event PictureDownload.PictureDownloadEvent QueueIsEmpty;
        
        //Events for Download Manager UI
        public event PictureDownload.PictureDownloadEvent PictureAddedToQueue;
        public event PictureDownload.PictureDownloadEvent PictureRemovedFromQueue;

        public event PictureDownload.PictureDownloadEvent PictureDownloaded;
        public event PictureDownload.PictureDownloadEvent PictureDownloading;
        public event PictureDownload.PictureDownloadEvent PictureDownloadingAborted;
        public event PictureDownload.PictureDownloadEvent PictureDownloadProgressChanged;


        //how often to check for new images in the queue/open slots in the queue
        private System.Timers.Timer _queuePollingTimer = new System.Timers.Timer(1000);
        
        //number of concurrent image downloads to perform
        private int _maxConcurrentDownloads = 5;

        private int _failureRetries = 3;

        //used for random image selection
        private readonly Random rnd = new Random();

        //download queue for all queued images
        private DownloadQueue _downloadQueue = null;

        public DownloadManager() : this(Settings.CurrentSettings.CachePath) { 
           
        }

        public DownloadManager(string saveFolder)
        {
            _downloadQueue = new DownloadQueue();

            this.SaveFolder = saveFolder;

            //validate that the output directory exists
            if (!Directory.Exists(SaveFolder))
                Directory.CreateDirectory(SaveFolder);

            //setup the timer
            _queuePollingTimer.Elapsed += new System.Timers.ElapsedEventHandler(_queuePollingTimer_Elapsed);
            _queuePollingTimer.Enabled = true;
        }

        public void QueuePicture(PictureDownload p)
        {
            if (_downloadQueue.Contains(p.Picture.Url)) return;

            //add to the queue
            _downloadQueue.Add(p);

            //hook events
            p.PictureDownloaded += p_PictureDownloaded;
            p.PictureDownloading += p_PictureDownloading;
            p.PictureDownloadingAborted += p_PictureDownloadingAborted;
            p.PictureDownloadProgressChanged += p_PictureDownloadProgressChanged;

            //call picture added event
            if (PictureAddedToQueue != null) PictureAddedToQueue(p);
        }

        public void RemovePictureFromQueue(PictureDownload p)
        {
            if (!_downloadQueue.Contains(p)) return;
            
            //call cancel, just in case
            p.CancelDownload();

            _downloadQueue.Remove(p);

            //call removed event
            if (PictureRemovedFromQueue != null) PictureRemovedFromQueue(p);
        }

        public void ClearQueue()
        {
            foreach (PictureDownload pd in _downloadQueue.ToList())
            {
                RemovePictureFromQueue(pd);
            }
        }

        #region PictureEventBubbler
        void p_PictureDownloadProgressChanged(PictureDownload sender)
        {
            if (PictureDownloadProgressChanged != null) PictureDownloadProgressChanged(sender);
        }

        void p_PictureDownloadingAborted(PictureDownload sender)
        {
            if (PictureDownloadingAborted != null) PictureDownloadingAborted(sender);
        }

        void p_PictureDownloading(PictureDownload sender)
        {
            if (PictureDownloading != null) PictureDownloading(sender);
        }

        void p_PictureDownloaded(PictureDownload sender)
        {
            if (PictureDownloaded != null) PictureDownloaded(sender);
        }
        #endregion
        
        private void _queuePollingTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _queuePollingTimer.Stop();
            try
            {
                var queueSnapshot = _downloadQueue.ToList();

                //check for any open spots in the queue
                var count = (from c in queueSnapshot
                             where c.Status == PictureDownload.DownloadStatus.Downloading
                             select c).Count();

                //get pictures that are queued up to go
                var queued = (from c in queueSnapshot
                               where c.Status == PictureDownload.DownloadStatus.Stopped ||
                               (c.Status == PictureDownload.DownloadStatus.Error && c.FailureCount < _failureRetries)
                               orderby c.Priority ascending
                               select c);

                if (queued.Any())
                {
                    //start up the difference
                    //Include download retries here by checking for status of error and retry count < limit
                    var toStart = queued.Take(_maxConcurrentDownloads - count);

                    foreach (PictureDownload pd in toStart)
                    {
                        pd.StartDownload();
                    }
                }
                else
                    if (QueueIsEmpty != null) QueueIsEmpty(null);
            }
            catch (Exception ex)
            {
                Log.Logger.Write(string.Format("Error starting queued downloads. Exception details: {0}", ex.ToString()), Log.LoggerLevels.Errors);
            }
            finally { _queuePollingTimer.Start(); }
        }

        /// <summary>
        /// Retrieves a random picture from the picture list
        /// </summary>
        /// <param name="pl">Picture list from which to retrieve pictures</param>
        /// <param name="saveFolder">Location where to save the picture</param>
        /// <param name="currentPicture">(optional) the current picture, to avoid repeates.  Pass null if not needed or this is the first picture.</param>
        public PictureDownload GetPicture(PictureList pl, Picture currentPicture, bool queueForDownload)
        {
            Picture pic = null;

            if (pl == null || pl.Pictures.Count == 0) return null;

            //pick the next picture at random
            // only "non-random" bit is that we make sure that the next random picture isn't the same as our current one
            var index = 0;
            do
            {
                index = rnd.Next(pl.Pictures.Count);
            } while (currentPicture != null && currentPicture.Url == pl.Pictures[index].Url);

            pic = pl.Pictures[index];
            //download current picture first
            PictureDownload pd = GetPicture(pic, queueForDownload);

            return pd;
        }

        public PictureDownload GetPicture(Picture pic, bool queueForDownload)
        {
            //check if the requested image exists, if it does then return
            //if image already has a local path then use it (just what we need for local provider where images are not stored in cache).
            var picturePath = string.IsNullOrEmpty(pic.LocalPath) ? pic.CalculateLocalPath(SaveFolder) : pic.LocalPath;
            pic.LocalPath = picturePath;


            if (pic.IsGood)
            {
                //if the wallpaper image already exists, and passes our 0 size check then fire the event
                return new PictureDownload(pic);
            }

            var fi = new FileInfo(picturePath);

            //for files that do not exist "Delete" does nothing per MSDN docs
            try { fi.Delete(); }
            catch (Exception ex)
            {
                Log.Logger.Write(string.Format("Error deleting 0 byte file '{0}' while prepareing to redownload it. Exception details: {1}", fi.FullName, ex.ToString()), Log.LoggerLevels.Errors);
            }

            PictureDownload pd = new PictureDownload(pic);

            if (queueForDownload)
            {
                //add file to Queue
                QueuePicture(pd);
            }

            return pd;
        }

        public void PreFetchFiles(PictureBatch pb)
        {
            if (pb == null || pb.AllPictures.Count == 0) return;

            foreach (PictureList pl in pb.AllPictures.ToList())
            {
                foreach (Picture pic in pl.Pictures.ToList())
                {
                    GetPicture(pic, true);
                }
            }
        }

        public void Dispose()
        {
            _queuePollingTimer.Dispose();
        }
    }
}
