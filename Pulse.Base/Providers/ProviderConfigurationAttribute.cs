using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pulse.Base.Providers
{
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true)]
    public abstract class ProviderConfigurationAttribute : Attribute
    {
        public Type Type { get; protected set; }

        public ProviderConfigurationAttribute(Type type)
        {
            this.Type = type;
        }
    }
}
