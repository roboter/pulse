using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pulse.Base;
using System.IO;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Reflection;
using Microsoft.Win32;
using System.Diagnostics;

namespace PulseForm
{
    public partial class frmPulseOptions : Form
    {
        public List<ActiveProviderInfo> OutputProviderInfos
        {
            get { return _OutputProviderInfos; }
        }

        public List<ActiveProviderInfo> InputProviderInfos
        {
            get { return _InputProviderInfos; }
        } 

        public event EventHandler UpdateSettings;
        private readonly List<string> langCodes = new List<string>();

        List<ActiveProviderInfo> _OutputProviderInfos = new List<ActiveProviderInfo>();
        List<ActiveProviderInfo> _InputProviderInfos = new List<ActiveProviderInfo>();
        List<Guid> _ProvidersToRemove = new List<Guid>();
        string _cachePath = string.Empty;

        public frmPulseOptions()
        {
            InitializeComponent();
            cbUpdateFrequencyUnit.DataSource = Enum.GetValues(typeof(Settings.IntervalUnits));

        }

        private void frmPulseOptions_Load(object sender, EventArgs e)
        {
            var fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            BuildTag.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            _cachePath = Settings.CurrentSettings.CachePath;
            cbDownloadAutomatically.Checked = Settings.CurrentSettings.ChangeOnTimer;
            cbAutoChangeonStartup.Checked = Settings.CurrentSettings.DownloadOnAppStartup;
            udInterval.Value = Settings.CurrentSettings.RefreshInterval;
            cbUpdateFrequencyUnit.SelectedItem = Settings.CurrentSettings.IntervalUnit;
            nudMaxPictureCount.Value = Settings.CurrentSettings.MaxPictureDownloadCount;
            cbPrefetch.Checked = Settings.CurrentSettings.PreFetch;

            cbRunOnWindowsStartup.Checked = Settings.CurrentSettings.RunOnWindowsStartup;
            cbCheckForNewVersions.Checked = Settings.CurrentSettings.CheckForNewPulseVersions;
            cbDisableInFullScreen.Checked = Settings.CurrentSettings.SkipChangeIfFullScreen;
            //frequency

            //LanguageComboBox.Items.Add(new ComboBoxItem() { Content = CultureInfo.GetCultureInfo("en-US").NativeName });
            //langCodes.Add("en-US");
            //var langs = from x in Directory.GetDirectories(Settings.Path) where x.Contains("-") select System.IO.Path.GetFileNameWithoutExtension(x);
            //foreach (var l in langs)
            //{
            //    try
            //    {
            //        var c = CultureInfo.GetCultureInfo(l);
            //        langCodes.Add(c.Name);
            //        LanguageComboBox.Items.Add(new ComboBoxItem() { Content = c.NativeName });
            //    }
            //    catch { }
            //}

            //LanguageComboBox.Text = CultureInfo.GetCultureInfo(Settings.CurrentSettings.Language).NativeName;

            cbDeleteOldFiles.Checked = Settings.CurrentSettings.ClearOldPics;
            nudTempAge.Value = Settings.CurrentSettings.ClearInterval;
            
            //input providers
            var inputProviders = ProviderManager.Instance.GetProvidersByType<IInputProvider>();

            _InputProviderInfos.AddRange(
                from c in Settings.CurrentSettings.ProviderSettings
                let api = c.Value
                let provName = api.ProviderName
                where inputProviders.ContainsKey(provName)
                select new ActiveProviderInfo(provName, api.ProviderInstanceID) { 
                        Active = api.Active,
                        AsyncOK = api.AsyncOK,
                        ExecutionOrder = api.ExecutionOrder,
                        ProviderConfig = api.ProviderConfig,
                        ProviderLabel = api.ProviderLabel
                    }
                );

            //BindingSource bsInput = new BindingSource();
            //bsInput.DataSource = InputProviderInfos;
            BindProviderListView();
            
            //load into combo box
            BindingSource bs = new BindingSource();
            bs.DataSource = (from c in inputProviders select c.Key).ToList();
            cbProviders.DataSource = bs;

            //output providers
            foreach (var op in ProviderManager.Instance.GetProvidersByType<IOutputProvider>())
            {
                var apiList = from c in Settings.CurrentSettings.ProviderSettings
                              where c.Value.ProviderName == op.Key
                              select c;

                if (apiList.Count() > 0)
                {
                    var ep = apiList.First().Value;

                    _OutputProviderInfos.Add(new ActiveProviderInfo()
                    {
                        Active = ep.Active,
                        AsyncOK = ep.AsyncOK,
                        ExecutionOrder = ep.ExecutionOrder,
                        ProviderConfig = ep.ProviderConfig,
                        ProviderName = ep.ProviderName,
                        ProviderInstanceID = ep.ProviderInstanceID
                    });
                }
                else
                {
                    _OutputProviderInfos.Add(new ActiveProviderInfo(op.Key));
                }
            }

            //sort
            SortOutputProviders();

            dgvOutputProviders.DataSource = OutputProviderInfos;

            ApplyButton.Enabled = false;
        }

        private void BindProviderListView()
        {
            lbActiveInputProviders.Items.Clear();

            ImageList imgList = new ImageList();
            lbActiveInputProviders.SmallImageList = imgList;

            foreach (var c in InputProviderInfos)
            {
                if (!imgList.Images.ContainsKey(c.ProviderName))
                    imgList.Images.Add(c.ProviderName, ProviderManager.Instance.GetProviderIcon(c.ProviderName));

                ListViewItem lvi = new ListViewItem(c.ProviderLabel, c.ProviderName);
                lvi.SubItems.Add(c.ProviderInstanceID.ToString());
                lvi.Checked = c.Active;
                lbActiveInputProviders.Items.Add(lvi);
            }
        }

        private void SortOutputProviders()
        {
            _OutputProviderInfos = new List<ActiveProviderInfo>(
                _OutputProviderInfos.OrderBy(x => x.Active)
                .ThenBy(x => x.ExecutionOrder)
                .ThenBy(x => x.ProviderName));
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            ApplySettings();

            this.Close();
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ApplySettings()
        {
            if (Settings.CurrentSettings.CachePath != _cachePath)
            {
                Settings.CurrentSettings.CachePath = _cachePath;
                MessageBox.Show("You have changed the Cache folder.  Please restart Pulse for this change to take effect.", "Pulse", MessageBoxButtons.OK);
            }
            Settings.CurrentSettings.ChangeOnTimer = cbDownloadAutomatically.Checked;
            Settings.CurrentSettings.DownloadOnAppStartup = cbAutoChangeonStartup.Checked;
            Settings.CurrentSettings.ClearOldPics = cbDeleteOldFiles.Checked;
            Settings.CurrentSettings.ClearInterval = Convert.ToInt32(nudTempAge.Value);
            Settings.CurrentSettings.MaxPictureDownloadCount = Convert.ToInt32(nudMaxPictureCount.Value);
            //set pre-fetch flag
            Settings.CurrentSettings.PreFetch = cbPrefetch.Checked;


            Settings.CurrentSettings.RefreshInterval = (int)udInterval.Value;
            Settings.CurrentSettings.IntervalUnit = (Settings.IntervalUnits)cbUpdateFrequencyUnit.SelectedValue;

            //detect change in Run on Windows startup
            if (Settings.CurrentSettings.RunOnWindowsStartup != cbRunOnWindowsStartup.Checked)
            {
                Settings.CurrentSettings.RunOnWindowsStartup = cbRunOnWindowsStartup.Checked;
                //update registry key
                RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (Settings.CurrentSettings.RunOnWindowsStartup)
                {
                    rkApp.SetValue("Pulse", Application.ExecutablePath.ToString());
                }
                else
                {
                    rkApp.DeleteValue("Pulse", false);
                }
            }

            //check for new versions on startup?
            Settings.CurrentSettings.CheckForNewPulseVersions = cbCheckForNewVersions.Checked;

            Settings.CurrentSettings.SkipChangeIfFullScreen = cbDisableInFullScreen.Checked;
            
            //save inputs
            foreach (ActiveProviderInfo api in InputProviderInfos)
            {
                var index = InputProviderInfos.IndexOf(api);
                api.Active = lbActiveInputProviders.Items[index].Checked;
                Settings.CurrentSettings.ProviderSettings[api.ProviderInstanceID] = api;
            }
            //remove removed inputs
            foreach (Guid g in _ProvidersToRemove)
            {
                if (Settings.CurrentSettings.ProviderSettings.ContainsKey(g))
                {
                    Settings.CurrentSettings.ProviderSettings.Remove(g);
                }
            }

            //save output providers
            foreach (ActiveProviderInfo api in OutputProviderInfos)
            {
                if (Settings.CurrentSettings.ProviderSettings.ContainsKey(api.ProviderInstanceID))
                {
                    if (Settings.CurrentSettings.ProviderSettings[api.ProviderInstanceID].GetComparisonHashCode() != api.GetComparisonHashCode())
                    {
                        //since they are different check if we deactivated or just changed a setting
                        if (api.Active)
                        {
                            Settings.CurrentSettings.ProviderSettings[api.ProviderInstanceID] = api;
                        }
                        else
                        {
                            //validate that active state has changed, if it has then deactivate
                            if (Settings.CurrentSettings.ProviderSettings[api.ProviderInstanceID].Active != api.Active)
                            {
                                Settings.CurrentSettings.ProviderSettings[api.ProviderInstanceID].Instance.Deactivate(null);
                            }

                            Settings.CurrentSettings.ProviderSettings.Remove(api.ProviderInstanceID);
                        }
                    }
                }
                else if (api.Active)
                {
                    Settings.CurrentSettings.ProviderSettings.Add(api.ProviderInstanceID, api);

                    //activate new provider
                    api.Instance.Activate(null);
                }
            }

            //save config file
            Settings.CurrentSettings.Save(Settings.AppPath + "\\settings.conf");

            if (UpdateSettings != null)
            {
                UpdateSettings(null, EventArgs.Empty);
            }
        }

        private void OutputProvidersCheckBox_Click(object sender, EventArgs e)
        {
            ApplyButton.Enabled = true;
        }

        private void CheckBoxClick(object sender, EventArgs e)
        {
            ApplyButton.Enabled = true;
        }

        private void ComboBoxSelectionChanged(object sender, EventArgs e)
        {
            ApplyButton.Enabled = true;
        }

        private void lbActiveInputProviders_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            HandleProviderSettingsEnableAndLoad();
        }

        private void btnRemoveInputProvider_Click(object sender, EventArgs e)
        {
            if (lbActiveInputProviders.SelectedItems.Count <= 0) return;

            ApplyButton.Enabled = true;

            ListViewItem lvi = lbActiveInputProviders.SelectedItems[0];
            ActiveProviderInfo api = InputProviderInfos.Where(x => x.ProviderInstanceID == new Guid(lvi.SubItems[1].Text)).SingleOrDefault();

            InputProviderInfos.Remove(api);

            _ProvidersToRemove.Add(api.ProviderInstanceID);

            BindingSource bsInput = new BindingSource();
            bsInput.DataSource = InputProviderInfos;

            BindProviderListView();
        }

        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbActiveInputProviders.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lbActiveInputProviders.SelectedItems[0];
                ActiveProviderInfo api = InputProviderInfos.Where(x => x.ProviderInstanceID == new Guid(lvi.SubItems[1].Text)).SingleOrDefault();

                ActiveProviderInfo apiDup = new ActiveProviderInfo(api.ProviderName) { Active=true, ProviderConfig=api.ProviderConfig };
                InputProviderInfos.Add(apiDup);

                BindProviderListView();
            }
        }

        private void lbActiveInputProviders_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (lbActiveInputProviders.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lbActiveInputProviders.SelectedItems[0];
                ActiveProviderInfo api = InputProviderInfos.Where(x => x.ProviderInstanceID == new Guid(lvi.SubItems[1].Text)).SingleOrDefault();

                api.ProviderLabel = e.Label;

                this.ApplyButton.Enabled = true;

                BindProviderListView();
            }
        }

        private void HandleProviderSettingsEnableAndLoad()
        {
            bool result = false;

            if (lbActiveInputProviders.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lbActiveInputProviders.SelectedItems[0];
                ActiveProviderInfo api = InputProviderInfos.Where(x => x.ProviderInstanceID == new Guid(lvi.SubItems[1].Text)).SingleOrDefault();


                btnPreview.Enabled = api != null;
                btnRemoveInputProvider.Enabled = api != null;
                ctxProviders.Enabled = api != null;

                if (api != null)
                {
                    result = ProviderManager.Instance.HasConfigurationWindow(api.ProviderName) != null;
                }
            }
            else
            {
                btnPreview.Enabled = false;
                btnRemoveInputProvider.Enabled = false;
                ctxProviders.Enabled = false;
            }

            //enable or disable settings button depending on settings availability
            ProviderSettings.Enabled = result;
        }

        private void ClearNowButtonClick(object sender, EventArgs e)
        {
            if (Directory.Exists(Settings.AppPath + "\\Cache"))
            {
                foreach (var f in Directory.GetFiles(Settings.AppPath + "\\Cache"))
                {
                    try
                    {
                        File.Delete(f);
                    }
                    catch { }
                }
            }

            MessageBox.Show("All cached items have been cleared.");
        }

        private void ProviderSettings_Click(object sender, EventArgs e)
        {
            if (lbActiveInputProviders.SelectedItems.Count <= 0) return;
            ListViewItem lvi = lbActiveInputProviders.SelectedItems[0];

            ActiveProviderInfo api = InputProviderInfos.Where(x => x.ProviderInstanceID == new Guid(lvi.SubItems[1].Text)).SingleOrDefault();
            if (api == null) return;

            //get the usercontrol instance from Provider Manager
            var initSettings = ProviderManager.Instance.InitializeConfigurationWindow(api.ProviderName);
            if (initSettings == null) return;

            HostConfigurationWindow(initSettings, api);
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (lbActiveInputProviders.SelectedItems.Count <= 0) return;
            ListViewItem lvi = lbActiveInputProviders.SelectedItems[0];

            ActiveProviderInfo api = InputProviderInfos.Where(x => x.ProviderInstanceID == new Guid(lvi.SubItems[1].Text)).SingleOrDefault();
            if (api == null) return;

            InputProviderPreview ipp = new InputProviderPreview();
            ipp.Provider = api;

            ipp.ShowDialog();
        }


        private void btnAddInput_Click(object sender, EventArgs e)
        {
            ApplyButton.Enabled = true;
            string name = cbProviders.SelectedItem.ToString();

            ActiveProviderInfo api = new ActiveProviderInfo(name) { Active = true };
            InputProviderInfos.Add(api);

            BindProviderListView();

            //BindingSource bsInput = new BindingSource();
            //bsInput.DataSource = InputProviderInfos;

            //lbActiveInputProviders.DataSource = bsInput;
            //lbActiveInputProviders.SelectedItem = api;
        }


        private void cbProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddInput.Enabled = cbProviders.SelectedItem != null;
        }

        private void HostConfigurationWindow(IProviderConfigurationEditor initSettings, ActiveProviderInfo api)
        {
            //dialog window which will house the user control
            ProviderSettingsWindow psw = new ProviderSettingsWindow();

            Bitmap img = (Bitmap)ProviderManager.GetProviderIcon(api.Instance.GetType());
            if (img != null)
            {
                psw.Icon = System.Drawing.Icon.FromHandle(img.GetHicon());
                img.Dispose();
            }

            //load the usercontrol into the window
            psw.LoadSettings(initSettings, api.ProviderName);

            //get current config string

            //load the configuration info into the user control
            initSettings.LoadConfiguration(api.ProviderConfig);

            //show the dialog box
            psw.ShowDialog();

            //if user clicked OK then call save to keep a copy of settings in memory
            if (initSettings.IsOK)
            {
                api.ProviderConfig = initSettings.SaveConfiguration();
                //activate apply option
                ApplyButton.Enabled = true;
            }
        }

        private void dgvOutputProviders_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ApplyButton.Enabled = true;
        }

        private void dgvOutputProviders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var obj = dgvOutputProviders.CurrentRow.DataBoundItem;

                if (obj == null) return;

                ActiveProviderInfo api = obj as ActiveProviderInfo;

                Type t = ProviderManager.Instance.HasConfigurationWindow(api.ProviderName);

                //check for null
                if (t != null)
                {
                    IProviderConfigurationEditor cw = ProviderManager.InitializeConfigurationWindow(t);

                    HostConfigurationWindow(cw, api);
                }
                else
                {
                    MessageBox.Show(string.Format("Sorry! No settings available for {0}.", api.ProviderName));
                }
            }
            //fix for the Active checkbox not causing the apply button to activate for options
            else if (e.RowIndex >= 0 && e.ColumnIndex > 0)
            {
                ApplyButton.Enabled = true;
            }
        }

        private void cbRunOnWindowsStartup_CheckedChanged(object sender, EventArgs e)
        {
            ApplyButton.Enabled = true;
        }

        private void btnBannedImageReview_Click(object sender, EventArgs e)
        {
            BannedImageReview bir = new BannedImageReview();
            bir.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://pulse.codeplex.com/discussions");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://pulse.codeplex.com/workitem/list/basic");
        }

        private void LbActiveInputProvidersItemCheck(object sender, ItemCheckEventArgs e)
        {
            ApplyButton.Enabled = true;
        }

        private void btnChangeCachePath_Click(object sender, EventArgs e)
        {
            fbdCachePath.SelectedPath = _cachePath;
            if (fbdCachePath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (_cachePath != fbdCachePath.SelectedPath)
                {
                    _cachePath = fbdCachePath.SelectedPath;
                    ApplyButton.Enabled = true;
                }
            }
        }
    }
}
