using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Pulse.Base;
using System.IO;
using System.Diagnostics;
using Pulse.Forms.UI;

namespace PulseForm
{
    public partial class frmPulseHost : Form
    {
        public static PulseRunner Runner;
        
        public frmPulseHost()
        {
            InitializeComponent();
        }

        private void frmPulseHost_Load(object sender, EventArgs e)
        {
            //Hook global exception handler for the app domain
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            //create and initialize the runner
            Runner = new PulseRunner();
            //listen for new version available events
            Runner.NewVersionAvailable += Runner_NewVersionAvailable;
            Runner.BatchChanged += Runner_PictureChanged;
            Runner.TimerStarted += Runner_TimerStarted;
            Runner.TimerStopped += Runner_TimerStopped;
            Runner.Initialize();

            if (Runner.CurrentInputProviders.Count == 0)
            {
                MessageBox.Show("A provider is not currently selected.  Please choose a wallpaper provider from the options menu.");
            }
        }

        void Runner_TimerStopped()
        {
            this.Invoke((Action)delegate
            {
                this.timerStatusToolStripMenuItem.Image = Pulse.Forms.UI.Properties.Resources.PlayHS;
                this.timerStatusToolStripMenuItem.Text = "Start Timer";
            });
        }

        void Runner_TimerStarted()
        {
            this.timerStatusToolStripMenuItem.Image = Pulse.Forms.UI.Properties.Resources.Breakall_6323;
            this.timerStatusToolStripMenuItem.Text = "Pause Timer";
        }

        void Runner_PictureChanged(PictureBatch obj)
        {
            this.Invoke((Action)delegate
            {
                banImageToolStripMenuItem.Enabled = obj.CurrentPictures.Count > 0;
                previousPictureToolStripMenuItem.Enabled = (obj.PreviousBatch != null && obj.PreviousBatch.CurrentPictures.Count > 0);
            });
        }

        void Runner_NewVersionAvailable(CodePlexNewReleaseChecker.Release obj)
        {
            //show popup that new version is available.
            NewPulseVersion npv = new NewPulseVersion();
            npv.LoadRelease(obj);

            npv.ShowDialog();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Log.Logger.Write("Unhandled exception! Error Details: " + args.ExceptionObject.ToString(), Log.LoggerLevels.Errors);

            MessageBox.Show("Pulse has encountered an exception and will exit.  The exception has been logged to the Pulse Log file.  Please post on the Issue Tracker page located on the Pulse Website @ http://pulse.codeplex.com/.  Exception details: " + ((Exception)args.ExceptionObject).Message, 
                "Pulse Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmPulseOptions po = new frmPulseOptions();
            po.UpdateSettings += new EventHandler(po_UpdateSettings);
            po.Show();
        }

        void po_UpdateSettings(object sender, EventArgs e)
        {
            Runner.UpdateFromConfiguration();
            Runner.SkipToNextPicture();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openCacheFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Settings.CurrentSettings.CachePath)) Directory.CreateDirectory(Settings.CurrentSettings.CachePath);
            //launch directory
            Process.Start(Settings.CurrentSettings.CachePath);
        }

        private void banImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Runner.CurrentBatch != null && Runner.CurrentBatch.CurrentPictures.Any())
            {
                foreach (Picture p in Runner.CurrentBatch.CurrentPictures)
                {
                    string banString =
                        (p.Properties.ContainsKey(Picture.StandardProperties.BanImageKey)
                        ? p.Properties[Picture.StandardProperties.BanImageKey] : p.Url);

                    if (MessageBox.Show(string.Format("Ban '{0}'?", banString), "Image Ban", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                    {
                        continue;
                    }

                    Runner.BanPicture(p);
                }
            }
        }

        private void nextPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Runner.SkipToNextPicture();
        }

        private void downloadManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadMonitor dm = new DownloadMonitor();
            dm.Show();
        }

        private void previousPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Runner.BackToPreviousPicture();
        }

        private void timerStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Runner.IsTimerRunning)
            {
                Runner.StopTimer();
            }
            else
            {
                Runner.StartTimer();
            }
        }
    }
}
