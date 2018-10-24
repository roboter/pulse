using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wallbase
{
    public class WallbaseAccessDeniedException : Exception
    {
        public WallbaseAccessDeniedException(string msg, Exception inner)
            : base(msg, inner)
        {

        }
    }
}
