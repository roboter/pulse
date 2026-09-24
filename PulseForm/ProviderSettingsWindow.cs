using System;
using System.Windows.Forms;
using Pulse.Base;

namespace PulseForm
{
    public partial class ProviderSettingsWindow : Form
    {
        private IProviderConfigurationEditor _uc;

        public ProviderSettingsWindow()
        {
            InitializeComponent();
        }

        public void LoadSettings(IProviderConfigurationEditor uc, string providerName)
        {
            _uc = uc;

            this.Text = "Provider Settings for " + providerName;
            _uc.HostMe(scHost.Panel1);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _uc.IsOK = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _uc.IsOK = false;
            this.Close();
        }
    }
}
