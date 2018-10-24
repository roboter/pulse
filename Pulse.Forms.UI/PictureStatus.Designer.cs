namespace Pulse.Forms.UI
{
    partial class PictureStatus
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
            this.pbStatus = new System.Windows.Forms.PictureBox();
            this.pbDownloadProgress = new System.Windows.Forms.ProgressBar();
            this.lblFileName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbStatus
            // 
            this.pbStatus.Image = global::Pulse.Forms.UI.Properties.Resources.StatusAnnotations_Stop_16xLG_color;
            this.pbStatus.Location = new System.Drawing.Point(0, 2);
            this.pbStatus.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(16, 16);
            this.pbStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbStatus.TabIndex = 3;
            this.pbStatus.TabStop = false;
            // 
            // pbDownloadProgress
            // 
            this.pbDownloadProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDownloadProgress.Location = new System.Drawing.Point(219, 3);
            this.pbDownloadProgress.Name = "pbDownloadProgress";
            this.pbDownloadProgress.Size = new System.Drawing.Size(333, 14);
            this.pbDownloadProgress.TabIndex = 4;
            // 
            // lblFileName
            // 
            this.lblFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFileName.Location = new System.Drawing.Point(19, 0);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(194, 20);
            this.lblFileName.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pbStatus, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbDownloadProgress, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblFileName, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(555, 20);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // PictureStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PictureStatus";
            this.Size = new System.Drawing.Size(555, 20);
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbStatus;
        private System.Windows.Forms.ProgressBar pbDownloadProgress;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
