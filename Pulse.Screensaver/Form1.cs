using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pulse.Base;
using wallbase;
using System.IO;


namespace Pulse.Screensaver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private PictureList pl = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            WallbaseImageSearchSettings wiss = new WallbaseImageSearchSettings();
            wiss.SA="toplist";


            PictureSearch ps = new PictureSearch();
            ps.SearchProvider = new ActiveProviderInfo("Wallbase") {Active=true, ProviderConfig = wiss.Save()};
            ps.MaxPictureCount = 100;

            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "TEMP");
            Directory.CreateDirectory(path);
            ps.SaveFolder = path;

            IInputProvider p = ps.SearchProvider.Instance as IInputProvider;
            pl = p.GetPictures(ps);

            Timer t = new Timer();
            t.Tick += t_Tick;
            t.Interval = 5000;
            t.Enabled = true;
        }

        int lastindex = 0;

        void t_Tick(object sender, EventArgs e)
        {
            if(lastindex >= pl.Pictures.Count) lastindex = 0;

            PictureDownload pd = DownloadManager.Current.GetPicture(pl.Pictures[lastindex], false);
            pd.PictureDownloaded += pd_PictureDownloaded;
            pd.StartDownload();

            lastindex++;
        }

        void pd_PictureDownloaded(PictureDownload sender)
        {
            pb1.SizeMode = PictureBoxSizeMode.Zoom;
            pb1.ImageLocation = sender.Picture.LocalPath;
        }
    }
}
