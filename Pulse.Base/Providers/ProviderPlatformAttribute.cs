using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pulse.Base
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ProviderPlatformAttribute : Attribute
    {
        readonly PlatformID _platformID;
        readonly int _majorVersion;
        readonly int _minorVersion;

        // This is a positional argument
        public ProviderPlatformAttribute(PlatformID platform, int major, int minor)
        {
            this._platformID = platform;
            this._majorVersion = major;
            this._minorVersion = minor;
        }

        public PlatformID Platform
        {
            get { return _platformID; }
        }

        public int MajorVersion { get { return _majorVersion; } }
        public int MinorVersion { get { return _minorVersion; } }
    }
}
