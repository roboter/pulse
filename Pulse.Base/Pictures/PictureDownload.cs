using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Pulse.Base
{
    public class PictureDownload : IDisposable
    {
        public delegate void PictureDownloadEvent(PictureDownload sender);

        public event PictureDownloadEvent PictureDownloaded;
        public event PictureDownloadEvent PictureDownloading;
        public event PictureDownloadEvent PictureDownloadingAborted;
        public event PictureDownloadEvent PictureDownloadProgressChanged;
        public event PictureDownloadEvent PictureDownloadStatusChanged;

        public Picture Picture { get; private set; }
        public int DownloadProgress { get; private set; }
        public DownloadStatus Status
        {
            get { return _status; }
            private set { 
                _status = value;
                if (PictureDownloadStatusChanged != null)
                    PictureDownloadStatusChanged(this);
            }
        }
        public Exception LastError { get; private set; }
        public int Priority { get; set; }
        public int FailureCount { get { return _failureCount; } private set { _failureCount = value; } }

        private HttpUtility.CookieAwareWebClient _client = new HttpUtility.CookieAwareWebClient();
        private int _failureCount = 0;
        private string _tempDownloadPath = string.Empty;
        private DownloadStatus _status;

        public enum DownloadStatus
        {            
            Downloading = 1,
            Error = 10,
            Stopped = 20,
            Complete = 100,
            Cancelled = 500            
        }

        public PictureDownload(Picture p)
        {
            if (string.IsNullOrEmpty(p.LocalPath)) throw new ArgumentNullException("The Local Path of the picture must be defined before downloading it");

            this.Picture = p;
            //set default priority
            this.Priority = 999999999;

            //set default status of Stopped
            Status = DownloadStatus.Stopped;

            //setup webclient events
            _client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(_client_DownloadFileCompleted);
            _client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(_client_DownloadProgressChanged);
        }

        public void StartDownload()
        {
            //check if we have a referrer URL and assign it
            if (Picture.Properties.ContainsKey(Picture.StandardProperties.Referrer))
                _client.Referrer = Picture.Properties[Picture.StandardProperties.Referrer];

            if (!this.Picture.IsGood)
            {
                //generate a temp path to save the file to
                _tempDownloadPath = Path.Combine(Path.GetTempPath(), "PulseTemp_" + Path.GetRandomFileName());
                //download the file async
                _client.DownloadFileAsync(new Uri(Picture.Url), _tempDownloadPath, Picture);

                //set status to downloading
                Status = DownloadStatus.Downloading;
                //call picture downloading event
                if (PictureDownloading != null) PictureDownloading(this);
            }
            else
            {
                //if picture is already in good shape then don't worry about it
                MarkAsComplete();
            }
        }

        public void CancelDownload()
        {
            //if the client is busy then cancel, else just set cancelled
            if (_client.IsBusy)
                _client.CancelAsync();
            else
                Status = DownloadStatus.Cancelled;
        }

        void deleteTempFile()
        {
            try
            {
                //delete temp file (this fails sometimes so I put it in a try/catch)
                File.Delete(_tempDownloadPath);
            }
            catch { }
        }

        void _client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadProgress = e.ProgressPercentage;
            //call picture download progress changed
            if (PictureDownloadProgressChanged != null) PictureDownloadProgressChanged(this);
        }

        void _client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //if this was cancelled by the user then don't retry.  Set progress to 0 and downloading flag to false
            if (e.Cancelled) { 
                Status = DownloadStatus.Cancelled;
                DownloadProgress = 0;
                //try to delete the partially downloaded file
                deleteTempFile();

                //call aborted event
                if (PictureDownloadingAborted != null) PictureDownloadingAborted(this);

                return; 
            }
            
            //if this download errored check our retry count and initiate it again
            if(e.Error != null) {
                LastError = e.Error;

                HandleErrorRetry();
                //stop processing on error (retry is handled in above function)
                return;
            }

            //move the temporary file to it's final destination
            try
            {
                //copy temp file to final destination
                File.Copy(_tempDownloadPath, Picture.LocalPath, true);

                //mark the file as complete
                MarkAsComplete();
            }
            catch {
                HandleErrorRetry();
            }

            deleteTempFile();
        }

        private void HandleErrorRetry()
        {
            //try delete temp file on error/retry
            deleteTempFile();

            _failureCount++;
            Status = DownloadStatus.Error;
            //call download aborted
            if (PictureDownloadingAborted != null) PictureDownloadingAborted(this);
        }

        private void MarkAsComplete()
        {
            //if we aren't cancelled and didn't error then we were successful
            Status = DownloadStatus.Complete;

            //call download complete
            if (PictureDownloaded != null) PictureDownloaded(this);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
