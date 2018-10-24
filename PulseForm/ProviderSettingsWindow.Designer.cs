namespace PulseForm
{
    partial class ProviderSettingsWindow
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
            this.scHost = new System.Windows.Forms.SplitContainer();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.scHost.Panel2.SuspendLayout();
            this.scHost.SuspendLayout();
            this.SuspendLayout();
            // 
            // scHost
            // 
            this.scHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scHost.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scHost.IsSplitterFixed = true;
            this.scHost.Location = new System.Drawing.Point(0, 0);
            this.scHost.Name = "scHost";
            this.scHost.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scHost.Panel1
            // 
            this.scHost.Panel1.AutoScroll = true;
            // 
            // scHost.Panel2
            // 
            this.scHost.Panel2.Controls.Add(this.btnCancel);
            this.scHost.Panel2.Controls.Add(this.btnOK);
            this.scHost.Panel2MinSize = 50;
            this.scHost.Size = new System.Drawing.Size(633, 524);
            this.scHost.SplitterDistance = 470;
            this.scHost.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(427, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 35);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(523, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ProviderSettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 524);
            this.Controls.Add(this.scHost);
            this.Name = "ProviderSettingsWindow";
            this.Text = "ProviderSettingsWindow";
            this.scHost.Panel2.ResumeLayout(false);
            this.scHost.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scHost;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}