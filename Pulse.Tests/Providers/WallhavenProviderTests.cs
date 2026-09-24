using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wallhaven;
using Pulse.Tests.Helpers;

namespace Pulse.Tests.Providers
{
    [TestClass]
    public class WallhavenProviderTests
    {
        private Provider _provider;

        [TestInitialize]
        public void Setup()
        {
            _provider = new Provider();
            _provider.Initialize(null);
        }

        // ── Settings Tests ───────────────────────────────────────────────────

        [TestMethod]
        public void Settings_DefaultValues_AreSensible()
        {
            var s = new WallhavenSearchSettings();

            Assert.AreEqual("nature", s.Query);
            Assert.IsTrue(s.General);
            Assert.IsTrue(s.Anime);
            Assert.IsFalse(s.People);
            Assert.IsTrue(s.SFW);
            Assert.IsFalse(s.Sketchy);
            Assert.IsFalse(s.NSFW);
            Assert.AreEqual("relevance", s.Sorting);
            Assert.AreEqual("desc", s.Order);
            Assert.AreEqual("AtLeast", s.ResolutionMode);
        }

        [TestMethod]
        public void Settings_BuildUrl_FormatsParametersCorrectly()
        {
            var s = new WallhavenSearchSettings
            {
                Query = "cyberpunk",
                General = true,
                Anime = false,
                People = false,
                SFW = true,
                Sketchy = true,
                NSFW = false,
                Sorting = "toplist",
                Order = "desc",
                TopRange = "1M",
                ResolutionMode = "AtLeast",
                ImageWidth = 2560,
                ImageHeight = 1440,
                AspectRatio = "16x9",
                Color = "#660000"
            };

            string url = s.BuildUrl(1);

            Assert.IsTrue(url.StartsWith("https://wallhaven.cc/api/v1/search?"));
            Assert.IsTrue(url.Contains("q=cyberpunk"));
            Assert.IsTrue(url.Contains("categories=100"));
            Assert.IsTrue(url.Contains("purity=110"));
            Assert.IsTrue(url.Contains("sorting=toplist"));
            Assert.IsTrue(url.Contains("order=desc"));
            Assert.IsTrue(url.Contains("topRange=1M"));
            Assert.IsTrue(url.Contains("atleast=2560x1440"));
            Assert.IsTrue(url.Contains("ratios=16x9"));
            Assert.IsTrue(url.Contains("colors=660000"));
        }

        [TestMethod]
        public void Settings_BuildUrl_IncludesApiKey_WhenConfigured()
        {
            var s = new WallhavenSearchSettings
            {
                Query = "space",
                ApiKey = "my_secret_token"
            };

            string url = s.BuildUrl();
            Assert.IsTrue(url.Contains("apikey=my_secret_token"));
        }

        [TestMethod]
        public void Settings_BuildUrl_IncludesSeed_WhenProvided()
        {
            var s = new WallhavenSearchSettings
            {
                Sorting = "random"
            };

            string url = s.BuildUrl(page: 2, seed: "abc123");
            Assert.IsTrue(url.Contains("page=2"));
            Assert.IsTrue(url.Contains("seed=abc123"));
        }

        [TestMethod]
        public void Settings_BuildUrl_ExactResolutionMode_FormatsResolutionsParam()
        {
            var s = new WallhavenSearchSettings
            {
                ResolutionMode = "Exact",
                ImageWidth = 3840,
                ImageHeight = 2160
            };

            string url = s.BuildUrl();
            Assert.IsTrue(url.Contains("resolutions=3840x2160"));
            Assert.IsFalse(url.Contains("atleast="));
        }

        [TestMethod]
        public void Settings_XmlSerializationRoundTrip_PreservesAllFields()
        {
            var original = new WallhavenSearchSettings
            {
                Query = "synthwave",
                ApiKey = "key123",
                General = false,
                Anime = true,
                People = true,
                SFW = false,
                Sketchy = true,
                NSFW = false,
                Sorting = "views",
                Order = "asc",
                TopRange = "3M",
                ResolutionMode = "Exact",
                ImageWidth = 1920,
                ImageHeight = 1080,
                AspectRatio = "21x9",
                Color = "0066cc"
            };

            string xml = original.Save();
            var restored = WallhavenSearchSettings.LoadFromXML(xml);

            Assert.IsNotNull(restored);
            Assert.AreEqual(original.Query, restored.Query);
            Assert.AreEqual(original.ApiKey, restored.ApiKey);
            Assert.AreEqual(original.General, restored.General);
            Assert.AreEqual(original.Anime, restored.Anime);
            Assert.AreEqual(original.People, restored.People);
            Assert.AreEqual(original.SFW, restored.SFW);
            Assert.AreEqual(original.Sketchy, restored.Sketchy);
            Assert.AreEqual(original.NSFW, restored.NSFW);
            Assert.AreEqual(original.Sorting, restored.Sorting);
            Assert.AreEqual(original.Order, restored.Order);
            Assert.AreEqual(original.TopRange, restored.TopRange);
            Assert.AreEqual(original.ResolutionMode, restored.ResolutionMode);
            Assert.AreEqual(original.ImageWidth, restored.ImageWidth);
            Assert.AreEqual(original.ImageHeight, restored.ImageHeight);
            Assert.AreEqual(original.AspectRatio, restored.AspectRatio);
            Assert.AreEqual(original.Color, restored.Color);
        }

        // ── Provider Unit Tests ──────────────────────────────────────────────

        [TestMethod]
        public void GetPictures_DoesNotThrow_WhenNullConfig()
        {
            var ps = FakePictureSearch.Create(maxPictures: 0, providerConfig: null);
            var result = _provider.GetPictures(ps);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Pictures);
        }

        // ── Integration Tests (Live Wallhaven API v1) ─────────────────────────

        [TestMethod]
        public void GetPictures_Integration_LiveSearch_ReturnsValidPictures()
        {
            var settings = new WallhavenSearchSettings
            {
                Query = "nature",
                General = true,
                Anime = false,
                People = false,
                SFW = true,
                ResolutionMode = "None"
            };

            var ps = FakePictureSearch.Create(maxPictures: 5, providerConfig: settings.Save());
            var result = _provider.GetPictures(ps);

            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.Pictures.Count > 0, "Should return at least one picture from live Wallhaven API");
            Assert.IsTrue(result.Pictures.Count <= 5, "Should respect maxPictures limit");

            foreach (var pic in result.Pictures)
            {
                Assert.IsFalse(string.IsNullOrEmpty(pic.Url), "Picture Url must not be empty");
                Assert.IsTrue(pic.Url.StartsWith("https://", StringComparison.OrdinalIgnoreCase), "Url must be HTTPS");
                Assert.IsFalse(string.IsNullOrEmpty(pic.Id), "Picture Id must not be empty");
                Assert.IsTrue(pic.Id.StartsWith("wallhaven-"), "Picture Id must start with 'wallhaven-' prefix");
                Assert.IsTrue(pic.Properties.ContainsKey(Pulse.Base.Picture.StandardProperties.Thumbnail), "Should have thumbnail property");
                Assert.IsTrue(pic.Properties.ContainsKey(Pulse.Base.Picture.StandardProperties.Referrer), "Should have referrer property");
                Assert.AreEqual("Wallhaven", pic.Properties[Pulse.Base.Picture.StandardProperties.ProviderLabel]);
            }
        }

        [TestMethod]
        public void GetPictures_Integration_ExcludesBannedUrls()
        {
            var settings = new WallhavenSearchSettings
            {
                Query = "mountain",
                ResolutionMode = "None"
            };

            // First fetch 2 pictures to get known URLs
            var psInitial = FakePictureSearch.Create(maxPictures: 2, providerConfig: settings.Save());
            var initialResult = _provider.GetPictures(psInitial);

            if (initialResult.Pictures.Count == 0)
            {
                Assert.Inconclusive("API did not return pictures to test banning.");
            }

            var bannedUrl = initialResult.Pictures[0].Url;

            // Fetch again with the first picture URL banned
            var psBanned = FakePictureSearch.Create(maxPictures: 5, providerConfig: settings.Save());
            psBanned.BannedURLs.Add(bannedUrl);

            var filteredResult = _provider.GetPictures(psBanned);

            Assert.IsFalse(filteredResult.Pictures.Any(p => p.Url == bannedUrl),
                "Banned picture URL should not appear in results");
        }
    }
}
