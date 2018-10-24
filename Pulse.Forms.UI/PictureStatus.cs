using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pulse.Base;
using System.IO;

namespace Pulse.Forms.UI
{
    public partial class PictureStatus : UserControl
    {
        public PictureDownload CurrentPicture { get; protected set; }
        
        public PictureStatus()
        {
            InitializeComponent();
        }

        public void SetPicture(PictureDownload pd)
        {
            this.CurrentPicture = pd;

            CurrentPicture.PictureDownloadStatusChanged += pd_PictureDownloadStatusChanged;
            CurrentPicture.PictureDownloadProgressChanged += pd_PictureDownloadProgressChanged;

            this.Invoke((Action)delegate
            {
                if (!lblFileName.IsDisposed)
                {
                    lblFileName.Text = Path.GetFileName(pd.Picture.LocalPath);

                    SetStatus(pd.Status);
                }
            });
        }

        public void UnhookEvents()
        {
            CurrentPicture.PictureDownloadStatusChanged -= pd_PictureDownloadStatusChanged;
            CurrentPicture.PictureDownloadProgressChanged -= pd_PictureDownloadProgressChanged;
        }

        void pd_PictureDownloadProgressChanged(PictureDownload sender)
        {
            this.Invoke((Action)delegate
            {
                if(!pbDownloadProgress.IsDisposed)
                    pbDownloadProgress.Value = sender.DownloadProgress;
            });
        }

        void pd_PictureDownloadStatusChanged(PictureDownload sender)
        {
            this.Invoke((Action)delegate
            {
                SetStatus(sender.Status);
            });
        }

        protected void SetStatus(Pulse.Base.PictureDownload.DownloadStatus status)
        {
            if (pbStatus.IsDisposed) return;

            switch (status)
            {
                case Pulse.Base.PictureDownload.DownloadStatus.Stopped:
                    pbStatus.Image = Properties.Resources.StatusAnnotations_Stop_16xLG_color;
                    break;
                case Pulse.Base.PictureDownload.DownloadStatus.Downloading:
                    pbStatus.Image = Properties.Resources.StatusAnnotations_Play_16xLG_color;
                    break;
                case Pulse.Base.PictureDownload.DownloadStatus.Complete:
                    pbStatus.Image = Properties.Resources.StatusAnnotations_Complete_and_ok_16xLG_color;
                    break;
                case Pulse.Base.PictureDownload.DownloadStatus.Cancelled:
                    pbStatus.Image = Properties.Resources.action_Cancel_16xLG;
                    break;
                case Pulse.Base.PictureDownload.DownloadStatus.Error:
                    pbStatus.Image = Properties.Resources.StatusAnnotations_Critical_16xLG_color;
                    break;
                default:
                    break;
            }
        }
    }
}
