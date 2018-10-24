using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pulse.Base
{
    public interface IProvider
    {
        /// <summary>
        /// This method is called when this provider is activated (checkbox clicked in options, provider picked from ddl)
        /// </summary>
        /// <param name="args"></param>
        void Activate(object args);

        /// <summary>
        /// This method is called when the provider is deactivated (clean up routine)
        /// </summary>
        /// <param name="args"></param>
        void Deactivate(object args);

        /// <summary>
        /// This method is called everytime the applicaiton starts or immediately after activation
        /// </summary>
        void Initialize(object args);
    }
}
