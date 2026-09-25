using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using Pulse.Base;
using Pulse.Base.Providers;

namespace GoogleImages
{
    [ProviderConfigurationClass(typeof(GoogleImageSearchSettings))]
    [System.ComponentModel.Description("Google Images")]
    [ProviderIcon(typeof(Properties.Resources), "googleImages")]
    public class Provider : IInputProvider
    {
        private readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();
        private const string UserAgent = "Pulse/1.0 (Windows; Wallpaper changer)";

        public Provider()
        {
        }

        public void Initialize(object args)
        {
            ServicePointManager.Expect100Continue = false;
            try
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            }
            catch { }
        }

        public void Activate(object args) { }
        public void Deactivate(object args) { }

        public PictureList GetPictures(PictureSearch ps)
        {
            var result = new PictureList { FetchDate = DateTime.Now };

            GoogleImageSearchSettings settings = string.IsNullOrEmpty(ps.SearchProvider?.ProviderConfig)
                ? new GoogleImageSearchSettings()
                : GoogleImageSearchSettings.LoadFromXML(ps.SearchProvider.ProviderConfig) ?? new GoogleImageSearchSettings();

            if (string.IsNullOrEmpty(settings.Query))
            {
                return result;
            }

            if (string.IsNullOrWhiteSpace(settings.ApiKey) || string.IsNullOrWhiteSpace(settings.SearchEngineId))
            {
                Log.Logger.Write("Google Images Provider: An API Key and Search Engine ID (CX) are required. Please configure them in Google Images Provider Settings (obtain from Google Cloud Console & programmablesearchengine.google.com).", Log.LoggerLevels.Warnings);
                return result;
            }

            int maxPictures = ps.MaxPictureCount > 0 ? ps.MaxPictureCount : 10;
            int pageIndex = ps.PageToRetrieve > 0 ? ps.PageToRetrieve : 1;
            int startIndex = (pageIndex - 1) * 10 + 1;

            do
            {
                int countToFetch = Math.Min(10, maxPictures - result.Pictures.Count);
                string searchUrl = settings.BuildUrl(startIndex, countToFetch);
                string jsonResponse = null;

                try
                {
                    using (var client = new WebClient())
                    {
                        client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent);
                        client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                        client.Encoding = System.Text.Encoding.UTF8;
                        jsonResponse = client.DownloadString(searchUrl);
                    }
                }
                catch (WebException wex)
                {
                    var httpResp = wex.Response as HttpWebResponse;
                    string errDetail = string.Empty;
                    if (wex.Response != null)
                    {
                        try
                        {
                            using (var sr = new StreamReader(wex.Response.GetResponseStream()))
                            {
                                string errBody = sr.ReadToEnd();
                                var errObj = _serializer.Deserialize<GoogleCustomSearchErrorResponse>(errBody);
                                if (errObj?.error != null)
                                {
                                    errDetail = string.Format(" Code {0}: {1}", errObj.error.code, errObj.error.message);
                                }
                            }
                        }
                        catch { }
                    }

                    if (httpResp != null && httpResp.StatusCode == HttpStatusCode.Forbidden)
                    {
                        Log.Logger.Write(string.Format("Google Custom Search API error: Daily quota exceeded or invalid API Key / CX.{0}", errDetail), Log.LoggerLevels.Errors);
                    }
                    else if (httpResp != null && httpResp.StatusCode == HttpStatusCode.BadRequest)
                    {
                        Log.Logger.Write(string.Format("Google Custom Search API error: Bad request. Please check Search Engine ID (CX) and query.{0}", errDetail), Log.LoggerLevels.Errors);
                    }
                    else
                    {
                        Log.Logger.Write(string.Format("Google Custom Search API request failed: {0}.{1}", wex.Message, errDetail), Log.LoggerLevels.Warnings);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Log.Logger.Write(string.Format("Error fetching Google Custom Search images: {0}", ex.Message), Log.LoggerLevels.Errors);
                    break;
                }

                if (string.IsNullOrEmpty(jsonResponse))
                    break;

                GoogleCustomSearchResponse apiResponse;
                try
                {
                    apiResponse = _serializer.Deserialize<GoogleCustomSearchResponse>(jsonResponse);
                }
                catch (Exception ex)
                {
                    Log.Logger.Write(string.Format("Failed to deserialize Google Custom Search API response: {0}", ex.Message), Log.LoggerLevels.Errors);
                    break;
                }

                if (apiResponse?.items == null || apiResponse.items.Count == 0)
                    break;

                foreach (var item in apiResponse.items)
                {
                    if (string.IsNullOrWhiteSpace(item.link)) continue;

                    if (ps.BannedURLs != null && ps.BannedURLs.Contains(item.link)) continue;

                    AddPicture(result, item.link, item.image?.contextLink ?? item.displayLink, item.image?.thumbnailLink, item.title);

                    if (result.Pictures.Count >= maxPictures)
                        break;
                }

                startIndex += 10;
            } while (result.Pictures.Count < maxPictures && startIndex <= 91 && ps.PageToRetrieve == 0);

            return result;
        }

        private static void AddPicture(PictureList result, string purl, string referrer, string thumbnail, string title)
        {
            if (string.IsNullOrEmpty(purl)) return;

            string id = Path.GetFileNameWithoutExtension(purl);
            if (string.IsNullOrEmpty(id) || id.Length > 50)
            {
                id = id != null && id.Length > 50 ? id.Substring(0, 50) : "google_img";
            }
            id = string.Format("{0}_{1:X8}", id, (uint)purl.GetHashCode());

            var p = new Picture { Url = purl, Id = id };

            if (!string.IsNullOrEmpty(thumbnail))
                p.Properties.Add(Picture.StandardProperties.Thumbnail, thumbnail);
            if (!string.IsNullOrEmpty(referrer))
                p.Properties.Add(Picture.StandardProperties.Referrer, referrer);
            if (!string.IsNullOrEmpty(title))
                p.Properties.Add("Title", title);

            p.Properties.Add(Picture.StandardProperties.ProviderLabel, "Google Images");

            result.Pictures.Add(p);
        }
    }

    #region Google Custom Search JSON API Models
    public class GoogleCustomSearchResponse
    {
        public GoogleCustomSearchSearchInformation searchInformation { get; set; }
        public List<GoogleCustomSearchItem> items { get; set; }
    }

    public class GoogleCustomSearchSearchInformation
    {
        public string totalResults { get; set; }
        public double searchTime { get; set; }
    }

    public class GoogleCustomSearchItem
    {
        public string title { get; set; }
        public string link { get; set; }
        public string displayLink { get; set; }
        public string snippet { get; set; }
        public string mime { get; set; }
        public GoogleCustomSearchImage image { get; set; }
    }

    public class GoogleCustomSearchImage
    {
        public string contextLink { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public long byteSize { get; set; }
        public string thumbnailLink { get; set; }
        public int thumbnailHeight { get; set; }
        public int thumbnailWidth { get; set; }
    }

    public class GoogleCustomSearchErrorResponse
    {
        public GoogleCustomSearchErrorDetails error { get; set; }
    }

    public class GoogleCustomSearchErrorDetails
    {
        public int code { get; set; }
        public string message { get; set; }
        public string status { get; set; }
    }
    #endregion
}
