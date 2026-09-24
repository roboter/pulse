namespace wallhaven
{
    partial class WallhavenProviderPrefs
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.gbQuery = new System.Windows.Forms.GroupBox();
            this.lblQueryHelp = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.gbCategories = new System.Windows.Forms.GroupBox();
            this.cbPeople = new System.Windows.Forms.CheckBox();
            this.cbAnime = new System.Windows.Forms.CheckBox();
            this.cbGeneral = new System.Windows.Forms.CheckBox();
            this.gbPurity = new System.Windows.Forms.GroupBox();
            this.lblNsfwNote = new System.Windows.Forms.Label();
            this.cbNSFW = new System.Windows.Forms.CheckBox();
            this.cbSketchy = new System.Windows.Forms.CheckBox();
            this.cbSFW = new System.Windows.Forms.CheckBox();
            this.gbSorting = new System.Windows.Forms.GroupBox();
            this.lblTopRange = new System.Windows.Forms.Label();
            this.cbTopRange = new System.Windows.Forms.ComboBox();
            this.lblOrder = new System.Windows.Forms.Label();
            this.cbOrder = new System.Windows.Forms.ComboBox();
            this.lblSorting = new System.Windows.Forms.Label();
            this.cbSorting = new System.Windows.Forms.ComboBox();
            this.gbResolution = new System.Windows.Forms.GroupBox();
            this.cbAspectRatio = new System.Windows.Forms.ComboBox();
            this.lblAspectRatio = new System.Windows.Forms.Label();
            this.btnDetectResolution = new System.Windows.Forms.Button();
            this.lblResolutionX = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.cbResolutionMode = new System.Windows.Forms.ComboBox();
            this.lblResolutionMode = new System.Windows.Forms.Label();
            this.gbColor = new System.Windows.Forms.GroupBox();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.gbAuth = new System.Windows.Forms.GroupBox();
            this.lnkApiKeyHelp = new System.Windows.Forms.LinkLabel();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.lblApiKey = new System.Windows.Forms.Label();
            this.gbQuery.SuspendLayout();
            this.gbCategories.SuspendLayout();
            this.gbPurity.SuspendLayout();
            this.gbSorting.SuspendLayout();
            this.gbResolution.SuspendLayout();
            this.gbColor.SuspendLayout();
            this.gbAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbQuery
            // 
            this.gbQuery.Controls.Add(this.lblQueryHelp);
            this.gbQuery.Controls.Add(this.txtSearch);
            this.gbQuery.Location = new System.Drawing.Point(12, 10);
            this.gbQuery.Name = "gbQuery";
            this.gbQuery.Size = new System.Drawing.Size(560, 68);
            this.gbQuery.TabIndex = 0;
            this.gbQuery.TabStop = false;
            this.gbQuery.Text = "Search Keywords / Tags";
            // 
            // lblQueryHelp
            // 
            this.lblQueryHelp.AutoSize = true;
            this.lblQueryHelp.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblQueryHelp.Location = new System.Drawing.Point(12, 45);
            this.lblQueryHelp.Name = "lblQueryHelp";
            this.lblQueryHelp.Size = new System.Drawing.Size(378, 13);
            this.lblQueryHelp.TabIndex = 1;
            this.lblQueryHelp.Text = "Keywords (e.g. nature, space), +tag1 -tag2, @username, or leave empty for top/latest";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(15, 20);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(530, 20);
            this.txtSearch.TabIndex = 0;
            // 
            // gbCategories
            // 
            this.gbCategories.Controls.Add(this.cbPeople);
            this.gbCategories.Controls.Add(this.cbAnime);
            this.gbCategories.Controls.Add(this.cbGeneral);
            this.gbCategories.Location = new System.Drawing.Point(12, 85);
            this.gbCategories.Name = "gbCategories";
            this.gbCategories.Size = new System.Drawing.Size(270, 52);
            this.gbCategories.TabIndex = 1;
            this.gbCategories.TabStop = false;
            this.gbCategories.Text = "Categories";
            // 
            // cbPeople
            // 
            this.cbPeople.AutoSize = true;
            this.cbPeople.Location = new System.Drawing.Point(180, 22);
            this.cbPeople.Name = "cbPeople";
            this.cbPeople.Size = new System.Drawing.Size(59, 17);
            this.cbPeople.TabIndex = 2;
            this.cbPeople.Text = "People";
            this.cbPeople.UseVisualStyleBackColor = true;
            // 
            // cbAnime
            // 
            this.cbAnime.AutoSize = true;
            this.cbAnime.Location = new System.Drawing.Point(100, 22);
            this.cbAnime.Name = "cbAnime";
            this.cbAnime.Size = new System.Drawing.Size(55, 17);
            this.cbAnime.TabIndex = 1;
            this.cbAnime.Text = "Anime";
            this.cbAnime.UseVisualStyleBackColor = true;
            // 
            // cbGeneral
            // 
            this.cbGeneral.AutoSize = true;
            this.cbGeneral.Location = new System.Drawing.Point(15, 22);
            this.cbGeneral.Name = "cbGeneral";
            this.cbGeneral.Size = new System.Drawing.Size(63, 17);
            this.cbGeneral.TabIndex = 0;
            this.cbGeneral.Text = "General";
            this.cbGeneral.UseVisualStyleBackColor = true;
            // 
            // gbPurity
            // 
            this.gbPurity.Controls.Add(this.lblNsfwNote);
            this.gbPurity.Controls.Add(this.cbNSFW);
            this.gbPurity.Controls.Add(this.cbSketchy);
            this.gbPurity.Controls.Add(this.cbSFW);
            this.gbPurity.Location = new System.Drawing.Point(302, 85);
            this.gbPurity.Name = "gbPurity";
            this.gbPurity.Size = new System.Drawing.Size(270, 52);
            this.gbPurity.TabIndex = 2;
            this.gbPurity.TabStop = false;
            this.gbPurity.Text = "Purity";
            // 
            // lblNsfwNote
            // 
            this.lblNsfwNote.AutoSize = true;
            this.lblNsfwNote.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblNsfwNote.Location = new System.Drawing.Point(220, 23);
            this.lblNsfwNote.Name = "lblNsfwNote";
            this.lblNsfwNote.Size = new System.Drawing.Size(43, 13);
            this.lblNsfwNote.TabIndex = 3;
            this.lblNsfwNote.Text = "(*API Key)";
            // 
            // cbNSFW
            // 
            this.cbNSFW.AutoSize = true;
            this.cbNSFW.Location = new System.Drawing.Point(165, 22);
            this.cbNSFW.Name = "cbNSFW";
            this.cbNSFW.Size = new System.Drawing.Size(58, 17);
            this.cbNSFW.TabIndex = 2;
            this.cbNSFW.Text = "NSFW";
            this.cbNSFW.UseVisualStyleBackColor = true;
            // 
            // cbSketchy
            // 
            this.cbSketchy.AutoSize = true;
            this.cbSketchy.Location = new System.Drawing.Point(85, 22);
            this.cbSketchy.Name = "cbSketchy";
            this.cbSketchy.Size = new System.Drawing.Size(65, 17);
            this.cbSketchy.TabIndex = 1;
            this.cbSketchy.Text = "Sketchy";
            this.cbSketchy.UseVisualStyleBackColor = true;
            // 
            // cbSFW
            // 
            this.cbSFW.AutoSize = true;
            this.cbSFW.Location = new System.Drawing.Point(15, 22);
            this.cbSFW.Name = "cbSFW";
            this.cbSFW.Size = new System.Drawing.Size(50, 17);
            this.cbSFW.TabIndex = 0;
            this.cbSFW.Text = "SFW";
            this.cbSFW.UseVisualStyleBackColor = true;
            // 
            // gbSorting
            // 
            this.gbSorting.Controls.Add(this.lblTopRange);
            this.gbSorting.Controls.Add(this.cbTopRange);
            this.gbSorting.Controls.Add(this.lblOrder);
            this.gbSorting.Controls.Add(this.cbOrder);
            this.gbSorting.Controls.Add(this.lblSorting);
            this.gbSorting.Controls.Add(this.cbSorting);
            this.gbSorting.Location = new System.Drawing.Point(12, 145);
            this.gbSorting.Name = "gbSorting";
            this.gbSorting.Size = new System.Drawing.Size(560, 58);
            this.gbSorting.TabIndex = 3;
            this.gbSorting.TabStop = false;
            this.gbSorting.Text = "Sorting & Order";
            // 
            // lblTopRange
            // 
            this.lblTopRange.AutoSize = true;
            this.lblTopRange.Location = new System.Drawing.Point(365, 24);
            this.lblTopRange.Name = "lblTopRange";
            this.lblTopRange.Size = new System.Drawing.Size(64, 13);
            this.lblTopRange.TabIndex = 5;
            this.lblTopRange.Text = "Top Range:";
            // 
            // cbTopRange
            // 
            this.cbTopRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTopRange.FormattingEnabled = true;
            this.cbTopRange.Location = new System.Drawing.Point(435, 21);
            this.cbTopRange.Name = "cbTopRange";
            this.cbTopRange.Size = new System.Drawing.Size(110, 21);
            this.cbTopRange.TabIndex = 4;
            // 
            // lblOrder
            // 
            this.lblOrder.AutoSize = true;
            this.lblOrder.Location = new System.Drawing.Point(195, 24);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(36, 13);
            this.lblOrder.TabIndex = 3;
            this.lblOrder.Text = "Order:";
            // 
            // cbOrder
            // 
            this.cbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrder.FormattingEnabled = true;
            this.cbOrder.Location = new System.Drawing.Point(235, 21);
            this.cbOrder.Name = "cbOrder";
            this.cbOrder.Size = new System.Drawing.Size(100, 21);
            this.cbOrder.TabIndex = 2;
            // 
            // lblSorting
            // 
            this.lblSorting.AutoSize = true;
            this.lblSorting.Location = new System.Drawing.Point(12, 24);
            this.lblSorting.Name = "lblSorting";
            this.lblSorting.Size = new System.Drawing.Size(44, 13);
            this.lblSorting.TabIndex = 1;
            this.lblSorting.Text = "Sort By:";
            // 
            // cbSorting
            // 
            this.cbSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSorting.FormattingEnabled = true;
            this.cbSorting.Location = new System.Drawing.Point(62, 21);
            this.cbSorting.Name = "cbSorting";
            this.cbSorting.Size = new System.Drawing.Size(115, 21);
            this.cbSorting.TabIndex = 0;
            // 
            // gbResolution
            // 
            this.gbResolution.Controls.Add(this.cbAspectRatio);
            this.gbResolution.Controls.Add(this.lblAspectRatio);
            this.gbResolution.Controls.Add(this.btnDetectResolution);
            this.gbResolution.Controls.Add(this.lblResolutionX);
            this.gbResolution.Controls.Add(this.txtHeight);
            this.gbResolution.Controls.Add(this.txtWidth);
            this.gbResolution.Controls.Add(this.cbResolutionMode);
            this.gbResolution.Controls.Add(this.lblResolutionMode);
            this.gbResolution.Location = new System.Drawing.Point(12, 211);
            this.gbResolution.Name = "gbResolution";
            this.gbResolution.Size = new System.Drawing.Size(560, 60);
            this.gbResolution.TabIndex = 4;
            this.gbResolution.TabStop = false;
            this.gbResolution.Text = "Resolution & Sizing";
            // 
            // cbAspectRatio
            // 
            this.cbAspectRatio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAspectRatio.FormattingEnabled = true;
            this.cbAspectRatio.Location = new System.Drawing.Point(455, 23);
            this.cbAspectRatio.Name = "cbAspectRatio";
            this.cbAspectRatio.Size = new System.Drawing.Size(90, 21);
            this.cbAspectRatio.TabIndex = 7;
            // 
            // lblAspectRatio
            // 
            this.lblAspectRatio.AutoSize = true;
            this.lblAspectRatio.Location = new System.Drawing.Point(415, 26);
            this.lblAspectRatio.Name = "lblAspectRatio";
            this.lblAspectRatio.Size = new System.Drawing.Size(35, 13);
            this.lblAspectRatio.TabIndex = 6;
            this.lblAspectRatio.Text = "Ratio:";
            // 
            // btnDetectResolution
            // 
            this.btnDetectResolution.Location = new System.Drawing.Point(315, 22);
            this.btnDetectResolution.Name = "btnDetectResolution";
            this.btnDetectResolution.Size = new System.Drawing.Size(85, 23);
            this.btnDetectResolution.TabIndex = 5;
            this.btnDetectResolution.Text = "Screen Res";
            this.btnDetectResolution.UseVisualStyleBackColor = true;
            // 
            // lblResolutionX
            // 
            this.lblResolutionX.AutoSize = true;
            this.lblResolutionX.Location = new System.Drawing.Point(232, 26);
            this.lblResolutionX.Name = "lblResolutionX";
            this.lblResolutionX.Size = new System.Drawing.Size(12, 13);
            this.lblResolutionX.TabIndex = 4;
            this.lblResolutionX.Text = "x";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(248, 23);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(55, 20);
            this.txtHeight.TabIndex = 3;
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(173, 23);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(55, 20);
            this.txtWidth.TabIndex = 2;
            // 
            // cbResolutionMode
            // 
            this.cbResolutionMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResolutionMode.FormattingEnabled = true;
            this.cbResolutionMode.Location = new System.Drawing.Point(55, 23);
            this.cbResolutionMode.Name = "cbResolutionMode";
            this.cbResolutionMode.Size = new System.Drawing.Size(105, 21);
            this.cbResolutionMode.TabIndex = 1;
            // 
            // lblResolutionMode
            // 
            this.lblResolutionMode.AutoSize = true;
            this.lblResolutionMode.Location = new System.Drawing.Point(12, 26);
            this.lblResolutionMode.Name = "lblResolutionMode";
            this.lblResolutionMode.Size = new System.Drawing.Size(37, 13);
            this.lblResolutionMode.TabIndex = 0;
            this.lblResolutionMode.Text = "Mode:";
            // 
            // gbColor
            // 
            this.gbColor.Controls.Add(this.cbColor);
            this.gbColor.Controls.Add(this.lblColor);
            this.gbColor.Location = new System.Drawing.Point(12, 279);
            this.gbColor.Name = "gbColor";
            this.gbColor.Size = new System.Drawing.Size(560, 52);
            this.gbColor.TabIndex = 5;
            this.gbColor.TabStop = false;
            this.gbColor.Text = "Color Filter";
            // 
            // cbColor
            // 
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Location = new System.Drawing.Point(55, 20);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(180, 21);
            this.cbColor.TabIndex = 1;
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(12, 23);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(34, 13);
            this.lblColor.TabIndex = 0;
            this.lblColor.Text = "Color:";
            // 
            // gbAuth
            // 
            this.gbAuth.Controls.Add(this.lnkApiKeyHelp);
            this.gbAuth.Controls.Add(this.txtApiKey);
            this.gbAuth.Controls.Add(this.lblApiKey);
            this.gbAuth.Location = new System.Drawing.Point(12, 339);
            this.gbAuth.Name = "gbAuth";
            this.gbAuth.Size = new System.Drawing.Size(560, 58);
            this.gbAuth.TabIndex = 6;
            this.gbAuth.TabStop = false;
            this.gbAuth.Text = "Authentication (Optional)";
            // 
            // lnkApiKeyHelp
            // 
            this.lnkApiKeyHelp.AutoSize = true;
            this.lnkApiKeyHelp.Location = new System.Drawing.Point(365, 23);
            this.lnkApiKeyHelp.Name = "lnkApiKeyHelp";
            this.lnkApiKeyHelp.Size = new System.Drawing.Size(149, 13);
            this.lnkApiKeyHelp.TabIndex = 2;
            this.lnkApiKeyHelp.TabStop = true;
            this.lnkApiKeyHelp.Text = "Get your API key on wallhaven";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(70, 20);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(280, 20);
            this.txtApiKey.TabIndex = 1;
            this.txtApiKey.UseSystemPasswordChar = true;
            // 
            // lblApiKey
            // 
            this.lblApiKey.AutoSize = true;
            this.lblApiKey.Location = new System.Drawing.Point(12, 23);
            this.lblApiKey.Name = "lblApiKey";
            this.lblApiKey.Size = new System.Drawing.Size(48, 13);
            this.lblApiKey.TabIndex = 0;
            this.lblApiKey.Text = "API Key:";
            // 
            // WallhavenProviderPrefs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbAuth);
            this.Controls.Add(this.gbColor);
            this.Controls.Add(this.gbResolution);
            this.Controls.Add(this.gbSorting);
            this.Controls.Add(this.gbPurity);
            this.Controls.Add(this.gbCategories);
            this.Controls.Add(this.gbQuery);
            this.Name = "WallhavenProviderPrefs";
            this.Size = new System.Drawing.Size(585, 410);
            this.gbQuery.ResumeLayout(false);
            this.gbQuery.PerformLayout();
            this.gbCategories.ResumeLayout(false);
            this.gbCategories.PerformLayout();
            this.gbPurity.ResumeLayout(false);
            this.gbPurity.PerformLayout();
            this.gbSorting.ResumeLayout(false);
            this.gbSorting.PerformLayout();
            this.gbResolution.ResumeLayout(false);
            this.gbResolution.PerformLayout();
            this.gbColor.ResumeLayout(false);
            this.gbColor.PerformLayout();
            this.gbAuth.ResumeLayout(false);
            this.gbAuth.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbQuery;
        private System.Windows.Forms.Label lblQueryHelp;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.GroupBox gbCategories;
        private System.Windows.Forms.CheckBox cbPeople;
        private System.Windows.Forms.CheckBox cbAnime;
        private System.Windows.Forms.CheckBox cbGeneral;
        private System.Windows.Forms.GroupBox gbPurity;
        private System.Windows.Forms.Label lblNsfwNote;
        private System.Windows.Forms.CheckBox cbNSFW;
        private System.Windows.Forms.CheckBox cbSketchy;
        private System.Windows.Forms.CheckBox cbSFW;
        private System.Windows.Forms.GroupBox gbSorting;
        private System.Windows.Forms.Label lblTopRange;
        private System.Windows.Forms.ComboBox cbTopRange;
        private System.Windows.Forms.Label lblOrder;
        private System.Windows.Forms.ComboBox cbOrder;
        private System.Windows.Forms.Label lblSorting;
        private System.Windows.Forms.ComboBox cbSorting;
        private System.Windows.Forms.GroupBox gbResolution;
        private System.Windows.Forms.ComboBox cbAspectRatio;
        private System.Windows.Forms.Label lblAspectRatio;
        private System.Windows.Forms.Button btnDetectResolution;
        private System.Windows.Forms.Label lblResolutionX;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.ComboBox cbResolutionMode;
        private System.Windows.Forms.Label lblResolutionMode;
        private System.Windows.Forms.GroupBox gbColor;
        private System.Windows.Forms.ComboBox cbColor;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.GroupBox gbAuth;
        private System.Windows.Forms.LinkLabel lnkApiKeyHelp;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Label lblApiKey;
    }
}
