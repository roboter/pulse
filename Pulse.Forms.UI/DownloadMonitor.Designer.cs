namespace Pulse.Forms.UI
{
    partial class DownloadMonitor
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClearQueue = new System.Windows.Forms.Button();
            this.downloadQueue1 = new Pulse.Forms.UI.DownloadQueue();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.btnClearQueue);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.downloadQueue1);
            this.splitContainer1.Size = new System.Drawing.Size(661, 410);
            this.splitContainer1.SplitterDistance = 34;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.Location = new System.Drawing.Point(549, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 34);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClearQueue
            // 
            this.btnClearQueue.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnClearQueue.Image = global::Pulse.Forms.UI.Properties.Resources.Clearwindowcontent_6304;
            this.btnClearQueue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearQueue.Location = new System.Drawing.Point(0, 0);
            this.btnClearQueue.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClearQueue.Name = "btnClearQueue";
            this.btnClearQueue.Size = new System.Drawing.Size(112, 34);
            this.btnClearQueue.TabIndex = 0;
            this.btnClearQueue.Text = "Clear Queue";
            this.btnClearQueue.UseVisualStyleBackColor = true;
            this.btnClearQueue.Click += new System.EventHandler(this.btnClearQueue_Click);
            // 
            // downloadQueue1
            // 
            this.downloadQueue1.Location = new System.Drawing.Point(144, 105);
            this.downloadQueue1.Name = "downloadQueue1";
            this.downloadQueue1.Size = new System.Drawing.Size(533, 365);
            this.downloadQueue1.TabIndex = 0;
            // 
            // DownloadMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 410);
            this.Controls.Add(this.splitContainer1);
            this.Name = "DownloadMonitor";
            this.Text = "Download Monitor";
            this.Load += new System.EventHandler(this.DownloadMonitor_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnClearQueue;
        private System.Windows.Forms.Button btnClose;
        private DownloadQueue downloadQueue1;

    }
}