using System;
using System.Windows.Forms;

namespace GoogleImages
{
    public partial class GoogleImageProviderPreferences : UserControl, Pulse.Base.IProviderConfigurationEditor
    {
        GoogleImageSearchSettings _giss;

        public GoogleImageProviderPreferences()
        {
            InitializeComponent();
            IsOK = false;
        }

        private void GoogleImageProviderPreferences_Load(object sender, EventArgs e)
        {
            //bind colors to colors combobox on load
            var colors = GoogleImageSearchSettings.GoogleImageColors.GetColors();
            comboBox1.DataSource = colors;
        }

        public void LoadConfiguration(string config)
        {
            //fresh config
            _giss = string.IsNullOrEmpty(config) ? new GoogleImageSearchSettings() : GoogleImageSearchSettings.LoadFromXML(config);

            comboBox1.SelectedValue = _giss.Color;
            txtHeight.Text = _giss.ImageHeight.ToString();
            txtWidth.Text = _giss.ImageWidth.ToString();
        }

        public string SaveConfiguration()
        {
            //I should find out if we can do two-way binding on these properties to make this cleaner...
            _giss.Color = comboBox1.SelectedValue != null ? comboBox1.SelectedValue.ToString() : "";
            _giss.ImageWidth = Convert.ToInt32(txtWidth.Text);
            _giss.ImageHeight = Convert.ToInt32(txtHeight.Text);

            var s = _giss.Save();

            return s;
        }

        public bool IsOK { get; set; }


        public void HostMe(object parent)
        {
            var c = parent as Control;

            if (c != null) c.Controls.Add(this);
        }
    }
}
