using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pulse.Base
{
    /// <summary>
    /// Gathers pictures using a provider defined method
    /// </summary>
    public interface IInputProvider : IProvider
    {
        /// <summary>
        /// Retrieves a list of pictures from the current provider using the specified search parameters
        /// </summary>
        /// <param name="ps">Search parameters that will be used to find pictures</param>
        /// <returns></returns>
        PictureList GetPictures(PictureSearch ps);
    }
}
