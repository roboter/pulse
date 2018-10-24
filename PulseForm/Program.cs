using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Pulse.Base;

namespace PulseForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPulseHost));
            
            frmPulseHost fph = new frmPulseHost();
            fph.niPulse.Icon = Properties.Resources.PulseIcon;
            fph.Icon = Properties.Resources.PulseIcon;
            
            //decide whether the Pulse Host should be shown or if we should show the tray Icon
            //if we are a linux/unix/mac client
            if (GeneralHelper.IsClientLinux)
            {
                fph.WindowState = FormWindowState.Normal;
                fph.niPulse.Visible = false;
            }
            else {
                fph.ShowInTaskbar = false;
                Form f1 = new Form();
                f1.ShowInTaskbar = false;
                f1.FormBorderStyle = FormBorderStyle.FixedToolWindow;

                fph.Owner = f1;

                fph.niPulse.Visible = true; 
            }

            Application.Run(fph);

            //TrayIconWrapper icon = new TrayIconWrapper(fph);
            //icon.Icon = Properties.Resources.PulseIcon;
            //icon.Text = "Pulse";
            //icon.Visible = true;
            //icon.ContextMenuStrip = fph.cmsTrayIcon;

            //icon.Run();

            //if (GeneralHelper.IsClientLinux)
            //{
            //    while (true)
            //    {
            //        Application.DoEvents();
            //    }
            //}
           
        }
    }
}
