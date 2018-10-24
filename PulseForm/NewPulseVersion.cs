using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PulseForm
{
    public partial class NewPulseVersion : Form
    {
        private CodePlexNewReleaseChecker.Release _release;
        public NewPulseVersion()
        {
            InitializeComponent();
        }

        public void LoadRelease(CodePlexNewReleaseChecker.Release release)
        {
            _release = release;

            //get current version
            string versionDesc = "{0} ({1})";
            string version = string.Format("Pulse {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            lblOldVersion.Text = version;
            lblNewVersion.Text = string.Format(versionDesc, _release.Name, _release.ReleaseDate.Value.ToShortDateString());

            rtbDesc.Text = _release.Description;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //launch download URL
            OpenWebsite(_release.Link);
            this.Close();
        }

        private static void OpenWebsite(string url)
        {
            System.Diagnostics.Process.Start(GetDefaultBrowserPath(), url);
        }

        private static string GetDefaultBrowserPath()
        {
            string key = @"http\shell\open\command";
            Microsoft.Win32.RegistryKey registryKey =
                Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(key, false);
            return ((string)registryKey.GetValue(null, null)).Split('"')[1];
        }

        private void cbCheckForNewVersions_CheckedChanged(object sender, EventArgs e)
        {
            //save setting to settings and to disc
            Pulse.Base.Settings.CurrentSettings.CheckForNewPulseVersions = cbCheckForNewVersions.Checked;
            Pulse.Base.Settings.CurrentSettings.Save(Pulse.Base.Settings.AppPath + "\\settings.conf");
        }
    }
}
