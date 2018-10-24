namespace Pulse.Forms.UI
{
    partial class DownloadQueue
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flpDownloads = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flpDownloads
            // 
            this.flpDownloads.AutoScroll = true;
            this.flpDownloads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDownloads.Location = new System.Drawing.Point(0, 0);
            this.flpDownloads.Margin = new System.Windows.Forms.Padding(2);
            this.flpDownloads.Name = "flpDownloads";
            this.flpDownloads.Size = new System.Drawing.Size(533, 365);
            this.flpDownloads.TabIndex = 1;
            // 
            // DownloadQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpDownloads);
            this.Name = "DownloadQueue";
            this.Size = new System.Drawing.Size(533, 365);
            this.Load += new System.EventHandler(this.DownloadQueue_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpDownloads;
    }
}
