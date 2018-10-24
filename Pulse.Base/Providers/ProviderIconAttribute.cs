using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

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
