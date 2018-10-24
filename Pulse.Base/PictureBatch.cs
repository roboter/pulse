using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pulse.Base
{
    public class PictureBatch
    {
        public Guid BatchID { get; set; }
        public List<PictureList> AllPictures { get; set; }
        public List<Picture> CurrentPictures { get; set; }
        
        public PictureBatch PreviousBatch {
            get { return _previousBatch; }
            set
            {   
                _previousBatch = value;
                //run decendent cleanup process
                PreviousBatchCleanupRecursive(0);
            }
        }

        private PictureBatch _previousBatch = null;
        
        public enum PictureBatchStatus
        {
            Prepping,
            Downloading,
            Finished
        }

        public PictureBatch()
        {
            BatchID = Guid.NewGuid();
            AllPictures = new List<PictureList>();
            CurrentPictures = new List<Picture>();
        }

        public List<Picture> GetPictures(int count)
        {
            lock (CurrentPictures)
            {
                //calculate how many pictures we need to get download  
                int toDownloadCount = count - CurrentPictures.Count;

                if (toDownloadCount > 0)
                {
                    Random random = new Random();
                    var allPics = (from c in AllPictures select c.Pictures)
                                    .SelectMany(p => p)
                                    .Where(p => !(CurrentPictures.Contains(p)))
                                    .OrderBy(p => random.Next());

                    List<Picture> toProcess = allPics.Take(count).ToList();

                    DownloadSelectedPictures(toProcess);
                }

                return CurrentPictures.Take(count).ToList();
            }
        }

        private void DownloadSelectedPictures(List<Picture> toProcess)
        {
            //validate that there are actually pictures to be processed (search may return 0 results, or very few results)
            if (toProcess.Count > 0)
            {
                //download the new pictures, wait for them to all finish
                ManualResetEvent[] manualEvents = new ManualResetEvent[toProcess.Count];

                // Queue the work items that create and write to the files.
                for (int i = 0; i < toProcess.Count; i++)
                {
                    manualEvents[i] = new ManualResetEvent(false);

                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object state)
                    {
                        object[] states = (object[])state;

                        ManualResetEvent mre = (ManualResetEvent)states[0];
                        Picture p = (Picture)states[1];

                        PictureDownload pd = DownloadManager.Current.GetPicture(p, false);

                        //hook events and set mre in event
                        PictureDownload.PictureDownloadEvent pdEvent = new PictureDownload.PictureDownloadEvent(
                                delegate(PictureDownload t)
                                {
                                    mre.Set();
                                });

                        //on success or failure make sure to set mre
                        pd.PictureDownloaded += pdEvent;
                        pd.PictureDownloadingAborted += pdEvent;

                        DownloadManager.Current.QueuePicture(pd);

                    }), new object[] { manualEvents[i], toProcess[i] });
                }

                //wait for all items to finish
                //3 minute timeout
                WaitHandle.WaitAll(manualEvents, 3 * 60 * 1000);

                CurrentPictures.AddRange(toProcess);
            }
        }

        private void PreviousBatchCleanupRecursive(int myDepth)
        {
            if (myDepth > Settings.CurrentSettings.MaxPreviousPictureDepth)
            {
                _previousBatch = null;
            }
            else
            {
                if(PreviousBatch != null) 
                    PreviousBatch.PreviousBatchCleanupRecursive(myDepth + 1);
            }
        }
    }
}
