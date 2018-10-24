using System;using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace wallbase
{
    public partial class WallbaseProviderPrefs : UserControl, Pulse.Base.IProviderConfigurationEditor
    {

        WallbaseImageSearchSettings wiss = null;

        public WallbaseProviderPrefs()
        {
            InitializeComponent();

            cbArea.DataSource = WallbaseImageSearchSettings.SearchArea.GetSearchAreas();
            cbImageSizeType.DataSource = WallbaseImageSearchSettings.SizingOption.GetDirectionList();
            cbOrderBy.DataSource = WallbaseImageSearchSettings.OrderBy.GetOrderByList();
            cbOrderByDirection.DataSource = WallbaseImageSearchSettings.OrderByDirection.GetDirectionList();
            //cbTopTimespan.DataSource = WallbaseImageSearchSettings.TopTimeSpan.GetTimespanList();
            cbAspectRatio.DataSource = WallbaseImageSearchSettings.AspectRatio.GetAspectRatioList();
        }

        public void LoadConfiguration(string config)
        {
            try
            {   if(!string.IsNullOrEmpty(config))
                    wiss = WallbaseImageSearchSettings.LoadFromXML(config);
            }
            catch { }

            if (wiss == null)
                wiss = new WallbaseImageSearchSettings();

            txtUserID.Text = wiss.Username;
            txtPassword.Text = wiss.Password;

            txtSearch.Text = wiss.Query;

            cbArea.SelectedValue = wiss.SA;

            cbWG.Checked = wiss.WG;
            cbW.Checked = wiss.W;
            cbHR.Checked = wiss.HR;

            cbSFW.Checked = wiss.SFW;
            cbSketchy.Checked = wiss.SKETCHY;
            cbNSFW.Checked = wiss.NSFW;

            cbImageSizeType.SelectedValue = wiss.SO;
            cbOrderBy.SelectedValue = wiss.OB;
            cbOrderByDirection.SelectedValue = wiss.OBD;

            txtWidth.Text = wiss.ImageWidth.ToString();
            txtHeight.Text = wiss.ImageHeight.ToString();
            cbAspectRatio.SelectedValue = wiss.AR;

            txtCollectionID.Text = wiss.CollectionID;
            txtFavoritesID.Text = wiss.FavoriteID;

            if (wiss.Color != System.Drawing.Color.Empty)
            {
                pnlColor.BackColor = wiss.Color;
                cdPicker.Color = wiss.Color;
            }
        }

        public string SaveConfiguration()
        {
            wiss.Username = txtUserID.Text;
            wiss.Password = txtPassword.Text;

            wiss.Query = txtSearch.Text;

            wiss.WG = cbWG.Checked;
            wiss.W = cbW.Checked;
            wiss.HR = cbHR.Checked;

            wiss.SFW = cbSFW.Checked;
            wiss.SKETCHY = cbSketchy.Checked;
            wiss.NSFW = cbNSFW.Checked;

            wiss.SA = cbArea.SelectedValue.ToString();
            wiss.SO = cbImageSizeType.SelectedValue.ToString();
            wiss.OB = cbOrderBy.SelectedValue==null?"":cbOrderBy.SelectedValue.ToString();
            wiss.OBD = cbOrderByDirection.SelectedValue==null?"":cbOrderByDirection.SelectedValue.ToString();

            wiss.ImageWidth = string.IsNullOrEmpty(txtWidth.Text) ? 0 : Convert.ToInt32(txtWidth.Text);
            wiss.ImageHeight = string.IsNullOrEmpty(txtHeight.Text) ? 0 : Convert.ToInt32(txtHeight.Text);
            wiss.AR = cbAspectRatio.SelectedValue==null?"":cbAspectRatio.SelectedValue.ToString();
            
            wiss.CollectionID = txtCollectionID.Text;
            wiss.FavoriteID = txtFavoritesID.Text;

            return wiss.Save();
        }

        private void lbPickColor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cdPicker.ShowDialog() == DialogResult.OK)
            {
                pnlColor.BackColor = cdPicker.Color;
                wiss.Color = cdPicker.Color;
            }
        }

        private void lbClearColor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlColor.BackColor = SystemColors.Control;
            wiss.Color = System.Drawing.Color.Empty;

        }

        private void WallbaseProviderPrefs_Load(object sender, EventArgs e)
        {
            
        }

        public bool IsOK { get; set; }

        public void HostMe(object parent)
        {
            Control c = parent as Control;

            c.Controls.Add(this);
        }

        private void cbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbArea.SelectedValue == null) return;

            string val = cbArea.SelectedValue.ToString();

            if (val == "search")
                tabControl1.SelectedTab = tpSearch;
            else if (val == "toplist")
                tabControl1.SelectedTab = tpSearch;
            else if (val == "user/favorites")
                tabControl1.SelectedTab = tpFavorites;
            else if (val == "user/collection")
                tabControl1.SelectedTab = tpCollections;
        }
    }
}
