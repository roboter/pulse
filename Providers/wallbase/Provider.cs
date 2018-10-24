using System;
using CsQuery;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using Pulse.Base;
using Pulse.Base.Providers;
using System.Threading;
using System.IO;
using System.Collections.Specialized;

namespace wallbase
{
    [System.ComponentModel.Description("Wallhaven")]
    [ProviderConfigurationUserControl(typeof(WallbaseProviderPrefs))]
    [ProviderConfigurationClass(typeof(WallbaseImageSearchSettings))]
    [ProviderIcon(typeof(wallhaven.Properties.Resources), "wallhaven")]
    public class Provider : IInputProvider
    {
        private int resultCount;
        private CookieContainer _cookies = new CookieContainer();

        public void Initialize(object args)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
        }

        public void Activate(object args) { }
        public void Deactivate(object args) { }

        public PictureList GetPictures(PictureSearch ps)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, policyErrors) => true;
            WallbaseImageSearchSettings wiss = string.IsNullOrEmpty(ps.SearchProvider.ProviderConfig) ? new WallbaseImageSearchSettings() : WallbaseImageSearchSettings.LoadFromXML(ps.SearchProvider.ProviderConfig);

            //if max picture count is 0, then no maximum, else specified max
            var maxPictureCount = wiss.GetMaxImageCount(ps.MaxPictureCount);
            int pageSize = wiss.GetPageSize();
            int pageIndex = (ps.PageToRetrieve <= 0 ? 1 : ps.PageToRetrieve); //set page to retreive if one is specified

            var imgFoundCount = 0;

            //authenticate to wallbase
            Authenticate(wiss.Username, wiss.Password);

            var wallResults = new List<Picture>();

            string areaURL = wiss.BuildURL();

            do
            {
                //calculate page index.  Random does not use pages, so for random just refresh with same url
                string strPageNum = pageIndex.ToString();

                string pageURL = areaURL.Contains("{0}") ? string.Format(areaURL, strPageNum) : areaURL;
                //string content = HttpPost(pageURL, postParams);
                string content = string.Empty;

                using (HttpUtility.CookieAwareWebClient _client = new HttpUtility.CookieAwareWebClient(_cookies))
                {
                    try
                    {
                        //if random then don't post values
                        //if (wiss.SA == "random")
                        //{
                        content = _client.DownloadString(pageURL);
                        //}
                        //else
                        //{
                        //    byte[] reqResult = _client.UploadValues(pageURL, wiss.GetPostParams());
                        //    content = System.Text.Encoding.Default.GetString(reqResult);
                        //}
                    }
                    catch (Exception ex)
                    {
                        Log.Logger.Write(string.Format("Failed to download search results from wallbase.cc, error: {0}", ex.ToString()), Log.LoggerLevels.Warnings);
                    }
                }

                if (string.IsNullOrEmpty(content))
                    break;

                //parse html and get count
                var pics = ParsePictures(content);
                imgFoundCount = pics.Count();

                //if we have an image ban list check for them
                // doing this in the provider instead of picture manager
                // ensures that our count does not go down if we have a max
                if (ps.BannedURLs != null && ps.BannedURLs.Count > 0)
                {
                    pics = (from c in pics where !(ps.BannedURLs.Contains(c.Url)) select c).ToList();
                }

                wallResults.AddRange(pics);

                //increment page index so we can get the next set of images if they exist
                pageIndex++;
            } while (imgFoundCount > 0 && wallResults.Count < maxPictureCount && ps.PageToRetrieve == 0);

            PictureList result = FetchPictures(wallResults, ps.PreviewOnly);
            result.Pictures = result.Pictures.Take(maxPictureCount).ToList();

            return result;
        }

        private PictureList FetchPictures(List<Picture> wallResults, bool previewOnly)
        {
            var result = new PictureList { FetchDate = DateTime.Now };

            try
            {
                wallResults
                    .AsParallel()
                    .WithDegreeOfParallelism(1)
                    .ForAll(delegate (Picture p)
                    {
                        try
                        {
                            //save original URL as referrer
                            p.Properties.Add(Picture.StandardProperties.Referrer, p.Url);
                            p.Properties.Add(Picture.StandardProperties.BanImageKey, Path.GetFileName(p.Url));
                            p.Properties.Add(Picture.StandardProperties.ProviderLabel, "Wallhaven");
                            //get actual image URL if this is not a preview
                            if (!previewOnly)
                                p.Url = GetDirectPictureUrl(p.Url);
                            p.Id = Path.GetFileNameWithoutExtension(p.Url);

                            if (!string.IsNullOrEmpty(p.Url) && !string.IsNullOrEmpty(p.Id))
                            {
                                result.Pictures.Add(p);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Logger.Write($"Error downloading picture object from '{ex}'. Exception details: {ex}", Log.LoggerLevels.Errors);
                        }
                    });
            }
            catch (Exception ex)
            {
                Log.Logger.Write(
                    $"Error during multi-threaded wallbase.cc image get.  Exception details: {ex.ToString()}", Log.LoggerLevels.Errors);
            }

            return result;
        }

        //find links to pages with wallpaper only with matching resolution
        private List<Picture> ParsePictures(string content)
        {
            var dom = CQ.CreateDocument(content);

            var previews = dom["figure.thumb"];

            var result = new List<Picture>();
            for (var i = 0; i < previews.Length; i++)
            {
                var pic = new Picture();
                var cq = previews[i].Cq();
                var url = cq["a.preview"][0];
                pic.Url = url.GetAttribute("href");

                var thumb = cq["img.lazyload"][0];
                pic.Properties.Add(Picture.StandardProperties.Thumbnail, thumb.GetAttribute("data-src"));

                result.Add(pic);
            }
            resultCount = result.Count;
            return result;
        }

        private string GetDirectPictureUrl(string pageUrl)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, policyErrors) => true;
            using (HttpUtility.CookieAwareWebClient cawc = new HttpUtility.CookieAwareWebClient(_cookies))
            {
                var content = cawc.DownloadString(pageUrl);
                if (string.IsNullOrEmpty(content)) return string.Empty;
                var dom = CQ.CreateDocument(content);
                var wall = dom["#wallpaper"];

                if (wall.Length > 0)
                {
                    return "https:" + wall[0].GetAttribute("data-cfsrc");
                }

                return string.Empty;
            }
        }

        private void Authenticate(string username, string password)
        {
            //if we have a username/password and we aren't already authenticated then authenticate
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                using (HttpUtility.CookieAwareWebClient _client = new HttpUtility.CookieAwareWebClient(_cookies))
                {
                    string homepage = _client.DownloadString("http://alpha.wallhaven.cc/");
                    var dom = CQ.CreateDocument(homepage);

                    //check if the user is already logged in (doh!)
                    try
                    {
                        var loginReg = dom["span.username"];

                        if (loginReg.Length > 0) return;
                    }
                    catch (Exception ex)
                    {
                        Log.Logger.Write(string.Format("There was an error trying to check for a pre-existing wallhaven auth, ignoring it.  Exception details: {0}", ex.ToString()), Log.LoggerLevels.Errors);
                    }

                    try
                    {
                        var token = dom["input[name='_token']"].Val<string>();

                        var loginData = new NameValueCollection();
                        loginData.Add("_token", token);

                        loginData.Add("username", username);
                        loginData.Add("password", password);

                        _client.Referrer = "http://alpha.wallhaven.cc/";
                        _client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");

                        byte[] result = _client.UploadValues(@"http://alpha.wallhaven.cc/auth/login", "POST", loginData);

                        //we do not need the response, all we need are the cookies
                        string response = System.Text.Encoding.UTF8.GetString(result);
                    }
                    catch (Exception ex)
                    {
                        throw new WallbaseAccessDeniedException("Wallhaven authentication failed. Please verify your username and password.", ex);
                    }
                }
            }
        }
    }
}
