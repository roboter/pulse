using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoogleImages;
using Pulse.Tests.Helpers;

namespace Pulse.Tests.Providers
{
    /// <summary>
    /// Tests for the Google Images provider powered by Google Custom Search JSON API.
    /// </summary>
    [TestClass]
    public class GoogleImagesProviderTests
    {
        private Provider _provider;

        [TestInitialize]
        public void Setup() => _provider = new Provider();

        // ── Settings Tests ───────────────────────────────────────────────────

        [TestMethod]
        public void Settings_DefaultValues_AreSensible()
        {
            var s = new GoogleImageSearchSettings();

            Assert.AreEqual("nature wallpaper", s.Query);
            Assert.AreEqual(string.Empty, s.ApiKey);
            Assert.AreEqual(string.Empty, s.SearchEngineId);
            Assert.AreEqual("large", s.ImageSize);
            Assert.AreEqual("any", s.ColorType);
            Assert.AreEqual("any", s.DominantColor);
            Assert.AreEqual("photo", s.ImageType);
            Assert.AreEqual(GoogleImageSearchSettings.GoogleSafeSearchOptions.On, s.GoogleSafeSearchOption);
        }

        [TestMethod]
        public void Settings_BuildUrl_FormatsParametersCorrectly()
        {
            var s = new GoogleImageSearchSettings
            {
                Query = "mountains 4k",
                ApiKey = "AIzaSyTestKey123",
                SearchEngineId = "0123456789:abcdefg",
                ImageSize = "xlarge",
                ColorType = "color",
                DominantColor = "blue",
                ImageType = "photo",
                GoogleSafeSearchOption = GoogleImageSearchSettings.GoogleSafeSearchOptions.On
            };

            string url = s.BuildUrl(startIndex: 11, count: 10);

            Assert.IsTrue(url.StartsWith("https://www.googleapis.com/customsearch/v1?"));
            Assert.IsTrue(url.Contains("searchType=image"));
            Assert.IsTrue(url.Contains("q=mountains%204k"));
            Assert.IsTrue(url.Contains("start=11"));
            Assert.IsTrue(url.Contains("num=10"));
            Assert.IsTrue(url.Contains("key=AIzaSyTestKey123"));
            Assert.IsTrue(url.Contains("cx=0123456789%3Aabcdefg") || url.Contains("cx=0123456789:abcdefg"));
            Assert.IsTrue(url.Contains("safe=active"));
            Assert.IsTrue(url.Contains("imgSize=xlarge"));
            Assert.IsTrue(url.Contains("imgColorType=color"));
            Assert.IsTrue(url.Contains("imgDominantColor=blue"));
            Assert.IsTrue(url.Contains("imgType=photo"));
        }

        [TestMethod]
        public void Settings_BuildUrl_SafeSearchOff_SetsSafeOff()
        {
            var s = new GoogleImageSearchSettings
            {
                Query = "sunset",
                GoogleSafeSearchOption = GoogleImageSearchSettings.GoogleSafeSearchOptions.Off
            };

            string url = s.BuildUrl();
            Assert.IsTrue(url.Contains("safe=off"));
        }

        [TestMethod]
        public void Settings_XmlSerializationRoundTrip_PreservesAllFields()
        {
            var original = new GoogleImageSearchSettings
            {
                Query = "aurora borealis",
                ApiKey = "SecretKey_XYZ",
                SearchEngineId = "cx_test_id",
                ImageSize = "huge",
                ColorType = "trans",
                DominantColor = "green",
                ImageType = "animated",
                GoogleSafeSearchOption = GoogleImageSearchSettings.GoogleSafeSearchOptions.Off
            };

            string xml = original.Save();
            var restored = GoogleImageSearchSettings.LoadFromXML(xml);

            Assert.IsNotNull(restored);
            Assert.AreEqual(original.Query, restored.Query);
            Assert.AreEqual(original.ApiKey, restored.ApiKey);
            Assert.AreEqual(original.SearchEngineId, restored.SearchEngineId);
            Assert.AreEqual(original.ImageSize, restored.ImageSize);
            Assert.AreEqual(original.ColorType, restored.ColorType);
            Assert.AreEqual(original.DominantColor, restored.DominantColor);
            Assert.AreEqual(original.ImageType, restored.ImageType);
            Assert.AreEqual(original.GoogleSafeSearchOption, restored.GoogleSafeSearchOption);
        }

        // ── Provider Defensive Unit Tests ────────────────────────────────────

        [TestMethod]
        public void GetPictures_ReturnsEmptyList_WhenQueryIsEmpty()
        {
            var s = new GoogleImageSearchSettings
            {
                Query = "",
                ApiKey = "AIzaSyTestKey123",
                SearchEngineId = "0123456789:abcdefg"
            };
            var ps = FakePictureSearch.Create(maxPictures: 10, providerConfig: s.Save());
            var result = _provider.GetPictures(ps);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Pictures.Count,
                "Provider should return empty list when no query is set");
        }

        [TestMethod]
        public void GetPictures_ReturnsEmptyList_WhenApiKeyOrCxMissing()
        {
            var s = new GoogleImageSearchSettings
            {
                Query = "landscape",
                ApiKey = "",
                SearchEngineId = ""
            };
            var ps = FakePictureSearch.Create(maxPictures: 10, providerConfig: s.Save());
            var result = _provider.GetPictures(ps);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Pictures.Count,
                "Provider should gracefully return empty list when API Key or CX is not set");
        }

        [TestMethod]
        public void GetPictures_SetsFetchDate_EvenWithEmptyQuery()
        {
            var s = new GoogleImageSearchSettings { Query = "" };
            var before = DateTime.Now.AddSeconds(-1);
            var ps = FakePictureSearch.Create(providerConfig: s.Save());
            var result = _provider.GetPictures(ps);

            Assert.IsTrue(result.FetchDate >= before, "FetchDate should always be set");
        }

        [TestMethod]
        public void GetPictures_DoesNotThrow_WhenNullConfig()
        {
            var ps = FakePictureSearch.Create(maxPictures: 0, providerConfig: "");
            var result = _provider.GetPictures(ps);
            Assert.IsNotNull(result);
        }

        // ── Integration Tests ────────────────────────────────────────────────

        [TestMethod]
        [TestCategory("Integration")]
        public void GetPictures_Integration_LiveSearch_WhenCredentialsProvided()
        {
            string apiKey = Environment.GetEnvironmentVariable("GOOGLE_API_KEY");
            string cx = Environment.GetEnvironmentVariable("GOOGLE_SEARCH_ENGINE_ID");

            if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(cx))
            {
                Assert.Inconclusive("Skipping live Google Custom Search test — GOOGLE_API_KEY and GOOGLE_SEARCH_ENGINE_ID environment variables are not set.");
                return;
            }

            var s = new GoogleImageSearchSettings
            {
                Query = "mountains",
                ApiKey = apiKey,
                SearchEngineId = cx,
                ImageSize = "large"
            };

            var ps = FakePictureSearch.Create(maxPictures: 3, providerConfig: s.Save());
            var result = _provider.GetPictures(ps);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Pictures.Count > 0, "Expected pictures from Google Custom Search API with valid credentials");
            foreach (var pic in result.Pictures)
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(pic.Url), "Picture URL should not be empty");
                Assert.IsTrue(pic.Url.StartsWith("http"), $"URL should be absolute: {pic.Url}");
                Assert.IsFalse(string.IsNullOrWhiteSpace(pic.Id), "Picture Id should not be empty");
            }
        }
    }
}

