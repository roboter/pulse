using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.IO;
using System.Threading;
using System.Resources;
using CodePlexNewReleaseChecker;
using System.Threading.Tasks;

namespace Pulse.Base
{
    public class PulseRunner : IDisposable
    {
        public event Pulse.Base.Status.StatusChanged StatusChanged;
        public event Action<CodePlexNewReleaseChecker.Release> NewVersionAvailable;
        public event Action<PictureBatch> BatchChanging;
        public event Action<PictureBatch> BatchChanged;
        public event Action TimerStarted;
        public event Action TimerStopped;

        public PictureBatch CurrentBatch { get; set; }

        public bool IsTimerRunning { get { return wallpaperChangerTimer.Enabled; } }

        public PictureManager PictureManager = new PictureManager();
        public Dictionary<Guid, ActiveProviderInfo> CurrentInputProviders = new Dictionary<Guid, ActiveProviderInfo>();

        private DownloadManager DownloadManager = DownloadManager.Current;

        private System.Timers.Timer wallpaperChangerTimer = new System.Timers.Timer();
        private System.Timers.Timer clearOldWallpapersTimer = new System.Timers.Timer();

        public void Initialize()
        {
            //App startup, load settings
            Log.Logger.Write("Pulse starting up", Log.LoggerLevels.Information);

            //check if verbose before logging the settings so we only do the work of 
            // serializing them if we actually have to.
            if (Log.Logger.LoggerLevel == Log.LoggerLevels.Verbose)
            {
                Log.Logger.Write(string.Format("Settings loaded, settings are: {0}", Environment.NewLine + Settings.CurrentSettings.Save()), Log.LoggerLevels.Verbose);
            }

            //check if we should check for new versions or not, if yes then use the awesome CodePlex version Checker project :)
            Log.Logger.Write(string.Format("Check for new Pulse Versions set to '{0}'", Settings.CurrentSettings.CheckForNewPulseVersions.ToString()), Log.LoggerLevels.Verbose);
            if (Settings.CurrentSettings.CheckForNewPulseVersions)
            {
                CheckForNewVersion();
            }

            Log.Logger.Write(string.Format("Clear old pics flag set to '{0}'", Settings.CurrentSettings.ClearOldPics.ToString()), Log.LoggerLevels.Verbose);

            if (Settings.CurrentSettings.ClearOldPics)
            {
                ClearOldWallpapers();
            }           

            wallpaperChangerTimer.Elapsed += WallpaperChangerTimerTick;
            clearOldWallpapersTimer.Elapsed += clearOldWallpapersTimer_Elapsed;

            UpdateFromConfiguration();

            //if we have a provider and we are setup for automatic picture download/change on startup then do so
            if(CurrentInputProviders.Count > 0)
            {
                Log.Logger.Write(string.Format("Autodownload wallpaper on startup is: '{0}'", Settings.CurrentSettings.DownloadOnAppStartup.ToString()),
                    Log.LoggerLevels.Information);

                //get next photo on startup if requested
                if (Settings.CurrentSettings.DownloadOnAppStartup)
                {
                    Log.Logger.Write("DownloadNextPicture called because 'Settings.CurrentSettings.DownloadOnAppStartup' == true", Log.LoggerLevels.Verbose);

                    SkipToNextPicture();
                }
            }
        }

        public void CheckForNewVersion()
        {
            //NewVersionAvailable
            try
            {
                VersionChecker vcReleasedPageStripper = new VersionChecker("Pulse", new RssFeedReleases());

                //get the release record for the version installed on the client
                Release client = vcReleasedPageStripper.GetReleaseByName(string.Format("Pulse {0}",
                                        System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()));

                //if we weren't able to find the current version then return
                if (client == null) return;

                //get the default release
                Release defaultRelease = vcReleasedPageStripper.GetDefaultRelease();

                //compare against the version installed on client (logic is up to you)
                if (defaultRelease.ReleaseDate > client.ReleaseDate)
                {
                    //Notify user of upgrade
                    if (NewVersionAvailable != null)
                        NewVersionAvailable(defaultRelease);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Write(string.Format("Error checking for new Pulse Versions: {0}", ex.ToString()), Log.LoggerLevels.Warnings);
            }
        }

        public void StartTimer()
        {
            wallpaperChangerTimer.Start();
            if (TimerStarted != null)
                TimerStarted();
        }

        public void StopTimer()
        {
            wallpaperChangerTimer.Stop();
            if (TimerStopped != null)
                TimerStopped();
        }

        public void UpdateFromConfiguration()
        {
            //setup the timers for changing wallpapers and clearing old files
            SetTimers();

            //setup list of valid & active input providers
            SetInputProviders();
        }

        public void SetNewStatus(Status status)
        {
            if (StatusChanged != null)
                StatusChanged(status);
        }

        public void ClearOldWallpapers()
        {
            //if our cache folder does not exist then don't do it
            if (!Directory.Exists(Settings.CurrentSettings.CachePath))
                return;

            var oFiles = Directory.GetFiles(Settings.CurrentSettings.CachePath).Where(x => DateTime.Now.Subtract(File.GetCreationTime(x)).TotalDays >= Settings.CurrentSettings.ClearInterval);

            Log.Logger.Write(string.Format("Deleting {0} expired pics", Settings.CurrentSettings.ClearOldPics.ToString()), Log.LoggerLevels.Information);

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

        public void SkipToNextPicture()
        {
            wallpaperChangerTimer.Stop();

            ThreadStart tstarter = () => {
                DownloadNextPicture();

                //restart the timer if appropriate
                if (Settings.CurrentSettings.ChangeOnTimer)
                {
                    wallpaperChangerTimer.Start();
                }
            };

            Thread t = new Thread(tstarter);

            t.Start();
        }

        public void BackToPreviousPicture()
        {
            if (CurrentBatch == null || CurrentBatch.PreviousBatch == null) return;

            ProcessDownloadedPicture(CurrentBatch.PreviousBatch);
        }

        public void BanPicture(Picture p)
        {
            string banString =
                        (p.Properties.ContainsKey(Picture.StandardProperties.BanImageKey)
                        ? p.Properties[Picture.StandardProperties.BanImageKey] : p.Url);

            //add the url to the ban list
            Settings.CurrentSettings.BannedImages.Add(banString);
            //save the settings file
            Settings.CurrentSettings.Save(Settings.AppPath + "\\settings.conf");

            try
            {
                //delete the file from the local disk
                File.Delete(p.LocalPath);
            }
            catch (Exception ex)
            {
                Log.Logger.Write(string.Format("Error deleting banned pictures. Exception details: {0}", ex.ToString()), Log.LoggerLevels.Errors);
            }

            //skip to next photo
            SkipToNextPicture();        
        }

        protected void DownloadNextPicture()
        {
            if (CurrentInputProviders.Count == 0) return;
            //create the new picture batch
            PictureBatch pb = new PictureBatch() {PreviousBatch = CurrentBatch};

            //create another view of the input providers, otherwise if the list changes
            // because user changes options then it breaks :)
            foreach (KeyValuePair<Guid, ActiveProviderInfo> kvpGAPI in CurrentInputProviders.ToArray())
            {
                ActiveProviderInfo api = kvpGAPI.Value;

                var ps = new PictureSearch()
                {
                    SaveFolder = Settings.CurrentSettings.CachePath,
                    MaxPictureCount = Settings.CurrentSettings.MaxPictureDownloadCount,
                    SearchProvider = api,
                    BannedURLs = Settings.CurrentSettings.BannedImages
                };

                //get new pictures
                PictureList pl = PictureManager.GetPictureList(ps);
                
                //save to picturebatch 
                pb.AllPictures.Add(pl);
            }
            
            //process downloaded picture list
            ProcessDownloadedPicture(pb);
            
            //if prefetch is enabled, validate that all pictures have been downloaded
            if (Settings.CurrentSettings.PreFetch) DownloadManager.PreFetchFiles(pb);
        }

        protected void SetTimers()
        {
            StopTimer();
            clearOldWallpapersTimer.Stop();

            RecalculateTimerIntervals();

            if(Settings.CurrentSettings.ClearOldPics)
                clearOldWallpapersTimer.Start();

            if (Settings.CurrentSettings.ChangeOnTimer)
                StartTimer();
        }

        protected void RecalculateTimerIntervals()
        {
            wallpaperChangerTimer.Interval =
                GetTimerTickTime(
                    Settings.CurrentSettings.IntervalUnit,
                    Settings.CurrentSettings.RefreshInterval
                        ).TotalMilliseconds;

            clearOldWallpapersTimer.Interval = TimeSpan.FromDays(Settings.CurrentSettings.ClearInterval).TotalMilliseconds;
        }

        protected void SetInputProviders()
        {
            //load providers from settings
            var validInputs = ProviderManager.Instance.GetProvidersByType<IInputProvider>();
            var toKeep = new List<Guid>();

            foreach (var op in Settings.CurrentSettings.ProviderSettings)
            {
                if (!CurrentInputProviders.ContainsKey(op.Key))
                {
                    if (validInputs.ContainsKey(op.Value.ProviderName) && op.Value.Active)
                    {
                        CurrentInputProviders.Add(op.Key, op.Value);

                        //activate provider
                        op.Value.Instance.Activate(null);
                    }
                }
                else
                {
                    //if this instance of the provider is already present then make sure settings are the same
                    if (CurrentInputProviders[op.Key].GetComparisonHashCode() !=
                        op.Value.GetComparisonHashCode())
                    {
                        //if not the same configuration the swap for new config
                        CurrentInputProviders[op.Key] = op.Value;
                    }
                }

                toKeep.Add(op.Key);
            }

            //deactivate and remove any providers that aren't being kept
            var toRemove = (from c in CurrentInputProviders
                            where !toKeep.Contains(c.Key)
                            select c).ToList();

            foreach (var c in toRemove)
            {
                c.Value.Instance.Deactivate(null);
                CurrentInputProviders.Remove(c.Key);
            }
        }

        protected TimeSpan GetTimerTickTime(Settings.IntervalUnits unit, int amount)
        {
            TimeSpan ts;

            //calculate timer elapse frequency
            switch (unit)
            {
                case Settings.IntervalUnits.Days:
                    ts = new TimeSpan(amount, 0, 0, 0);
                    break;
                case Settings.IntervalUnits.Hours:
                    ts = new TimeSpan(0, amount, 0, 0);
                    break;
                case Settings.IntervalUnits.Minutes:
                    ts = new TimeSpan(0, 0, amount, 0);
                    break;
                case Settings.IntervalUnits.Seconds:
                    ts = new TimeSpan(0, 0, 0, amount);
                    break;
                default:
                    ts = new TimeSpan(0, 1, 0, 0);
                    break;
            }

            return ts;
        }

        private void WallpaperChangerTimerTick(object sender, EventArgs e)
        {
            Log.Logger.Write("DownloadNextPicture called from 'TimerTick'",
                Log.LoggerLevels.Verbose);

            if (Settings.CurrentSettings.SkipChangeIfFullScreen && WinAPI.WinAPI.IsForegroundWwindowFullScreen())
            {
                Log.Logger.Write("DownloadNextPicture Skipped because window is in full screen mode", Log.LoggerLevels.Verbose);
                return;
            }

            //stop the timer
            wallpaperChangerTimer.Stop();

            try
            {
                DownloadNextPicture();
            }
            catch (Exception ex)
            {
                Log.Logger.Write("TimerTick Errored: " + ex.ToString(),
                    Log.LoggerLevels.Errors);
            }
            finally
            {
                wallpaperChangerTimer.Start();
            }
        }

        private void clearOldWallpapersTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ClearOldWallpapers();
        }

        private void ProcessDownloadedPicture(PictureBatch pb)
        {
            if (pb != null && pb.AllPictures.Any())
            {
                this.CurrentBatch = pb;

                if (BatchChanging != null)
                    BatchChanging(pb);

                //Clear the download Queue
                DownloadManager.ClearQueue();

                List<ManualResetEvent> manualEvents = new List<ManualResetEvent>();

                //call all activated output providers in order
                foreach (var op in Settings.CurrentSettings.ProviderSettings.Values.Where(x => x.Active && x.Instance != null).OrderBy(x => x.ExecutionOrder))
                {
                    if (!typeof(IOutputProvider).IsAssignableFrom(op.Instance.GetType())) continue;
                    //wrap each in a try/catch block so we avoid killing pulse on error
                    try
                    {
                        //check if this can be run in async
                        bool asyncOK = ProviderManager.GetAsyncStatusForType(op.Instance.GetType());

                        IOutputProvider ip = ((IOutputProvider)op.Instance);
                        string config = op.ProviderConfig;

                        if (asyncOK)
                        {
                            ThreadStart threadStarter = () =>
                            {
                                ManualResetEvent mre = new ManualResetEvent(false);
                                manualEvents.Add(mre);
                                try
                                {
                                    ip.ProcessPicture(pb, config);
                                }
                                catch (Exception ex) { 
                                    Log.Logger.Write(string.Format("Error processing output plugin '{0}'.  Error: {1}", op.ProviderName, ex.ToString()), Log.LoggerLevels.Warnings);
                                }
                                finally { mre.Set(); }
                            };
                            Thread thread = new Thread(threadStarter);
                            thread.Start();
                        }
                        else
                        {
                            ip.ProcessPicture(pb, config);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Logger.Write(string.Format("Error processing output plugin '{0}'.  Error: {1}", op.ProviderName, ex.ToString()), Log.LoggerLevels.Errors);
                    }
                }
                //make sure async operations finish before
                if (manualEvents.Count > 1)
                {
                    WaitHandle.WaitAll(manualEvents.ToArray(), 60 * 1000);
                }
                else if (manualEvents.Count == 1)
                {
                    manualEvents[0].WaitOne();
                }

                if (BatchChanged != null)
                    BatchChanged(pb);
            }
        }

        public void Dispose()
        {
            wallpaperChangerTimer.Dispose();
            clearOldWallpapersTimer.Dispose();
        }
    }
}
