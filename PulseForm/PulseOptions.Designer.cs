namespace PulseForm
{
    partial class frmPulseOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPulseOptions));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnClearNow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nudTempAge = new System.Windows.Forms.NumericUpDown();
            this.cbDeleteOldFiles = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbActiveInputProviders = new System.Windows.Forms.ListView();
            this.ctxProviders = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRemoveInputProvider = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnAddInput = new System.Windows.Forms.Button();
            this.cbProviders = new Pulse.Forms.UI.ProviderComboBox();
            this.ProviderSettings = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbDisableInFullScreen = new System.Windows.Forms.CheckBox();
            this.nudMaxPictureCount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBannedImageReview = new System.Windows.Forms.Button();
            this.cbCheckForNewVersions = new System.Windows.Forms.CheckBox();
            this.cbRunOnWindowsStartup = new System.Windows.Forms.CheckBox();
            this.udInterval = new System.Windows.Forms.NumericUpDown();
            this.cbDownloadAutomatically = new System.Windows.Forms.CheckBox();
            this.cbPrefetch = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbAutoChangeonStartup = new System.Windows.Forms.CheckBox();
            this.cbUpdateFrequencyUnit = new System.Windows.Forms.ComboBox();
            this.tpOutputs = new System.Windows.Forms.TabPage();
            this.dgvOutputProviders = new System.Windows.Forms.DataGridView();
            this.oppSettings = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tpAbout = new System.Windows.Forms.TabPage();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BuildTag = new System.Windows.Forms.Label();
            this.ilTabs = new System.Windows.Forms.ImageList(this.components);
            this.ApplyButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fbdCachePath = new System.Windows.Forms.FolderBrowserDialog();
            this.btnChangeCachePath = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTempAge)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.ctxProviders.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxPictureCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udInterval)).BeginInit();
            this.tpOutputs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutputProviders)).BeginInit();
            this.tpAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpGeneral);
            this.tabControl1.Controls.Add(this.tpOutputs);
            this.tabControl1.Controls.Add(this.tpAbout);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.ilTabs;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(587, 274);
            this.tabControl1.TabIndex = 0;
            // 
            // tpGeneral
            // 
            this.tpGeneral.AutoScroll = true;
            this.tpGeneral.Controls.Add(this.groupBox3);
            this.tpGeneral.Controls.Add(this.groupBox2);
            this.tpGeneral.Controls.Add(this.groupBox1);
            this.tpGeneral.ImageIndex = 0;
            this.tpGeneral.Location = new System.Drawing.Point(4, 23);
            this.tpGeneral.Margin = new System.Windows.Forms.Padding(2);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(2);
            this.tpGeneral.Size = new System.Drawing.Size(579, 247);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel4);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(6, 157);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(279, 84);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Temporary Files";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnChangeCachePath);
            this.panel4.Controls.Add(this.btnClearNow);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.nudTempAge);
            this.panel4.Controls.Add(this.cbDeleteOldFiles);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(2, 24);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(275, 58);
            this.panel4.TabIndex = 0;
            // 
            // btnClearNow
            // 
            this.btnClearNow.Image = ((System.Drawing.Image)(resources.GetObject("btnClearNow.Image")));
            this.btnClearNow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearNow.Location = new System.Drawing.Point(5, 29);
            this.btnClearNow.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearNow.Name = "btnClearNow";
            this.btnClearNow.Size = new System.Drawing.Size(98, 21);
            this.btnClearNow.TabIndex = 3;
            this.btnClearNow.Text = "Clear Now";
            this.btnClearNow.UseVisualStyleBackColor = true;
            this.btnClearNow.Click += new System.EventHandler(this.ClearNowButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(203, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "day(s) old";
            // 
            // nudTempAge
            // 
            this.nudTempAge.Location = new System.Drawing.Point(156, 8);
            this.nudTempAge.Margin = new System.Windows.Forms.Padding(2);
            this.nudTempAge.Name = "nudTempAge";
            this.nudTempAge.Size = new System.Drawing.Size(43, 20);
            this.nudTempAge.TabIndex = 1;
            this.nudTempAge.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudTempAge.ValueChanged += new System.EventHandler(this.ComboBoxSelectionChanged);
            // 
            // cbDeleteOldFiles
            // 
            this.cbDeleteOldFiles.AutoSize = true;
            this.cbDeleteOldFiles.Location = new System.Drawing.Point(5, 9);
            this.cbDeleteOldFiles.Margin = new System.Windows.Forms.Padding(2);
            this.cbDeleteOldFiles.Name = "cbDeleteOldFiles";
            this.cbDeleteOldFiles.Size = new System.Drawing.Size(150, 17);
            this.cbDeleteOldFiles.TabIndex = 0;
            this.cbDeleteOldFiles.Text = "Delete pictures older than ";
            this.cbDeleteOldFiles.UseVisualStyleBackColor = true;
            this.cbDeleteOldFiles.CheckedChanged += new System.EventHandler(this.CheckBoxClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(5, 4);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(280, 149);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbActiveInputProviders);
            this.panel3.Controls.Add(this.btnRemoveInputProvider);
            this.panel3.Controls.Add(this.btnPreview);
            this.panel3.Controls.Add(this.btnAddInput);
            this.panel3.Controls.Add(this.cbProviders);
            this.panel3.Controls.Add(this.ProviderSettings);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(2, 24);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(276, 123);
            this.panel3.TabIndex = 4;
            // 
            // lbActiveInputProviders
            // 
            this.lbActiveInputProviders.CheckBoxes = true;
            this.lbActiveInputProviders.ContextMenuStrip = this.ctxProviders;
            this.lbActiveInputProviders.LabelEdit = true;
            this.lbActiveInputProviders.Location = new System.Drawing.Point(7, 24);
            this.lbActiveInputProviders.Margin = new System.Windows.Forms.Padding(2);
            this.lbActiveInputProviders.MultiSelect = false;
            this.lbActiveInputProviders.Name = "lbActiveInputProviders";
            this.lbActiveInputProviders.Size = new System.Drawing.Size(168, 97);
            this.lbActiveInputProviders.TabIndex = 14;
            this.lbActiveInputProviders.UseCompatibleStateImageBehavior = false;
            this.lbActiveInputProviders.View = System.Windows.Forms.View.SmallIcon;
            this.lbActiveInputProviders.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lbActiveInputProviders_AfterLabelEdit);
            this.lbActiveInputProviders.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.LbActiveInputProvidersItemCheck);
            this.lbActiveInputProviders.SelectedIndexChanged += new System.EventHandler(this.lbActiveInputProviders_SelectedIndexChanged_1);
            // 
            // ctxProviders
            // 
            this.ctxProviders.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.duplicateToolStripMenuItem});
            this.ctxProviders.Name = "ctxProviders";
            this.ctxProviders.ShowImageMargin = false;
            this.ctxProviders.Size = new System.Drawing.Size(100, 26);
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.duplicateToolStripMenuItem.Text = "Duplicate";
            this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.duplicateToolStripMenuItem_Click);
            // 
            // btnRemoveInputProvider
            // 
            this.btnRemoveInputProvider.Enabled = false;
            this.btnRemoveInputProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveInputProvider.Image")));
            this.btnRemoveInputProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveInputProvider.Location = new System.Drawing.Point(178, 77);
            this.btnRemoveInputProvider.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoveInputProvider.Name = "btnRemoveInputProvider";
            this.btnRemoveInputProvider.Size = new System.Drawing.Size(96, 21);
            this.btnRemoveInputProvider.TabIndex = 7;
            this.btnRemoveInputProvider.Text = "Remove";
            this.btnRemoveInputProvider.UseVisualStyleBackColor = true;
            this.btnRemoveInputProvider.Click += new System.EventHandler(this.btnRemoveInputProvider_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Enabled = false;
            this.btnPreview.Image = ((System.Drawing.Image)(resources.GetObject("btnPreview.Image")));
            this.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPreview.Location = new System.Drawing.Point(178, 53);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(96, 21);
            this.btnPreview.TabIndex = 6;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnAddInput
            // 
            this.btnAddInput.Enabled = false;
            this.btnAddInput.Image = ((System.Drawing.Image)(resources.GetObject("btnAddInput.Image")));
            this.btnAddInput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddInput.Location = new System.Drawing.Point(178, 3);
            this.btnAddInput.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddInput.Name = "btnAddInput";
            this.btnAddInput.Size = new System.Drawing.Size(96, 21);
            this.btnAddInput.TabIndex = 5;
            this.btnAddInput.Text = "Add";
            this.btnAddInput.UseVisualStyleBackColor = true;
            this.btnAddInput.Click += new System.EventHandler(this.btnAddInput_Click);
            // 
            // cbProviders
            // 
            this.cbProviders.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProviders.FormattingEnabled = true;
            this.cbProviders.Location = new System.Drawing.Point(7, 3);
            this.cbProviders.Margin = new System.Windows.Forms.Padding(2);
            this.cbProviders.Name = "cbProviders";
            this.cbProviders.Size = new System.Drawing.Size(168, 21);
            this.cbProviders.TabIndex = 2;
            this.cbProviders.SelectedIndexChanged += new System.EventHandler(this.cbProviders_SelectedIndexChanged);
            // 
            // ProviderSettings
            // 
            this.ProviderSettings.Enabled = false;
            this.ProviderSettings.Image = ((System.Drawing.Image)(resources.GetObject("ProviderSettings.Image")));
            this.ProviderSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ProviderSettings.Location = new System.Drawing.Point(178, 28);
            this.ProviderSettings.Margin = new System.Windows.Forms.Padding(2);
            this.ProviderSettings.Name = "ProviderSettings";
            this.ProviderSettings.Size = new System.Drawing.Size(96, 21);
            this.ProviderSettings.TabIndex = 3;
            this.ProviderSettings.Text = "Settings";
            this.ProviderSettings.UseVisualStyleBackColor = true;
            this.ProviderSettings.Click += new System.EventHandler(this.ProviderSettings_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(290, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(279, 239);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Behavior";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbDisableInFullScreen);
            this.panel2.Controls.Add(this.nudMaxPictureCount);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnBannedImageReview);
            this.panel2.Controls.Add(this.cbCheckForNewVersions);
            this.panel2.Controls.Add(this.cbRunOnWindowsStartup);
            this.panel2.Controls.Add(this.udInterval);
            this.panel2.Controls.Add(this.cbDownloadAutomatically);
            this.panel2.Controls.Add(this.cbPrefetch);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cbAutoChangeonStartup);
            this.panel2.Controls.Add(this.cbUpdateFrequencyUnit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(2, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(275, 213);
            this.panel2.TabIndex = 11;
            // 
            // cbDisableInFullScreen
            // 
            this.cbDisableInFullScreen.AutoSize = true;
            this.cbDisableInFullScreen.Location = new System.Drawing.Point(2, 192);
            this.cbDisableInFullScreen.Name = "cbDisableInFullScreen";
            this.cbDisableInFullScreen.Size = new System.Drawing.Size(161, 17);
            this.cbDisableInFullScreen.TabIndex = 17;
            this.cbDisableInFullScreen.Text = "Disable if App. in Full Screen";
            this.cbDisableInFullScreen.UseVisualStyleBackColor = true;
            this.cbDisableInFullScreen.CheckedChanged += new System.EventHandler(this.CheckBoxClick);
            // 
            // nudMaxPictureCount
            // 
            this.nudMaxPictureCount.Location = new System.Drawing.Point(131, 80);
            this.nudMaxPictureCount.Margin = new System.Windows.Forms.Padding(2);
            this.nudMaxPictureCount.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudMaxPictureCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudMaxPictureCount.Name = "nudMaxPictureCount";
            this.nudMaxPictureCount.Size = new System.Drawing.Size(44, 20);
            this.nudMaxPictureCount.TabIndex = 16;
            this.nudMaxPictureCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudMaxPictureCount.ValueChanged += new System.EventHandler(this.ComboBoxSelectionChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Max Search Result Count: ";
            // 
            // btnBannedImageReview
            // 
            this.btnBannedImageReview.Location = new System.Drawing.Point(2, 110);
            this.btnBannedImageReview.Margin = new System.Windows.Forms.Padding(2);
            this.btnBannedImageReview.Name = "btnBannedImageReview";
            this.btnBannedImageReview.Size = new System.Drawing.Size(136, 21);
            this.btnBannedImageReview.TabIndex = 14;
            this.btnBannedImageReview.Text = "Review Banned Images";
            this.btnBannedImageReview.UseVisualStyleBackColor = true;
            this.btnBannedImageReview.Click += new System.EventHandler(this.btnBannedImageReview_Click);
            // 
            // cbCheckForNewVersions
            // 
            this.cbCheckForNewVersions.AutoSize = true;
            this.cbCheckForNewVersions.Image = ((System.Drawing.Image)(resources.GetObject("cbCheckForNewVersions.Image")));
            this.cbCheckForNewVersions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbCheckForNewVersions.Location = new System.Drawing.Point(2, 171);
            this.cbCheckForNewVersions.Margin = new System.Windows.Forms.Padding(2);
            this.cbCheckForNewVersions.Name = "cbCheckForNewVersions";
            this.cbCheckForNewVersions.Size = new System.Drawing.Size(203, 17);
            this.cbCheckForNewVersions.TabIndex = 13;
            this.cbCheckForNewVersions.Text = "Check for new versions on startup\r\n";
            this.cbCheckForNewVersions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbCheckForNewVersions.UseVisualStyleBackColor = true;
            this.cbCheckForNewVersions.CheckedChanged += new System.EventHandler(this.CheckBoxClick);
            // 
            // cbRunOnWindowsStartup
            // 
            this.cbRunOnWindowsStartup.AutoSize = true;
            this.cbRunOnWindowsStartup.Location = new System.Drawing.Point(2, 150);
            this.cbRunOnWindowsStartup.Margin = new System.Windows.Forms.Padding(2);
            this.cbRunOnWindowsStartup.Name = "cbRunOnWindowsStartup";
            this.cbRunOnWindowsStartup.Size = new System.Drawing.Size(145, 17);
            this.cbRunOnWindowsStartup.TabIndex = 12;
            this.cbRunOnWindowsStartup.Text = "Run on Windows Startup";
            this.cbRunOnWindowsStartup.UseVisualStyleBackColor = true;
            this.cbRunOnWindowsStartup.CheckedChanged += new System.EventHandler(this.cbRunOnWindowsStartup_CheckedChanged);
            // 
            // udInterval
            // 
            this.udInterval.Location = new System.Drawing.Point(91, 41);
            this.udInterval.Margin = new System.Windows.Forms.Padding(2);
            this.udInterval.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.udInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udInterval.Name = "udInterval";
            this.udInterval.Size = new System.Drawing.Size(43, 20);
            this.udInterval.TabIndex = 11;
            this.udInterval.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udInterval.ValueChanged += new System.EventHandler(this.ComboBoxSelectionChanged);
            // 
            // cbDownloadAutomatically
            // 
            this.cbDownloadAutomatically.AutoSize = true;
            this.cbDownloadAutomatically.Image = ((System.Drawing.Image)(resources.GetObject("cbDownloadAutomatically.Image")));
            this.cbDownloadAutomatically.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbDownloadAutomatically.Location = new System.Drawing.Point(2, 23);
            this.cbDownloadAutomatically.Margin = new System.Windows.Forms.Padding(2);
            this.cbDownloadAutomatically.Name = "cbDownloadAutomatically";
            this.cbDownloadAutomatically.Size = new System.Drawing.Size(159, 17);
            this.cbDownloadAutomatically.TabIndex = 5;
            this.cbDownloadAutomatically.Text = "Change Picture on Timer";
            this.cbDownloadAutomatically.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbDownloadAutomatically.UseVisualStyleBackColor = true;
            this.cbDownloadAutomatically.CheckedChanged += new System.EventHandler(this.CheckBoxClick);
            // 
            // cbPrefetch
            // 
            this.cbPrefetch.AutoSize = true;
            this.cbPrefetch.Image = ((System.Drawing.Image)(resources.GetObject("cbPrefetch.Image")));
            this.cbPrefetch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbPrefetch.Location = new System.Drawing.Point(2, 63);
            this.cbPrefetch.Margin = new System.Windows.Forms.Padding(2);
            this.cbPrefetch.Name = "cbPrefetch";
            this.cbPrefetch.Size = new System.Drawing.Size(125, 17);
            this.cbPrefetch.TabIndex = 10;
            this.cbPrefetch.Text = "Pre Fetch Pictures";
            this.cbPrefetch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbPrefetch.UseVisualStyleBackColor = true;
            this.cbPrefetch.CheckedChanged += new System.EventHandler(this.CheckBoxClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 42);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Every: ";
            // 
            // cbAutoChangeonStartup
            // 
            this.cbAutoChangeonStartup.AutoSize = true;
            this.cbAutoChangeonStartup.Image = ((System.Drawing.Image)(resources.GetObject("cbAutoChangeonStartup.Image")));
            this.cbAutoChangeonStartup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbAutoChangeonStartup.Location = new System.Drawing.Point(2, 2);
            this.cbAutoChangeonStartup.Margin = new System.Windows.Forms.Padding(2);
            this.cbAutoChangeonStartup.Name = "cbAutoChangeonStartup";
            this.cbAutoChangeonStartup.Size = new System.Drawing.Size(167, 17);
            this.cbAutoChangeonStartup.TabIndex = 6;
            this.cbAutoChangeonStartup.Text = "Change Picture on Startup";
            this.cbAutoChangeonStartup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbAutoChangeonStartup.UseVisualStyleBackColor = true;
            this.cbAutoChangeonStartup.CheckedChanged += new System.EventHandler(this.CheckBoxClick);
            // 
            // cbUpdateFrequencyUnit
            // 
            this.cbUpdateFrequencyUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUpdateFrequencyUnit.FormattingEnabled = true;
            this.cbUpdateFrequencyUnit.Location = new System.Drawing.Point(138, 41);
            this.cbUpdateFrequencyUnit.Margin = new System.Windows.Forms.Padding(2);
            this.cbUpdateFrequencyUnit.Name = "cbUpdateFrequencyUnit";
            this.cbUpdateFrequencyUnit.Size = new System.Drawing.Size(82, 21);
            this.cbUpdateFrequencyUnit.TabIndex = 9;
            this.cbUpdateFrequencyUnit.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectionChanged);
            // 
            // tpOutputs
            // 
            this.tpOutputs.AutoScroll = true;
            this.tpOutputs.Controls.Add(this.dgvOutputProviders);
            this.tpOutputs.ImageIndex = 2;
            this.tpOutputs.Location = new System.Drawing.Point(4, 23);
            this.tpOutputs.Margin = new System.Windows.Forms.Padding(2);
            this.tpOutputs.Name = "tpOutputs";
            this.tpOutputs.Padding = new System.Windows.Forms.Padding(2);
            this.tpOutputs.Size = new System.Drawing.Size(579, 247);
            this.tpOutputs.TabIndex = 1;
            this.tpOutputs.Text = "Outputs";
            this.tpOutputs.UseVisualStyleBackColor = true;
            // 
            // dgvOutputProviders
            // 
            this.dgvOutputProviders.AllowUserToAddRows = false;
            this.dgvOutputProviders.AllowUserToDeleteRows = false;
            this.dgvOutputProviders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvOutputProviders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutputProviders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.oppSettings});
            this.dgvOutputProviders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOutputProviders.Location = new System.Drawing.Point(2, 2);
            this.dgvOutputProviders.Margin = new System.Windows.Forms.Padding(2);
            this.dgvOutputProviders.MultiSelect = false;
            this.dgvOutputProviders.Name = "dgvOutputProviders";
            this.dgvOutputProviders.RowHeadersVisible = false;
            this.dgvOutputProviders.RowTemplate.Height = 28;
            this.dgvOutputProviders.Size = new System.Drawing.Size(575, 243);
            this.dgvOutputProviders.TabIndex = 0;
            this.dgvOutputProviders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOutputProviders_CellClick);
            this.dgvOutputProviders.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOutputProviders_CellValueChanged);
            // 
            // oppSettings
            // 
            this.oppSettings.HeaderText = "Settings";
            this.oppSettings.Name = "oppSettings";
            this.oppSettings.Text = "Change";
            this.oppSettings.Width = 51;
            // 
            // tpAbout
            // 
            this.tpAbout.Controls.Add(this.linkLabel2);
            this.tpAbout.Controls.Add(this.pictureBox2);
            this.tpAbout.Controls.Add(this.linkLabel1);
            this.tpAbout.Controls.Add(this.pictureBox1);
            this.tpAbout.Controls.Add(this.label5);
            this.tpAbout.Controls.Add(this.label4);
            this.tpAbout.Controls.Add(this.BuildTag);
            this.tpAbout.ImageIndex = 1;
            this.tpAbout.Location = new System.Drawing.Point(4, 23);
            this.tpAbout.Margin = new System.Windows.Forms.Padding(2);
            this.tpAbout.Name = "tpAbout";
            this.tpAbout.Size = new System.Drawing.Size(579, 247);
            this.tpAbout.TabIndex = 2;
            this.tpAbout.Text = "About";
            this.tpAbout.UseVisualStyleBackColor = true;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(26, 61);
            this.linkLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(101, 13);
            this.linkLabel2.TabIndex = 6;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Pulse Issue Tracker";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(14, 62);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(26, 45);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(65, 13);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Pulse Forum";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(14, 46);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 29);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(300, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Have a comment or find a bug in Pulse? Please come share it!";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 7);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Version: ";
            // 
            // BuildTag
            // 
            this.BuildTag.AutoSize = true;
            this.BuildTag.Location = new System.Drawing.Point(58, 7);
            this.BuildTag.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BuildTag.Name = "BuildTag";
            this.BuildTag.Size = new System.Drawing.Size(0, 13);
            this.BuildTag.TabIndex = 0;
            // 
            // ilTabs
            // 
            this.ilTabs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTabs.ImageStream")));
            this.ilTabs.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTabs.Images.SetKeyName(0, "OptionsHS.png");
            this.ilTabs.Images.SetKeyName(1, "Symbols_Help_and_inclusive_16xLG.png");
            this.ilTabs.Images.SetKeyName(2, "arrow_Up_16xLG.png");
            // 
            // ApplyButton
            // 
            this.ApplyButton.Enabled = false;
            this.ApplyButton.Image = ((System.Drawing.Image)(resources.GetObject("ApplyButton.Image")));
            this.ApplyButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ApplyButton.Location = new System.Drawing.Point(218, 15);
            this.ApplyButton.Margin = new System.Windows.Forms.Padding(2);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(71, 23);
            this.ApplyButton.TabIndex = 1;
            this.ApplyButton.Text = "Save";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(297, 15);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(73, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2MinSize = 45;
            this.splitContainer1.Size = new System.Drawing.Size(587, 322);
            this.splitContainer1.SplitterDistance = 274;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ApplyButton);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(587, 45);
            this.panel1.TabIndex = 4;
            // 
            // btnChangeCachePath
            // 
            this.btnChangeCachePath.Location = new System.Drawing.Point(108, 29);
            this.btnChangeCachePath.Name = "btnChangeCachePath";
            this.btnChangeCachePath.Size = new System.Drawing.Size(134, 21);
            this.btnChangeCachePath.TabIndex = 4;
            this.btnChangeCachePath.Text = "Change Cache Folder";
            this.btnChangeCachePath.UseVisualStyleBackColor = true;
            this.btnChangeCachePath.Click += new System.EventHandler(this.btnChangeCachePath_Click);
            // 
            // frmPulseOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 322);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmPulseOptions";
            this.Text = "Pulse Options";
            this.Load += new System.EventHandler(this.frmPulseOptions_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTempAge)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ctxProviders.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxPictureCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udInterval)).EndInit();
            this.tpOutputs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutputProviders)).EndInit();
            this.tpAbout.ResumeLayout(false);
            this.tpAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.TabPage tpOutputs;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabPage tpAbout;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbPrefetch;
        private System.Windows.Forms.ComboBox cbUpdateFrequencyUnit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbAutoChangeonStartup;
        private System.Windows.Forms.CheckBox cbDownloadAutomatically;
        private System.Windows.Forms.Button ProviderSettings;
        private Pulse.Forms.UI.ProviderComboBox cbProviders;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnClearNow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudTempAge;
        private System.Windows.Forms.CheckBox cbDeleteOldFiles;
        private System.Windows.Forms.NumericUpDown udInterval;
        private System.Windows.Forms.Label BuildTag;
        private System.Windows.Forms.DataGridView dgvOutputProviders;
        private System.Windows.Forms.CheckBox cbRunOnWindowsStartup;
        private System.Windows.Forms.DataGridViewButtonColumn oppSettings;
        private System.Windows.Forms.Button btnAddInput;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnRemoveInputProvider;
        private System.Windows.Forms.ListView lbActiveInputProviders;
        private System.Windows.Forms.CheckBox cbCheckForNewVersions;
        private System.Windows.Forms.Button btnBannedImageReview;
        private System.Windows.Forms.NumericUpDown nudMaxPictureCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList ilTabs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox cbDisableInFullScreen;
        private System.Windows.Forms.ContextMenuStrip ctxProviders;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
        private System.Windows.Forms.Button btnChangeCachePath;
        private System.Windows.Forms.FolderBrowserDialog fbdCachePath;
    }
}