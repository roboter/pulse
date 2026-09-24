using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NASAAPOD;
using Pulse.Tests.Helpers;

namespace Pulse.Tests.Providers
{
    /// <summary>
    /// Tests for the NASA APOD provider.
    /// All tests are integration tests as this provider relies on scraping apod.nasa.gov.
    /// Run with [TestCategory("Integration")] to allow skipping in CI without network.
    /// </summary>
    [TestClass]
    public class NasaApodProviderTests
    {
        private NASAAPODProviderza _provider;

        [TestInitialize]
        public void Setup() => _provider = new NASAAPODProviderza();

        [TestMethod]
        [TestCategory("Integration")]
        public void GetPictures_ReturnsNonEmptyList()
        {
            var ps = FakePictureSearch.Create(maxPictures: 3);
            var result = _provider.GetPictures(ps);

            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.Pictures.Count > 0, "Should return at least one picture");
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetPictures_RespectsMaxPictureCount()
        {
            var ps = FakePictureSearch.Create(maxPictures: 2);
            var result = _provider.GetPictures(ps);

            Assert.IsTrue(result.Pictures.Count <= 2, "Should not exceed MaxPictureCount");
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetPictures_AllPicturesHaveValidUrls()
        {
            var ps = FakePictureSearch.Create(maxPictures: 3);
            var result = _provider.GetPictures(ps);

            foreach (var pic in result.Pictures)
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(pic.Url),
                    $"Picture URL should not be empty");
                Assert.IsTrue(pic.Url.StartsWith("http"),
                    $"URL should be absolute: {pic.Url}");
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetPictures_AllPicturesHaveIds()
        {
            var ps = FakePictureSearch.Create(maxPictures: 3);
            var result = _provider.GetPictures(ps);

            foreach (var pic in result.Pictures)
                Assert.IsFalse(string.IsNullOrWhiteSpace(pic.Id),
                    "Each picture should have a non-empty Id");
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetPictures_ExcludesBannedUrls()
        {
            // First fetch to find a real URL to ban
            var ps1 = FakePictureSearch.Create(maxPictures: 3);
            var initial = _provider.GetPictures(ps1);
            if (initial.Pictures.Count == 0)
                Assert.Inconclusive("No pictures returned — cannot test ban filtering");

            var banned = initial.Pictures[0].Url;

            var ps2 = FakePictureSearch.Create(maxPictures: 3, bannedUrls: new[] { banned });
            var result = _provider.GetPictures(ps2);

            Assert.IsFalse(result.Pictures.Any(p => p.Url == banned),
                "Banned URL should not appear in results");
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetPictures_SetsFetchDate()
        {
            var before = DateTime.Now.AddSeconds(-2);
            var ps = FakePictureSearch.Create(maxPictures: 1);
            var result = _provider.GetPictures(ps);

            Assert.IsTrue(result.FetchDate >= before, "FetchDate should be set to roughly now");
        }
    }
}
