using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pulse.Base
{
    public interface IProviderConfigurationEditor : IProviderConfiguration
    {
        bool IsOK { get; set; }

        void HostMe(object parent);
    }

    public interface IProviderConfiguration
    {
        void LoadConfiguration(string config);
        string SaveConfiguration();
    }
}
