using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pulse.Base;

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
