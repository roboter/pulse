using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pulse.Base.Providers
{
    public partial class ProviderConfigurationPropertyGrid<T>: UserControl, IProviderConfigurationEditor
        where T : class, new()
    {
        public ProviderConfigurationPropertyGrid()
        {
            InitializeComponent();
        }

        public bool IsOK { get; set; }

        public void LoadConfiguration(string config)
        {
            T obj = XmlSerializable<T>.LoadFromXML(config);

            if (obj == null)
            {
                obj = (T)Activator.CreateInstance(typeof(T));
            }
            
            pgProvider.SelectedObject = obj;
        }

        public string SaveConfiguration()
        {
            string s = XmlSerializable<T>.Save((T)pgProvider.SelectedObject);

            return s;
        }

        public void HostMe(object parent)
        {
            Control c = parent as Control;

            c.Controls.Add(this);
        }
    }
}
