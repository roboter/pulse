namespace PulseMassDownloader
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDownloadPath = new System.Windows.Forms.TextBox();
            this.btnFolderBrowser = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.downloadQueue1 = new Pulse.Forms.UI.DownloadQueue();
            this.providerComboBox1 = new Pulse.Forms.UI.ProviderComboBox();
            this.btnClearQueue = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPausePlay = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(18, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(422, 311);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Save Path: ";
            // 
            // txtDownloadPath
            // 
            this.txtDownloadPath.Location = new System.Drawing.Point(134, 33);
            this.txtDownloadPath.Name = "txtDownloadPath";
            this.txtDownloadPath.Size = new System.Drawing.Size(300, 20);
            this.txtDownloadPath.TabIndex = 3;
            // 
            // btnFolderBrowser
            // 
            this.btnFolderBrowser.Location = new System.Drawing.Point(440, 31);
            this.btnFolderBrowser.Name = "btnFolderBrowser";
            this.btnFolderBrowser.Size = new System.Drawing.Size(75, 23);
            this.btnFolderBrowser.TabIndex = 4;
            this.btnFolderBrowser.Text = "&Browse";
            this.btnFolderBrowser.UseVisualStyleBackColor = true;
            this.btnFolderBrowser.Click += new System.EventHandler(this.btnFolderBrowser_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Download Source: ";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(18, 403);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 36);
            this.button1.TabIndex = 6;
            this.button1.Text = "Star the Downloads!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(447, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Download Queue:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Settings: ";
            // 
            // downloadQueue1
            // 
            this.downloadQueue1.Location = new System.Drawing.Point(450, 86);
            this.downloadQueue1.Name = "downloadQueue1";
            this.downloadQueue1.Size = new System.Drawing.Size(390, 311);
            this.downloadQueue1.TabIndex = 8;
            // 
            // providerComboBox1
            // 
            this.providerComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.providerComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.providerComboBox1.FormattingEnabled = true;
            this.providerComboBox1.Location = new System.Drawing.Point(134, 5);
            this.providerComboBox1.Name = "providerComboBox1";
            this.providerComboBox1.Size = new System.Drawing.Size(240, 21);
            this.providerComboBox1.TabIndex = 0;
            this.providerComboBox1.SelectedIndexChanged += new System.EventHandler(this.providerComboBox1_SelectedIndexChanged);
            // 
            // btnClearQueue
            // 
            this.btnClearQueue.Image = ((System.Drawing.Image)(resources.GetObject("btnClearQueue.Image")));
            this.btnClearQueue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearQueue.Location = new System.Drawing.Point(450, 407);
            this.btnClearQueue.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearQueue.Name = "btnClearQueue";
            this.btnClearQueue.Size = new System.Drawing.Size(112, 34);
            this.btnClearQueue.TabIndex = 10;
            this.btnClearQueue.Text = "Clear Queue";
            this.btnClearQueue.UseVisualStyleBackColor = true;
            this.btnClearQueue.Click += new System.EventHandler(this.btnClearQueue_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(619, 32);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown1.TabIndex = 11;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(521, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Download Count: ";
            // 
            // btnPausePlay
            // 
            this.btnPausePlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPausePlay.Image")));
            this.btnPausePlay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPausePlay.Location = new System.Drawing.Point(567, 407);
            this.btnPausePlay.Margin = new System.Windows.Forms.Padding(2);
            this.btnPausePlay.Name = "btnPausePlay";
            this.btnPausePlay.Size = new System.Drawing.Size(164, 34);
            this.btnPausePlay.TabIndex = 13;
            this.btnPausePlay.Text = "Pause Downloading";
            this.btnPausePlay.UseVisualStyleBackColor = true;
            this.btnPausePlay.Click += new System.EventHandler(this.btnPausePlay_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 452);
            this.Controls.Add(this.btnPausePlay);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnClearQueue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.downloadQueue1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFolderBrowser);
            this.Controls.Add(this.txtDownloadPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.providerComboBox1);
            this.Name = "Form1";
            this.Text = "Pulse Mass Downloader";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Pulse.Forms.UI.ProviderComboBox providerComboBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDownloadPath;
        private System.Windows.Forms.Button btnFolderBrowser;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private Pulse.Forms.UI.DownloadQueue downloadQueue1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClearQueue;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPausePlay;
    }
}

