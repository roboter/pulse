using System;
using System.Drawing;

namespace Pulse.Base.Providers
{
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
    public class ProviderIconAttribute : Attribute
    {
        public Image ProviderIcon { get; protected set; }

        public ProviderIconAttribute(Type resourceType, string resourceName)
        {
            var value = Pulse.Base.GeneralHelper.ResourceHelper.GetResourceLookup<Image>(resourceType, resourceName);

            this.ProviderIcon = value;
        }
    }
}
