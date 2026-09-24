using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Pulse.Base;

namespace wallhaven
{
    public partial class WallhavenProviderPrefs : UserControl, IProviderConfigurationEditor
    {
        private WallhavenSearchSettings _settings;

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }

            public ComboBoxItem(string text, string value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        public bool IsOK { get; set; }

        public WallhavenProviderPrefs()
        {
            InitializeComponent();
            IsOK = false;

            InitializeComboBoxes();
            WireEvents();
        }

        private void InitializeComboBoxes()
        {
            // Sorting
            cbSorting.DisplayMember = "Text";
            cbSorting.ValueMember = "Value";
            cbSorting.DataSource = new List<ComboBoxItem>
            {
                new ComboBoxItem("Relevance", "relevance"),
                new ComboBoxItem("Random", "random"),
                new ComboBoxItem("Date Added", "date_added"),
                new ComboBoxItem("Views", "views"),
                new ComboBoxItem("Favorites", "favorites"),
                new ComboBoxItem("Toplist", "toplist")
            };

            // Order
            cbOrder.DisplayMember = "Text";
            cbOrder.ValueMember = "Value";
            cbOrder.DataSource = new List<ComboBoxItem>
            {
                new ComboBoxItem("Descending", "desc"),
                new ComboBoxItem("Ascending", "asc")
            };

            // TopRange
            cbTopRange.DisplayMember = "Text";
            cbTopRange.ValueMember = "Value";
            cbTopRange.DataSource = new List<ComboBoxItem>
            {
                new ComboBoxItem("Last Month (1M)", "1M"),
                new ComboBoxItem("Last 3 Months (3M)", "3M"),
                new ComboBoxItem("Last 6 Months (6M)", "6M"),
                new ComboBoxItem("Last Year (1y)", "1y"),
                new ComboBoxItem("Last Week (1w)", "1w"),
                new ComboBoxItem("Last 3 Days (3d)", "3d"),
                new ComboBoxItem("Last Day (1d)", "1d")
            };

            // Resolution Mode
            cbResolutionMode.DisplayMember = "Text";
            cbResolutionMode.ValueMember = "Value";
            cbResolutionMode.DataSource = new List<ComboBoxItem>
            {
                new ComboBoxItem("At Least", "AtLeast"),
                new ComboBoxItem("Exact", "Exact"),
                new ComboBoxItem("Any", "None")
            };

            // Aspect Ratio
            cbAspectRatio.DisplayMember = "Text";
            cbAspectRatio.ValueMember = "Value";
            cbAspectRatio.DataSource = new List<ComboBoxItem>
            {
                new ComboBoxItem("Any", ""),
                new ComboBoxItem("16:9", "16x9"),
                new ComboBoxItem("16:10", "16x10"),
                new ComboBoxItem("21:9", "21x9"),
                new ComboBoxItem("32:9", "32x9"),
                new ComboBoxItem("48:9", "48x9"),
                new ComboBoxItem("4:3", "4x3"),
                new ComboBoxItem("5:4", "5x4"),
                new ComboBoxItem("9:16", "9x16"),
                new ComboBoxItem("10:16", "10x16"),
                new ComboBoxItem("1:1", "1x1")
            };

            // Color Filter
            cbColor.DisplayMember = "Text";
            cbColor.ValueMember = "Value";
            cbColor.DataSource = new List<ComboBoxItem>
            {
                new ComboBoxItem("Any Color", ""),
                new ComboBoxItem("Black (#000000)", "000000"),
                new ComboBoxItem("Gray (#999999)", "999999"),
                new ComboBoxItem("White (#ffffff)", "ffffff"),
                new ComboBoxItem("Red (#cc0000)", "cc0000"),
                new ComboBoxItem("Dark Red (#660000)", "660000"),
                new ComboBoxItem("Orange (#ff6600)", "ff6600"),
                new ComboBoxItem("Yellow (#ffff00)", "ffff00"),
                new ComboBoxItem("Light Green (#77cc33)", "77cc33"),
                new ComboBoxItem("Dark Green (#336600)", "336600"),
                new ComboBoxItem("Teal (#66cccc)", "66cccc"),
                new ComboBoxItem("Blue (#0066cc)", "0066cc"),
                new ComboBoxItem("Purple (#663399)", "663399"),
                new ComboBoxItem("Pink (#ea4c88)", "ea4c88")
            };
        }

        private void WireEvents()
        {
            btnDetectResolution.Click += (sender, args) =>
            {
                try
                {
                    txtWidth.Text = PictureManager.PrimaryScreenResolution.First.ToString();
                    txtHeight.Text = PictureManager.PrimaryScreenResolution.Second.ToString();
                }
                catch
                {
                    txtWidth.Text = Screen.PrimaryScreen.Bounds.Width.ToString();
                    txtHeight.Text = Screen.PrimaryScreen.Bounds.Height.ToString();
                }
            };

            lnkApiKeyHelp.LinkClicked += (sender, args) =>
            {
                try
                {
                    Process.Start("https://wallhaven.cc/settings/account");
                }
                catch { }
            };

            cbSorting.SelectedIndexChanged += (sender, args) =>
            {
                UpdateTopRangeVisibility();
            };

            cbResolutionMode.SelectedIndexChanged += (sender, args) =>
            {
                bool enabled = cbResolutionMode.SelectedValue != null &&
                               cbResolutionMode.SelectedValue.ToString() != "None";
                txtWidth.Enabled = enabled;
                txtHeight.Enabled = enabled;
                btnDetectResolution.Enabled = enabled;
            };
        }

        private void UpdateTopRangeVisibility()
        {
            bool isToplist = cbSorting.SelectedValue != null &&
                             string.Equals(cbSorting.SelectedValue.ToString(), "toplist", StringComparison.OrdinalIgnoreCase);
            lblTopRange.Visible = isToplist;
            cbTopRange.Visible = isToplist;
        }

        public void LoadConfiguration(string config)
        {
            _settings = string.IsNullOrEmpty(config)
                ? new WallhavenSearchSettings()
                : WallhavenSearchSettings.LoadFromXML(config) ?? new WallhavenSearchSettings();

            txtSearch.Text = _settings.Query;
            txtApiKey.Text = _settings.ApiKey;

            cbGeneral.Checked = _settings.General;
            cbAnime.Checked = _settings.Anime;
            cbPeople.Checked = _settings.People;

            cbSFW.Checked = _settings.SFW;
            cbSketchy.Checked = _settings.Sketchy;
            cbNSFW.Checked = _settings.NSFW;

            SelectComboByValue(cbSorting, _settings.Sorting);
            SelectComboByValue(cbOrder, _settings.Order);
            SelectComboByValue(cbTopRange, _settings.TopRange);
            SelectComboByValue(cbResolutionMode, _settings.ResolutionMode);
            SelectComboByValue(cbAspectRatio, _settings.AspectRatio);
            SelectComboByValue(cbColor, _settings.Color);

            txtWidth.Text = _settings.ImageWidth.ToString();
            txtHeight.Text = _settings.ImageHeight.ToString();

            UpdateTopRangeVisibility();
        }

        public string SaveConfiguration()
        {
            if (_settings == null) _settings = new WallhavenSearchSettings();

            _settings.Query = txtSearch.Text;
            _settings.ApiKey = txtApiKey.Text;

            _settings.General = cbGeneral.Checked;
            _settings.Anime = cbAnime.Checked;
            _settings.People = cbPeople.Checked;

            _settings.SFW = cbSFW.Checked;
            _settings.Sketchy = cbSketchy.Checked;
            _settings.NSFW = cbNSFW.Checked;

            _settings.Sorting = cbSorting.SelectedValue != null ? cbSorting.SelectedValue.ToString() : "relevance";
            _settings.Order = cbOrder.SelectedValue != null ? cbOrder.SelectedValue.ToString() : "desc";
            _settings.TopRange = cbTopRange.SelectedValue != null ? cbTopRange.SelectedValue.ToString() : "1M";
            _settings.ResolutionMode = cbResolutionMode.SelectedValue != null ? cbResolutionMode.SelectedValue.ToString() : "AtLeast";
            _settings.AspectRatio = cbAspectRatio.SelectedValue != null ? cbAspectRatio.SelectedValue.ToString() : "";
            _settings.Color = cbColor.SelectedValue != null ? cbColor.SelectedValue.ToString() : "";

            int w, h;
            _settings.ImageWidth = int.TryParse(txtWidth.Text, out w) ? w : 0;
            _settings.ImageHeight = int.TryParse(txtHeight.Text, out h) ? h : 0;

            return _settings.Save();
        }

        public void HostMe(object parent)
        {
            var control = parent as Control;
            if (control != null)
            {
                Dock = DockStyle.Fill;
                control.Controls.Add(this);
            }
        }

        private static void SelectComboByValue(ComboBox cb, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                if (cb.Items.Count > 0) cb.SelectedIndex = 0;
                return;
            }

            for (int i = 0; i < cb.Items.Count; i++)
            {
                var item = cb.Items[i] as ComboBoxItem;
                if (item != null && string.Equals(item.Value, value, StringComparison.OrdinalIgnoreCase))
                {
                    cb.SelectedIndex = i;
                    return;
                }
            }

            if (cb.Items.Count > 0) cb.SelectedIndex = 0;
        }
    }
}
