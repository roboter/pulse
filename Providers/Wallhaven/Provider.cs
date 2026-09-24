using System;
using System.Net;
using System.Web.Script.Serialization;
using Pulse.Base;
using Pulse.Base.Providers;

namespace wallhaven
{
    [System.ComponentModel.Description("Wallhaven")]
    [ProviderConfigurationUserControl(typeof(WallhavenProviderPrefs))]
    [ProviderConfigurationClass(typeof(WallhavenSearchSettings))]
    [ProviderIcon(typeof(wallhaven.Properties.Resources), "wallhaven")]
    public class Provider : IInputProvider
    {
        private readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();

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

            WallhavenSearchSettings settings = string.IsNullOrEmpty(ps.SearchProvider?.ProviderConfig)
                ? new WallhavenSearchSettings()
                : WallhavenSearchSettings.LoadFromXML(ps.SearchProvider.ProviderConfig) ?? new WallhavenSearchSettings();

            int maxPictures = ps.MaxPictureCount > 0 ? ps.MaxPictureCount : 24;
            int pageIndex = ps.PageToRetrieve > 0 ? ps.PageToRetrieve : 1;
            string seed = null;
            int collectedCount = 0;

            do
            {
                string searchUrl = settings.BuildUrl(pageIndex, seed);
                string jsonResponse = null;

                try
                {
                    using (var client = new WebClient())
                    {
                        client.Headers.Add(HttpRequestHeader.UserAgent, "Pulse/1.0 (Windows; Wallpaper changer)");
                        if (!string.IsNullOrEmpty(settings.ApiKey))
                        {
                            client.Headers.Add("X-API-Key", settings.ApiKey.Trim());
                        }
                        client.Encoding = System.Text.Encoding.UTF8;
                        jsonResponse = client.DownloadString(searchUrl);
                    }
                }
                catch (WebException wex)
                {
                    var httpResp = wex.Response as HttpWebResponse;
                    if (httpResp != null && (int)httpResp.StatusCode == 429)
                    {
                        Log.Logger.Write("Wallhaven API rate limit reached (45 requests/minute). Stopping search.", Log.LoggerLevels.Warnings);
                    }
                    else if (httpResp != null && httpResp.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Log.Logger.Write("Wallhaven API unauthorized (401). Please check your API key.", Log.LoggerLevels.Errors);
                    }
                    else
                    {
                        Log.Logger.Write(string.Format("Wallhaven API search request failed: {0}", wex.Message), Log.LoggerLevels.Warnings);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Log.Logger.Write(string.Format("Error fetching Wallhaven wallpapers: {0}", ex.Message), Log.LoggerLevels.Errors);
                    break;
                }

                if (string.IsNullOrEmpty(jsonResponse))
                    break;

                WallhavenApiResponse apiResponse;
                try
                {
                    apiResponse = _serializer.Deserialize<WallhavenApiResponse>(jsonResponse);
                }
                catch (Exception ex)
                {
                    Log.Logger.Write(string.Format("Failed to deserialize Wallhaven API response: {0}", ex.Message), Log.LoggerLevels.Errors);
                    break;
                }

                if (apiResponse == null || apiResponse.data == null || apiResponse.data.Count == 0)
                    break;

                if (apiResponse.meta != null && !string.IsNullOrEmpty(apiResponse.meta.seed))
                {
                    seed = apiResponse.meta.seed;
                }

                int newOnThisPage = 0;
                foreach (var item in apiResponse.data)
                {
                    if (string.IsNullOrEmpty(item.path))
                        continue;

                    // Skip banned items (check full URL, id, or formatted id)
                    if (ps.BannedURLs != null &&
                        (ps.BannedURLs.Contains(item.path) ||
                         ps.BannedURLs.Contains(item.id) ||
                         ps.BannedURLs.Contains("wallhaven-" + item.id)))
                    {
                        continue;
                    }

                    var pic = new Picture
                    {
                        Url = item.path,
                        Id = "wallhaven-" + item.id,
                        ProviderInstance = ps.SearchProvider != null ? ps.SearchProvider.ProviderInstanceID : Guid.Empty
                    };

                    if (item.thumbs != null && !string.IsNullOrEmpty(item.thumbs.small))
                    {
                        pic.Properties.Add(Picture.StandardProperties.Thumbnail, item.thumbs.small);
                    }
                    else if (item.thumbs != null && !string.IsNullOrEmpty(item.thumbs.large))
                    {
                        pic.Properties.Add(Picture.StandardProperties.Thumbnail, item.thumbs.large);
                    }

                    if (!string.IsNullOrEmpty(item.url))
                    {
                        pic.Properties.Add(Picture.StandardProperties.Referrer, item.url);
                    }

                    pic.Properties.Add(Picture.StandardProperties.BanImageKey, item.id);
                    pic.Properties.Add(Picture.StandardProperties.ProviderLabel, "Wallhaven");

                    result.Pictures.Add(pic);
                    collectedCount++;
                    newOnThisPage++;

                    if (collectedCount >= maxPictures)
                        break;
                }

                // If no new pictures were added, or user requested only a specific page, or reached last page
                if (newOnThisPage == 0 ||
                    ps.PageToRetrieve > 0 ||
                    (apiResponse.meta != null && pageIndex >= apiResponse.meta.last_page))
                {
                    break;
                }

                pageIndex++;
            } while (collectedCount < maxPictures);

            return result;
        }
    }
}
