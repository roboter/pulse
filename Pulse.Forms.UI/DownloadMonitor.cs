using System;
using System.Windows.Forms;

namespace Pulse.Forms.UI
{
    public partial class DownloadMonitor : Form
    {

        public DownloadMonitor()
        {
            InitializeComponent();
        }

        private void DownloadMonitor_Load(object sender, EventArgs e)
        {
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClearQueue_Click(object sender, EventArgs e)
        {
            downloadQueue1.ClearQueue();
        }
    }
}
