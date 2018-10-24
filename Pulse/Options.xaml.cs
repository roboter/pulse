using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using Pulse.Base;

namespace Pulse
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public ObservableCollection<ActiveProviderInfo> OutputProviderInfos
        {
            get { return _OutputProviderInfos; }
        }

        public event EventHandler UpdateSettings;
        private readonly List<string> langCodes = new List<string>();
        private string _tempProviderConfig = string.Empty;
        ObservableCollection<ActiveProviderInfo> _OutputProviderInfos = new ObservableCollection<ActiveProviderInfo>();

        public Options()
        {
            InitializeComponent();
        }

        private void WindowSourceInitialized(object sender, EventArgs e)
        {
            var fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            BuildTag.Text = Assembly.GetExecutingAssembly().GetName().Version + ".beta." + fileInfo.LastWriteTimeUtc.ToString("yyMMdd-HHmm");

            if (!string.IsNullOrEmpty(App.Settings.Search))
                SearchBox.Text = App.Settings.Search;

            AutoDownloadCheckBox.IsChecked = App.Settings.DownloadAutomatically;
            DownloadOnStartCheckBox.IsChecked = App.Settings.DownloadOnAppStartup;
            if (App.Settings.RefreshInterval != 1)
                RefreshIntervalSlider.Value = App.Settings.RefreshInterval;
            else
                RefreshIntervalSlider.Value = 0;
            PreFetchCheckBox.IsChecked = App.Settings.PreFetch;
            

            LanguageComboBox.Items.Add(new ComboBoxItem() { Content = CultureInfo.GetCultureInfo("en-US").NativeName });
            langCodes.Add("en-US");
            var langs = from x in Directory.GetDirectories(App.Path) where x.Contains("-") select System.IO.Path.GetFileNameWithoutExtension(x);
            foreach (var l in langs)
            {
                try
                {
                    var c = CultureInfo.GetCultureInfo(l);
                    langCodes.Add(c.Name);
                    LanguageComboBox.Items.Add(new ComboBoxItem() { Content = c.NativeName });
                }
                catch { }
            }

            LanguageComboBox.Text = CultureInfo.GetCultureInfo(App.Settings.Language).NativeName;

            ClearCacheCheckBox.IsChecked = App.Settings.ClearOldPics;
            ClearIntervalSlider.Value = App.Settings.ClearInterval;


            //if (Environment.OSVersion.Version.Major < 6 || Environment.OSVersion.Version.Minor < 1)
            //    ChangeLogonBgCheckBox.Visibility = System.Windows.Visibility.Collapsed;
            //ChangeLogonBgCheckBox.IsChecked = App.Settings.ChangeLogonBg;

            //input providers
            var inputProviders = ProviderManager.Instance.GetProvidersByType<IInputProvider>();
            if (inputProviders.Count > 0)
            {
                foreach (var p in inputProviders)
                {
                    ProvidersBox.Items.Add(p.Key);
                }
                ProvidersBox.SelectedValue = App.Settings.Provider;

                //handle settings button enable/disable and loading string from config if it exists
                HandleProviderSettingsEnableAndLoad();
            }

            //output providers
            foreach (var op in ProviderManager.Instance.GetProvidersByType<IOutputProvider>())
            {
                if (App.Settings.ProviderSettings.ContainsKey(op.Key))
                {
                    var ep = App.Settings.ProviderSettings[op.Key];

                    _OutputProviderInfos.Add(new ActiveProviderInfo()
                    {
                        Active = ep.Active,
                        AsyncOK = ep.AsyncOK,
                        ExecutionOrder = ep.ExecutionOrder,
                        ProviderConfig = ep.ProviderConfig,
                        ProviderName = ep.ProviderName
                    });
                }
                else
                {
                    _OutputProviderInfos.Add(new ActiveProviderInfo()
                    {
                        Active = false,
                        AsyncOK = false,
                        ExecutionOrder = 0,
                        ProviderConfig = string.Empty,
                        ProviderName = op.Key
                    });
                }
            }

            //sort
            SortOutputProviders();

            ApplyButton.IsEnabled = false;
        }

        private void SortOutputProviders()
        {
            _OutputProviderInfos = new ObservableCollection<ActiveProviderInfo>(
                _OutputProviderInfos.OrderBy(x => x.Active)
                .ThenBy(x => x.ExecutionOrder)
                .ThenBy(x => x.ProviderName));
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            if (ApplyButton.IsEnabled)
            {
                ApplySettings();
                ApplyButton.IsEnabled = false;
            }
            this.Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ApplyButtonClick(object sender, RoutedEventArgs e)
        {
            ApplySettings();
            ApplyButton.IsEnabled = false;
        }

        private void ApplySettings()
        {
            //if (App.Settings.Search != SearchBox.Text)
            //    App.Translator.Result = string.Empty;
            //check if the search box text == options search box text (keyword(s)), if so use empty string
            // on some sites searching with no query is fine
            App.Settings.Search = SearchBox.Text == Properties.Resources.OptionsSearchBox ? "":SearchBox.Text;
            App.Settings.DownloadAutomatically = (bool)AutoDownloadCheckBox.IsChecked;
            App.Settings.DownloadOnAppStartup = (bool)DownloadOnStartCheckBox.IsChecked;
            App.Settings.ClearOldPics = (bool)ClearCacheCheckBox.IsChecked;
            App.Settings.ClearInterval = (int)ClearIntervalSlider.Value;

            //set pre-fetch flag
            App.Settings.PreFetch = (bool)PreFetchCheckBox.IsChecked;
            
            if (RefreshIntervalSlider.Value > 0)
                App.Settings.RefreshInterval = RefreshIntervalSlider.Value;
            else
                App.Settings.RefreshInterval = 1;

            if (LanguageComboBox.SelectedIndex >= 0)
                App.Settings.Language = langCodes[LanguageComboBox.SelectedIndex];

            //Input provider settings (only if provider changed)
            if (!string.IsNullOrEmpty(ProvidersBox.Text) && App.Settings.Provider != ProvidersBox.Text)
            {
                //deactivate the old provider
                App.Settings.ProviderSettings.Remove(App.Settings.Provider);
                
                //set new provider
                App.Settings.Provider = ProvidersBox.Text;
            
                //initialize the new provider
                App.CurrentProvider = (IInputProvider)ProviderManager.Instance.InitializeProvider(App.Settings.Provider);
            }

            //save provider config if it exists
            if (!string.IsNullOrEmpty(_tempProviderConfig))
            {
                if (!App.Settings.ProviderSettings.ContainsKey(App.Settings.Provider))
                {
                    App.Settings.ProviderSettings[App.Settings.Provider] = new ActiveProviderInfo() { 
                        Active=true, AsyncOK = false, 
                        ExecutionOrder = 0, ProviderConfig = _tempProviderConfig, 
                        ProviderName = App.Settings.Provider};
                }
                else
                {
                    App.Settings.ProviderSettings[App.Settings.Provider].ProviderConfig = _tempProviderConfig;
                }
            }

            //save output providers
            foreach (ActiveProviderInfo api in OutputProviderInfos)
            {
                if (App.Settings.ProviderSettings.ContainsKey(api.ProviderName))
                {
                    //check if the existing item is different then the previous
                    if (App.Settings.ProviderSettings[api.ProviderName].GetComparisonHashCode() != api.GetComparisonHashCode())
                    {
                        //since they are different check if we deactivated or just changed a setting
                        if (api.Active)
                        {
                            App.Settings.ProviderSettings[api.ProviderName] = api;
                        }
                        else
                        {
                            //validate that active state has changed, if it has then deactivate
                            if (App.Settings.ProviderSettings[api.ProviderName].Active != api.Active)
                            {
                                App.Settings.ProviderSettings[api.ProviderName].Instance.Deactivate(null);
                            }

                            App.Settings.ProviderSettings.Remove(api.ProviderName);
                        }
                    }
                }
                else if (api.Active)
                {
                    App.Settings.ProviderSettings.Add(api.ProviderName, api);

                    //activate new provider
                    api.Instance.Activate(null);
                }
            }

            //save config file
            App.Settings.Save(App.Path + "\\settings.conf");

            if (UpdateSettings != null)
            {
                UpdateSettings(null, EventArgs.Empty);
            }
        }

        private void OutputProvidersCheckBox_Click(object sender, RoutedEventArgs e)
        {
            ApplyButton.IsEnabled = true;
        }


        private void SearchBoxIsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == false)
            {
                if (string.IsNullOrEmpty(SearchBox.Text))
                {
                    SearchBox.Text = Properties.Resources.OptionsSearchBox;
                    SearchBox.FontStyle = FontStyles.Italic;
                    SearchBox.Foreground = Brushes.Gray;
                }
            }
            else
            {
                if (SearchBox.Text == Properties.Resources.OptionsSearchBox)
                {
                    SearchBox.Text = "";
                    SearchBox.FontStyle = FontStyles.Normal;
                    SearchBox.Foreground = Brushes.Black;
                }
            }
        }

        private void SearchBoxKeyUp(object sender, KeyEventArgs e)
        {
            //What is this for?  SO that you can hit enter while in options and have it run the search??  Seems silly to me.
            //if (e.Key == Key.Enter && SearchBox.Text != Properties.Resources.OptionsSearchBox && SearchBox.Text.Length > 2)
            //{
            //    App.PictureManager.GetPicture(new PictureSearch() { ProviderSearchSettings = SearchBox.Text, PreFetch=false, MaxPictureCount=200, SearchProvider=App.CurrentProvider });
            //}
        }

        private void SearchBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text != Properties.Resources.OptionsSearchBox)
            {
                SearchBox.FontStyle = FontStyles.Normal;
                SearchBox.Foreground = Brushes.Black;
            }

            ApplyButton.IsEnabled = true;
        }

        private void CheckBoxClick(object sender, RoutedEventArgs e)
        {
            ApplyButton.IsEnabled = true;
        }

        private void RefreshIntervalSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ApplyButton.IsEnabled = true;
            if (RefreshIntervalSlider.Value < 60)
            {
                RefreshIntervalValueTextBlock.Text = RefreshIntervalSlider.Value + " " + Properties.Resources.OptionsIntervalMinutes;
            }
            else if (RefreshIntervalSlider.Value == 60)
            {
                RefreshIntervalValueTextBlock.Text = 1 + " " + Properties.Resources.OptionsIntervalHours;
            }
            else
            {
                RefreshIntervalValueTextBlock.Text = string.Format("{0} {1} {2} {3}", Math.Truncate(RefreshIntervalSlider.Value / 60), Properties.Resources.OptionsIntervalHours,
                    Math.Abs(Math.IEEERemainder(RefreshIntervalSlider.Value, 60)), Properties.Resources.OptionsIntervalMinutes);
            }
            if (RefreshIntervalSlider.Value == 0)
                RefreshIntervalValueTextBlock.Text = "1 " + Properties.Resources.OptionsIntervalMinutes;
        }

        private void ClearIntervalSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ApplyButton.IsEnabled = true;

            if (ClearIntervalSlider.Value == 1)
                ClearIntervalTextBlock.Text = string.Format(Properties.Resources.OptionsClearInterval, ClearIntervalSlider.Value + " " + Properties.Resources.OptionsDay);
            else
                ClearIntervalTextBlock.Text = string.Format(Properties.Resources.OptionsClearInterval, ClearIntervalSlider.Value + " " + Properties.Resources.OptionsDays);
        }

        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyButton.IsEnabled = true;
        }

        private void ProviderSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxSelectionChanged(sender, e);

            HandleProviderSettingsEnableAndLoad();
        }

        private void HandleProviderSettingsEnableAndLoad()
        {
            //enable or disable settings button depending on settings availability
            ProviderSettings.IsEnabled = (ProvidersBox.SelectedValue != null) && ProviderManager.Instance.HasConfigurationWindow(ProvidersBox.SelectedValue.ToString()) != null;

            //load provider config from settings if it exists
            if (ProvidersBox.SelectedValue != null && App.Settings.ProviderSettings.ContainsKey(ProvidersBox.SelectedValue.ToString()))
            {
                _tempProviderConfig = App.Settings.ProviderSettings[ProvidersBox.SelectedValue.ToString()].ProviderConfig;
            }
            else { _tempProviderConfig = string.Empty; }
        }

        private void ClearNowButtonClick(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(App.Path + "\\Cache"))
            {
                foreach (var f in Directory.GetFiles(App.Path + "\\Cache"))
                    File.Delete(f);
            }
        }

        private void ProviderSettings_Click(object sender, RoutedEventArgs e)
        {
            //get the usercontrol instance from Provider Manager
            var initSettings = ProviderManager.Instance.InitializeConfigurationWindow(ProvidersBox.SelectedValue.ToString());
            if (initSettings == null) return;

            //dialog window which will house the user control
            ProviderSettingsWindow psw = new ProviderSettingsWindow();

            //load the usercontrol into the window
            psw.LoadSettings(initSettings, ProvidersBox.SelectedValue.ToString());

            //load the configuration info into the user control
            initSettings.LoadConfiguration(_tempProviderConfig);

            //show the dialog box
            psw.ShowDialog();
            
            //if user clicked OK then call save to keep a copy of settings in memory
            if (initSettings.IsOK)
            {
                _tempProviderConfig = initSettings.SaveConfiguration();
                //activate apply option
                ApplyButton.IsEnabled = true;
            }
        }
    }
}
