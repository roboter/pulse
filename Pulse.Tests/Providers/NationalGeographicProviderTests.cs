using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NationalGeographicWallpapers;
using Pulse.Tests.Helpers;

namespace Pulse.Tests.Providers
{
    /// <summary>
    /// Tests for the National Geographic provider.
    /// Integration tests require live network access.
    /// </summary>
    [TestClass]
    public class NationalGeographicProviderTests
    {
        private NationalGeographicProvider _provider;

        [TestInitialize]
        public void Setup() => _provider = new NationalGeographicProvider();

        [TestMethod]
        [TestCategory("Integration")]
        [Ignore("ngm.nationalgeographic.com returns 403 — provider URL needs updating")]
        public void GetPictures_ReturnsNonEmptyList()
        {
            var ps = FakePictureSearch.Create(maxPictures: 5);
            var result = _provider.GetPictures(ps);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Pictures.Count > 0, "Should return at least one picture from NatGeo");
        }

        [TestMethod]
        [TestCategory("Integration")]
        [Ignore("ngm.nationalgeographic.com returns 403 — provider URL needs updating")]
        public void GetPictures_AllPicturesHaveAbsoluteUrls()
        {
            var ps = FakePictureSearch.Create(maxPictures: 5);
            var result = _provider.GetPictures(ps);

            foreach (var pic in result.Pictures)
            {
                Assert.IsFalse(string.IsNullOrEmpty(pic.Url), "URL should not be empty");
                Assert.IsTrue(pic.Url.StartsWith("http"), $"URL should be absolute: {pic.Url}");
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        [Ignore("ngm.nationalgeographic.com returns 403 — provider URL needs updating")]
        public void GetPictures_AllPicturesHaveIds()
        {
            var ps = FakePictureSearch.Create(maxPictures: 5);
            var result = _provider.GetPictures(ps);

            foreach (var pic in result.Pictures)
                Assert.IsFalse(string.IsNullOrEmpty(pic.Id), "Each picture should have an Id");
        }

        [TestMethod]
        [TestCategory("Integration")]
        [Ignore("ngm.nationalgeographic.com returns 403 — provider URL needs updating")]
        public void GetPictures_RespectsMaxPictureCount()
        {
            var ps = FakePictureSearch.Create(maxPictures: 3);
            var result = _provider.GetPictures(ps);

            Assert.IsTrue(result.Pictures.Count <= 3, "Should not exceed MaxPictureCount");
        }

        [TestMethod]
        [TestCategory("Integration")]
        [Ignore("ngm.nationalgeographic.com returns 403 — provider URL needs updating")]
        public void GetPictures_ExcludesBannedUrls()
        {
            var initial = _provider.GetPictures(FakePictureSearch.Create(maxPictures: 5));
            if (initial.Pictures.Count == 0)
                Assert.Inconclusive("No pictures returned — cannot test ban filtering");

            var banned = initial.Pictures[0].Url;
            var ps = FakePictureSearch.Create(maxPictures: 5, bannedUrls: new[] { banned });
            var result = _provider.GetPictures(ps);

            Assert.IsFalse(result.Pictures.Any(p => p.Url == banned),
                "Banned picture should not appear in results");
        }

        [TestMethod]
        [TestCategory("Integration")]
        [Ignore("ngm.nationalgeographic.com returns 403 — provider URL needs updating")]
        public void GetPictures_SetsFetchDate()
        {
            var before = DateTime.Now.AddSeconds(-2);
            var ps = FakePictureSearch.Create(maxPictures: 1);
            var result = _provider.GetPictures(ps);

            Assert.IsTrue(result.FetchDate >= before, "FetchDate should be set to roughly now");
        }
    }
}
