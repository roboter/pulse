namespace PulseForm
{
    partial class frmPulseHost
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmsTrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nextPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.banImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openCacheFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.niPulse = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsTrayIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsTrayIcon
            // 
            this.cmsTrayIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nextPictureToolStripMenuItem,
            this.previousPictureToolStripMenuItem,
            this.timerStatusToolStripMenuItem,
            this.banImageToolStripMenuItem,
            this.toolStripSeparator2,
            this.openCacheFolderToolStripMenuItem,
            this.downloadManagerToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
            this.cmsTrayIcon.Name = "cmsTrayIcon";
            this.cmsTrayIcon.Size = new System.Drawing.Size(211, 230);
            // 
            // nextPictureToolStripMenuItem
            // 
            this.nextPictureToolStripMenuItem.Image = Pulse.Forms.UI.Properties.Resources.arrow_Next_16xLG;
            this.nextPictureToolStripMenuItem.Name = "nextPictureToolStripMenuItem";
            this.nextPictureToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.nextPictureToolStripMenuItem.Text = "Next Picture";
            this.nextPictureToolStripMenuItem.Click += new System.EventHandler(this.nextPictureToolStripMenuItem_Click);
            // 
            // previousPictureToolStripMenuItem
            // 
            this.previousPictureToolStripMenuItem.Enabled = false;
            this.previousPictureToolStripMenuItem.Image = Pulse.Forms.UI.Properties.Resources.arrow_previous_16xLG;
            this.previousPictureToolStripMenuItem.Name = "previousPictureToolStripMenuItem";
            this.previousPictureToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.previousPictureToolStripMenuItem.Text = "Previous Picture";
            this.previousPictureToolStripMenuItem.Click += new System.EventHandler(this.previousPictureToolStripMenuItem_Click);
            // 
            // timerStatusToolStripMenuItem
            // 
            this.timerStatusToolStripMenuItem.Image = Pulse.Forms.UI.Properties.Resources.PlayHS;
            this.timerStatusToolStripMenuItem.Name = "timerStatusToolStripMenuItem";
            this.timerStatusToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.timerStatusToolStripMenuItem.Text = "Start Timer";
            this.timerStatusToolStripMenuItem.Click += new System.EventHandler(this.timerStatusToolStripMenuItem_Click);
            // 
            // banImageToolStripMenuItem
            // 
            this.banImageToolStripMenuItem.Enabled = false;
            this.banImageToolStripMenuItem.Image = Pulse.Forms.UI.Properties.Resources.banimage;
            this.banImageToolStripMenuItem.Name = "banImageToolStripMenuItem";
            this.banImageToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.banImageToolStripMenuItem.Text = "Ban Image";
            this.banImageToolStripMenuItem.Click += new System.EventHandler(this.banImageToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(207, 6);
            // 
            // openCacheFolderToolStripMenuItem
            // 
            this.openCacheFolderToolStripMenuItem.Image = Pulse.Forms.UI.Properties.Resources.folder_Open_16xLG;
            this.openCacheFolderToolStripMenuItem.Name = "openCacheFolderToolStripMenuItem";
            this.openCacheFolderToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.openCacheFolderToolStripMenuItem.Text = "Open Cache Folder";
            this.openCacheFolderToolStripMenuItem.Click += new System.EventHandler(this.openCacheFolderToolStripMenuItem_Click);
            // 
            // downloadManagerToolStripMenuItem
            // 
            this.downloadManagerToolStripMenuItem.Image = Pulse.Forms.UI.Properties.Resources.asset_progressBar_26x26_on;
            this.downloadManagerToolStripMenuItem.Name = "downloadManagerToolStripMenuItem";
            this.downloadManagerToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.downloadManagerToolStripMenuItem.Text = "Download Manager";
            this.downloadManagerToolStripMenuItem.Click += new System.EventHandler(this.downloadManagerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = Pulse.Forms.UI.Properties.Resources.properties_16xLG;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(210, 24);
            this.toolStripMenuItem1.Text = "Options";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(207, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = Pulse.Forms.UI.Properties.Resources.Close_16xLG;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // niPulse
            // 
            this.niPulse.ContextMenuStrip = this.cmsTrayIcon;
            this.niPulse.Text = "Pulse";
            // 
            // frmPulseHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 58);
            this.ContextMenuStrip = this.cmsTrayIcon;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frmPulseHost";
            this.ShowInTaskbar = false;
            this.Text = "Pulse Host";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.frmPulseHost_Load);
            this.cmsTrayIcon.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCacheFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextPictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem banImageToolStripMenuItem;
        internal System.Windows.Forms.ContextMenuStrip cmsTrayIcon;
        public System.Windows.Forms.NotifyIcon niPulse;
        private System.Windows.Forms.ToolStripMenuItem downloadManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousPictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timerStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

