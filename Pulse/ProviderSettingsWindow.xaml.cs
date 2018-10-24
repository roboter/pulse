using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Pulse.Base;

namespace Pulse
{
    /// <summary>
    /// Interaction logic for ProviderSettingsWindow.xaml
    /// </summary>
    public partial class ProviderSettingsWindow : Window
    {
        private ProviderConfigurationBase _uc;

        public ProviderSettingsWindow()
        {
            InitializeComponent();
        }

        public void LoadSettings(ProviderConfigurationBase uc, string providerName)
        {
            _uc = uc;

            this.Title = Properties.Resources.ProviderSettingsTitle + providerName;
            //spContent.Children.Add(uc);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            _uc.IsOK = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _uc.IsOK = false;
            this.Close();
        }
    }
}
