using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pulse.Base.Providers
{
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
    public class ProviderConfigurationClassAttribute : ProviderConfigurationAttribute
    {
        public ProviderConfigurationClassAttribute(Type configurationClassType) : base(configurationClassType)
        {
        }
    }
}
