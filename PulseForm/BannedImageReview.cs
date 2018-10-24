using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pulse.Base;

namespace PulseForm
{
    public partial class BannedImageReview : Form
    {
        public BannedImageReview()
        {
            InitializeComponent();
        }

        private void BannedImageReview_Load(object sender, EventArgs e)
        {
            lbBannedImages.DataSource = Settings.CurrentSettings.BannedImages;
        }

        private void lbBannedImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lbBannedImages.SelectedItem != null)
                pbImagePreview.ImageLocation = lbBannedImages.SelectedItem.ToString();
        }

        private void btnUnban_Click(object sender, EventArgs e)
        {
            //remove from banned images
            Settings.CurrentSettings.BannedImages.Remove(lbBannedImages.SelectedItem.ToString());
            //save settings
            Settings.CurrentSettings.Save(Settings.AppPath + "\\settings.conf");
            //rebind list
            lbBannedImages.DataSource = null;
            lbBannedImages.Items.Clear();
            lbBannedImages.DataSource = Settings.CurrentSettings.BannedImages;
            //clear image
            pbImagePreview.Image = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
