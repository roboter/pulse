using System;

namespace Pulse.Base.Providers
{
    public class ProviderConfigurationUserControlAttribute : ProviderConfigurationAttribute
    {
        public ProviderConfigurationUserControlAttribute(Type userControlType) : base(userControlType)
        {
        }
    }
}
