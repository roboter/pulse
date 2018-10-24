using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pulse.Base.Providers
{
    public class ProviderConfigurationUserControlAttribute : ProviderConfigurationAttribute
    {
        public ProviderConfigurationUserControlAttribute(Type userControlType) : base(userControlType)
        {
        }
    }
}
