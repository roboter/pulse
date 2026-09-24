using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoogleImages;
using Pulse.Tests.Helpers;

namespace Pulse.Tests.Providers
{
    /// <summary>
    /// Tests for the Google Images provider.
    ///
    /// NOTE: Google Images actively blocks scraping with bot detection (returns HTML
    /// with no image URLs). Unit tests verify the provider's defensive logic;
    /// integration tests document the known bot-wall behaviour.
    /// </summary>
    [TestClass]
    public class GoogleImagesProviderTests
    {
        private Provider _provider;

        [TestInitialize]
        public void Setup() => _provider = new Provider();

        // ── helpers ───────────────────────────────────────────────────────────

        private static string BuildConfig(string query = "nature wallpaper",
            int width = 0, int height = 0, string color = "")
        {
            var s = new GoogleImageSearchSettings
            {
                Query = query,
                ImageWidth = width,
                ImageHeight = height,
                Color = color
            };
            return s.Save();
        }

        // ── unit tests ────────────────────────────────────────────────────────

        [TestMethod]
        public void GetPictures_ReturnsEmptyList_WhenQueryIsEmpty()
        {
            // Provider should short-circuit when no query is configured
            var config = BuildConfig(query: "");
            var ps = FakePictureSearch.Create(maxPictures: 10, providerConfig: config);
            var result = _provider.GetPictures(ps);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Pictures.Count,
                "Provider should return empty list when no query is set");
        }

        [TestMethod]
        public void GetPictures_SetsFetchDate_EvenWithEmptyQuery()
        {
            var config = BuildConfig(query: "");
            var before = DateTime.Now.AddSeconds(-1);
            var ps = FakePictureSearch.Create(providerConfig: config);
            var result = _provider.GetPictures(ps);

            Assert.IsTrue(result.FetchDate >= before, "FetchDate should always be set");
        }

        [TestMethod]
        public void GetPictures_DoesNotThrow_WhenNullConfig()
        {
            // Provider falls back to default settings when config is null/empty
            var ps = FakePictureSearch.Create(maxPictures: 0, providerConfig: "");
            // Should not throw — just return empty
            var result = _provider.GetPictures(ps);
            Assert.IsNotNull(result);
        }

        // ── integration tests ─────────────────────────────────────────────────

        [TestMethod]
        [TestCategory("Integration")]
        public void GetPictures_Integration_GoogleBotWall_ReturnsEmpty()
        {
            // Google Images blocks scraping — this test documents the known failure.
            // If this starts passing, the scraping works again and tests should be updated.
            var config = BuildConfig("nature wallpaper");
            var ps = FakePictureSearch.Create(maxPictures: 5, providerConfig: config);
            var result = _provider.GetPictures(ps);

            // We expect 0 because Google bot-walls the request.
            // If this assertion fails, Google may have relaxed bot detection — re-evaluate.
            Assert.AreEqual(0, result.Pictures.Count,
                "Google Images scraping is blocked. If this fails, the provider may be working again.");
        }
    }
}
