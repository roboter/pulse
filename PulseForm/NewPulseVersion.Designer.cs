namespace PulseForm
{
    partial class NewPulseVersion
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblOldVersion = new System.Windows.Forms.Label();
            this.lblNewVersion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cbCheckForNewVersions = new System.Windows.Forms.CheckBox();
            this.rtbDesc = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(264, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "A new version of Pulse is available!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Your Version: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(10, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "New Version: ";
            // 
            // lblOldVersion
            // 
            this.lblOldVersion.AutoSize = true;
            this.lblOldVersion.Location = new System.Drawing.Point(125, 35);
            this.lblOldVersion.Name = "lblOldVersion";
            this.lblOldVersion.Size = new System.Drawing.Size(0, 17);
            this.lblOldVersion.TabIndex = 3;
            // 
            // lblNewVersion
            // 
            this.lblNewVersion.AutoSize = true;
            this.lblNewVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNewVersion.ForeColor = System.Drawing.Color.Blue;
            this.lblNewVersion.Location = new System.Drawing.Point(124, 52);
            this.lblNewVersion.Name = "lblNewVersion";
            this.lblNewVersion.Size = new System.Drawing.Size(0, 17);
            this.lblNewVersion.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(7, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "What\'s new?";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(322, 280);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 37);
            this.button1.TabIndex = 7;
            this.button1.Text = "Download!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(427, 280);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 37);
            this.button2.TabIndex = 8;
            this.button2.Text = "Ignore";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbCheckForNewVersions
            // 
            this.cbCheckForNewVersions.AutoSize = true;
            this.cbCheckForNewVersions.Checked = true;
            this.cbCheckForNewVersions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCheckForNewVersions.Location = new System.Drawing.Point(7, 289);
            this.cbCheckForNewVersions.Name = "cbCheckForNewVersions";
            this.cbCheckForNewVersions.Size = new System.Drawing.Size(176, 21);
            this.cbCheckForNewVersions.TabIndex = 9;
            this.cbCheckForNewVersions.Text = "Check for new versions";
            this.cbCheckForNewVersions.UseVisualStyleBackColor = true;
            this.cbCheckForNewVersions.CheckedChanged += new System.EventHandler(this.cbCheckForNewVersions_CheckedChanged);
            // 
            // rtbDesc
            // 
            this.rtbDesc.Location = new System.Drawing.Point(7, 106);
            this.rtbDesc.Name = "rtbDesc";
            this.rtbDesc.ReadOnly = true;
            this.rtbDesc.Size = new System.Drawing.Size(495, 168);
            this.rtbDesc.TabIndex = 10;
            this.rtbDesc.Text = "";
            // 
            // NewPulseVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 328);
            this.Controls.Add(this.rtbDesc);
            this.Controls.Add(this.cbCheckForNewVersions);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblNewVersion);
            this.Controls.Add(this.lblOldVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewPulseVersion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "New Pulse Version Available";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblOldVersion;
        private System.Windows.Forms.Label lblNewVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox cbCheckForNewVersions;
        private System.Windows.Forms.RichTextBox rtbDesc;
    }
}