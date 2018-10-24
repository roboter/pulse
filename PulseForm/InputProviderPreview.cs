using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Pulse.Base;

namespace PulseForm
{
    public partial class InputProviderPreview : Form
    {
        public ActiveProviderInfo Provider { get; set; }

        public InputProviderPreview()
        {
            InitializeComponent();
        }

        private void InputProviderPreview_Load(object sender, EventArgs e)
        {
            LoadPictures();
        }

        public void LoadPictures()
        {
            IInputProvider iprov = Provider.Instance as IInputProvider;
            PictureList pl = iprov.GetPictures(new PictureSearch()
            {
             SearchProvider = Provider,
             MaxPictureCount=10,
             BannedURLs = Settings.CurrentSettings.BannedImages,
             PreviewOnly=true
            });

            foreach (Picture p in pl.Pictures)
            {
                PictureBox pb = new PictureBox();
                Image i = p.GetThumbnail();

                if (i == null) continue;

                pb.Image = i;
                pb.Width = i.Width;
                pb.Height = i.Height;

                flowLayoutPanel1.Controls.Add(pb);
            }
        }
    }
}
