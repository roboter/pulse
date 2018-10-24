namespace PulseForm
{
    partial class BannedImageReview
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
            this.lbBannedImages = new System.Windows.Forms.ListBox();
            this.btnUnban = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbImagePreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // lbBannedImages
            // 
            this.lbBannedImages.FormattingEnabled = true;
            this.lbBannedImages.ItemHeight = 16;
            this.lbBannedImages.Location = new System.Drawing.Point(13, 13);
            this.lbBannedImages.Name = "lbBannedImages";
            this.lbBannedImages.Size = new System.Drawing.Size(706, 84);
            this.lbBannedImages.TabIndex = 0;
            this.lbBannedImages.SelectedIndexChanged += new System.EventHandler(this.lbBannedImages_SelectedIndexChanged);
            // 
            // btnUnban
            // 
            this.btnUnban.Location = new System.Drawing.Point(521, 482);
            this.btnUnban.Name = "btnUnban";
            this.btnUnban.Size = new System.Drawing.Size(111, 28);
            this.btnUnban.TabIndex = 1;
            this.btnUnban.Text = "Unban Image";
            this.btnUnban.UseVisualStyleBackColor = true;
            this.btnUnban.Click += new System.EventHandler(this.btnUnban_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(638, 482);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbImagePreview
            // 
            this.pbImagePreview.Location = new System.Drawing.Point(13, 104);
            this.pbImagePreview.Name = "pbImagePreview";
            this.pbImagePreview.Size = new System.Drawing.Size(706, 372);
            this.pbImagePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImagePreview.TabIndex = 3;
            this.pbImagePreview.TabStop = false;
            // 
            // BannedImageReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 521);
            this.Controls.Add(this.pbImagePreview);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUnban);
            this.Controls.Add(this.lbBannedImages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "BannedImageReview";
            this.Text = "Banned Image Review";
            this.Load += new System.EventHandler(this.BannedImageReview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImagePreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbBannedImages;
        private System.Windows.Forms.Button btnUnban;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbImagePreview;
    }
}