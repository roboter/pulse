using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Pulse.Base
{
    public class Status
    {
        public delegate void StatusChanged(Status sender);

        public Icon StatusIcon { get; protected set; }
        public string Title { get; protected set; }
        public string Message { get; protected set; }

        public Status(Icon statusIcon, string title, string message)
        {
            this.StatusIcon = statusIcon;
            this.Title = title;
            this.Message = message;
        }

        public static class StandardStatusMessages
        {
            public static Status Normal {get; private set; }
            public static Status Downloading { get; private set; }
            public static Status NotConfigured { get; private set; }

            static StandardStatusMessages()
            {
                Normal = new Status(Properties.Resources.Icon, "Pulse", "");
                Downloading = new Status(Properties.Resources.icon_downloading, "Pulse - Downloading", "");
                NotConfigured = new Status(Properties.Resources.Icon, "Pulse - Configuration Required", 
                                    @"Pulse has not been configured yet.  Click here to configure Pulse. You may also right click the Pulse icon and choose Options.");
            }
        }
    }
}
