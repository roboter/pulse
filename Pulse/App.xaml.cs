using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using Microsoft.Win32;
using Pulse.Base;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace Pulse
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Settings Settings;
        public static string Path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private Options optionsWindow;

        public static PictureManager PictureManager = new PictureManager();
        public static IInputProvider CurrentProvider = null;

        private DispatcherTimer timer;
        public static Translator Translator;
        private string translatedKeyword;

        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            //App startup, load settings
            Log.Logger.Write("Pulse starting up", Log.LoggerLevels.Information);

            //Hook global exception handler for the app domain
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            
            //load settings from file
            Settings = Settings.LoadFromFile("settings.conf") ?? new Settings();

            if (Log.Logger.LoggerLevel == Log.LoggerLevels.Verbose)
            {
                Log.Logger.Write(string.Format("Settings loaded, settings are: {0}", Environment.NewLine + Settings.Save()), Log.LoggerLevels.Verbose);
            }

            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(Settings.Language);
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Settings.Language);

            Log.Logger.Write(string.Format("Current Culture: {0}, UI Culture: {1}", 
                Thread.CurrentThread.CurrentCulture.ToString(),
                Thread.CurrentThread.CurrentUICulture.ToString()),
                Log.LoggerLevels.Information);

            //Create system tray icon
            AddIcon();

            Log.Logger.Write(string.Format("Clear old pics flag set to '{0}'", App.Settings.ClearOldPics.ToString()), Log.LoggerLevels.Verbose);
            //clear out old pictures on startup after 3 days
            // this should be on a timer job for people who never turn off their computers
            if (App.Settings.ClearOldPics && Directory.Exists(Settings.CachePath))
            {
                var oFiles = Directory.GetFiles(Settings.CachePath).Where(x => DateTime.Now.Subtract(File.GetCreationTime(x)).TotalDays >= App.Settings.ClearInterval);

                Log.Logger.Write(string.Format("Deleting {0} expired pics", App.Settings.ClearOldPics.ToString()), Log.LoggerLevels.Information);

                try
                {
                    foreach (var f in oFiles)
                    {
                        Log.Logger.Write(string.Format("Deleting '{0}'", f), Log.LoggerLevels.Verbose);
                        File.Delete(f);
                    }
                }
                catch (Exception ex)
                {
                    Log.Logger.Write(string.Format("Error cleaning out old pictures. Exception details: {0}", ex.ToString()), Log.LoggerLevels.Errors);
                }
            }

            PictureManager.PictureDownloaded += PmanagerPictureDownloaded;
            PictureManager.PictureDownloading += PictureManager_PictureDownloading;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(Settings.RefreshInterval);
            timer.Tick += TimerTick;

            if (Settings.DownloadAutomatically)
                timer.Start();

            Translator = new Translator();
            
            //load provider from settings
            CurrentProvider = (IInputProvider)ProviderManager.Instance.InitializeProvider(Settings.Provider);

            if (CurrentProvider == null)
            {
                MessageBox.Show("A provider is not currently selected.  Please choose a wallpaper provider from the options menu.");
            }
            else
            {
                Log.Logger.Write(string.Format("Autodownload wallpaper on startup is: '{0}'", Settings.DownloadOnAppStartup.ToString()),
                    Log.LoggerLevels.Information);

                //get next photo on startup if requested
                if (Settings.DownloadOnAppStartup)
                {
                    Log.Logger.Write("DownloadNextPicture called because 'Settings.DownloadOnAppStartup' == true", Log.LoggerLevels.Verbose);

                    DownloadNextPicture();
                }
            }
        }
        
        void TimerTick(object sender, EventArgs e)
        {
            Log.Logger.Write("DownloadNextPicture called from 'TimerTick'",
                Log.LoggerLevels.Verbose);

            DownloadNextPicture();
        }

        private static System.Windows.Forms.ContextMenu trayMenu;
        private static System.Windows.Forms.MenuItem closeMenuItem;
        private static System.Windows.Forms.MenuItem optionsMenuItem;
        private static System.Windows.Forms.MenuItem cacheViewMenuItem;
        private static System.Windows.Forms.MenuItem banImageMenuItem;
        private static System.Windows.Forms.MenuItem nextMenuItem;
        private static System.Windows.Forms.NotifyIcon trayIcon;

        public void AddIcon()
        {
            trayMenu = new System.Windows.Forms.ContextMenu();

            closeMenuItem = new System.Windows.Forms.MenuItem();
            closeMenuItem.Text = Pulse.Properties.Resources.Close;
            closeMenuItem.Click += CloseMenuItemClick;

            optionsMenuItem = new System.Windows.Forms.MenuItem();
            optionsMenuItem.Text = Pulse.Properties.Resources.Options;
            optionsMenuItem.Click += OptionsMenuItemClick;

            cacheViewMenuItem = new System.Windows.Forms.MenuItem();
            cacheViewMenuItem.Text = Pulse.Properties.Resources.ViewCacheFolder;
            cacheViewMenuItem.Click += ShowCacheFolderMenuItemClick;

            banImageMenuItem = new System.Windows.Forms.MenuItem();
            banImageMenuItem.Text = Pulse.Properties.Resources.BanImage;
            banImageMenuItem.Enabled = false;
            banImageMenuItem.Click += BanImageMenuItemClick;

            nextMenuItem = new System.Windows.Forms.MenuItem();
            nextMenuItem.Text = Pulse.Properties.Resources.NextPicture;
            nextMenuItem.Click += new EventHandler(NextMenuItemClick);

            trayMenu.MenuItems.Add(nextMenuItem);
            trayMenu.MenuItems.Add(cacheViewMenuItem);
            trayMenu.MenuItems.Add(banImageMenuItem);
            
            trayMenu.MenuItems.Add(optionsMenuItem);
            
            trayMenu.MenuItems.Add(closeMenuItem);

            trayIcon = new System.Windows.Forms.NotifyIcon();

            var stream = GetResourceStream(new Uri("/Pulse;component/Resources/icon_small.ico", UriKind.Relative)).Stream;
            trayIcon.Icon = new Icon(stream); //System.Drawing.Icon.ExtractAssociatedIcon(Path + @"\Pulse.exe");
            stream.Close();
            trayIcon.Text = "Pulse";
            trayIcon.Visible = true;
            trayIcon.ContextMenu = trayMenu;
        }

        void NextMenuItemClick(object sender, EventArgs e)
        {
            SkipToNextPicture();
        }

        void ShowOptionsMenu()
        {
            if (optionsWindow != null && optionsWindow.IsVisible)
            {
                optionsWindow.Activate();
                return;
            }

            if (optionsWindow != null)
                optionsWindow.UpdateSettings -= OptionsWindowUpdateSettings;

            optionsWindow = new Options();
            optionsWindow.UpdateSettings += OptionsWindowUpdateSettings;
            optionsWindow.Closed += OptionsWindowClosed;

            if (App.Settings.Language == "he-IL" || App.Settings.Language == "ar-SA")
            {
                optionsWindow.FlowDirection = System.Windows.FlowDirection.RightToLeft;
            }
            else
            {
                optionsWindow.FlowDirection = System.Windows.FlowDirection.LeftToRight;
            }

            optionsWindow.ShowDialog();
        }

        void OptionsMenuItemClick(object sender, EventArgs e)
        {
            ShowOptionsMenu();
        }

        void ShowCacheFolderMenuItemClick(object sender, EventArgs e)
        {
            if (!Directory.Exists(Settings.CachePath)) Directory.CreateDirectory(Settings.CachePath);
            //launch directory
            Process.Start("explorer.exe", Settings.CachePath);
        }

        void BanImageMenuItemClick(object sender, EventArgs e)
        {
            if (PictureManager.CurrentPicture != null)
            {
                if (MessageBox.Show(string.Format("Ban '{0}'?", PictureManager.CurrentPicture.Url), "Image Ban", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                {
                    return;
                }

                //add the url to the ban list
                Settings.BannedImages.Add(PictureManager.CurrentPicture.Url);
                //save the settings file
                Settings.Save(App.Path + "\\settings.conf");

                try
                {
                    //delete the file from the local disk
                    File.Delete(PictureManager.CurrentPicture.LocalPath);
                }
                catch (Exception ex)
                {
                    Log.Logger.Write(string.Format("Error deleting banned pictures. Exception details: {0}", ex.ToString()), Log.LoggerLevels.Errors);
                }

                //skip to next photo
                SkipToNextPicture();
            }
        }

        void OptionsWindowClosed(object sender, EventArgs e)
        {
            optionsWindow.Closed -= OptionsWindowClosed;
            //WinAPI.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        }

        void OptionsWindowUpdateSettings(object sender, EventArgs e)
        {
            translatedKeyword = string.Empty;

            timer.Stop();
            timer.Interval = TimeSpan.FromMinutes(Settings.RefreshInterval);
            if (Settings.DownloadAutomatically)
                timer.Start();
        }

        void CloseMenuItemClick(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();
            this.Shutdown();
        }

        void SkipToNextPicture()
        {
            DownloadNextPicture();
            //Stop and restart the timer so that the next picture won't happen to soon
            if (Settings.DownloadAutomatically)
            {
                timer.Stop();
                timer.Start();
            }
        }

        void DownloadNextPicture()
        {
            var ps = new PictureSearch()
            {
                SearchString = App.Settings.Search,
                PreFetch = App.Settings.PreFetch,
                SaveFolder = App.Settings.CachePath,
                MaxPictureCount = App.Settings.MaxPictureDownloadCount,
                SearchProvider = App.CurrentProvider,
                BannedURLs = App.Settings.BannedImages,
                ProviderSearchSettings = App.Settings.ProviderSettings.ContainsKey(App.Settings.Provider) ? App.Settings.ProviderSettings[App.Settings.Provider].ProviderConfig : string.Empty
            };

            if (App.Settings.Language == "ru-RU" || App.Settings.Provider != "Rewalls")
                App.PictureManager.GetPicture(ps);
            else
            {
                if (string.IsNullOrEmpty(translatedKeyword))
                {
                    ThreadStart threadStarter = delegate
                    {
                        translatedKeyword = Translator.TranslateText(App.Settings.Search, "en|ru");
                        ps.SearchString = translatedKeyword;
                        App.PictureManager.GetPicture(ps);
                    };
                    var thread = new Thread(threadStarter);
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
                else
                {
                    ps.SearchString = translatedKeyword;
                    App.PictureManager.GetPicture(ps);
                }
            }
        }

        void PmanagerPictureDownloaded(Picture pic)
        {
            if (pic != null)
            {
                //enable image banning
                banImageMenuItem.Enabled = true;

                //call all activated output providers in order
                foreach (var op in Settings.ProviderSettings.Values.Where(x => x.Active).OrderBy(x=> x.ExecutionOrder))
                {
                    if (!typeof(IOutputProvider).IsAssignableFrom(op.Instance.GetType())) continue;
                    //wrap each in a try/catch block so we avoid killing pulse on error
                    try
                    {
                        ((IOutputProvider)op.Instance).ProcessPicture(pic);
                    }
                    catch(Exception ex) {
                        Log.Logger.Write(string.Format("Error processing output plugin '{0}'.  Error: {1}", op.ProviderName, ex.ToString()), Log.LoggerLevels.Errors);
                    }
                }
            }

            var stream = GetResourceStream(new Uri("/Pulse;component/Resources/icon_small.ico", UriKind.Relative)).Stream;
            trayIcon.Icon = new Icon(stream);
            stream.Close();            
        }

        void PictureManager_PictureDownloading()
        {
            var stream = GetResourceStream(new Uri("/Pulse;component/Resources/icon_downloading.ico", UriKind.Relative)).Stream;
            trayIcon.Icon = new Icon(stream);
            stream.Close();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args) {
            Log.Logger.Write("Unhandled exception! Error Details: " + args.ExceptionObject.ToString(), Log.LoggerLevels.Errors);

            MessageBox.Show("Pulse has encountered an exception and will exit.  The exception has been logged to the Pulse Log file.  Please post on the Issue Tracker page located on the Pulse Website @ http://pulse.codeplex.com/.  Exception details: " + ((Exception)args.ExceptionObject).Message, "Pulse Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
