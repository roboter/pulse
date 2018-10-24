using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pulse.Base.Providers
{
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
    public class ProviderRunsAsyncAttribute : Attribute
    {
        public bool AsyncOK { get; protected set; }

        public ProviderRunsAsyncAttribute(bool asyncOK)
        {
            this.AsyncOK = asyncOK;
        }
    }
}
