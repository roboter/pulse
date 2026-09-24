using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaRSSProvider;
using Pulse.Base;
using Pulse.Tests.Helpers;

namespace Pulse.Tests.Providers
{
    /// <summary>
    /// Tests for the MediaRSS provider.
    ///
    /// Unit tests use synthetic in-memory XML to avoid network calls.
    /// Integration tests (marked [TestCategory("Integration")]) hit real feeds.
    /// </summary>
    [TestClass]
    public class MediaRssProviderTests
    {
        private Provider _provider;

        [TestInitialize]
        public void Setup() => _provider = new Provider();

        // ── helpers ───────────────────────────────────────────────────────────

        private const string MediaNs = "http://search.yahoo.com/mrss/";

        /// <summary>Builds a MediaRSS feed XML string with the given items.</summary>
        private static string BuildFeed(params (string imageUrl, string thumbUrl, string medium)[] items)
        {
            XNamespace media = MediaNs;
            var channel = new XElement("channel");
            foreach (var (imageUrl, thumbUrl, medium) in items)
            {
                var content = new XElement(media + "content",
                    new XAttribute("url", imageUrl));
                if (medium != null)
                    content.Add(new XAttribute("medium", medium));

                var item = new XElement("item", content);
                if (thumbUrl != null)
                    item.Add(new XElement(media + "thumbnail", new XAttribute("url", thumbUrl)));

                channel.Add(item);
            }
            return new XDocument(new XElement("rss", channel)).ToString();
        }

        private static string BuildSettingsXml(string feedUrl) =>
            $"<MediaRSSImageSearchSettings><MediaRSSURL>{feedUrl}</MediaRSSURL></MediaRSSImageSearchSettings>";

        // ── unit tests (no network) ───────────────────────────────────────────

        [TestMethod]
        public void GetPictures_ParsesImageItems_WithMediumAttribute()
        {
            // Use a local file URI with the feed XML
            var feedXml = BuildFeed(
                ("https://example.com/img1.jpg", "https://example.com/thumb1.jpg", "image"),
                ("https://example.com/img2.jpg", "https://example.com/thumb2.jpg", "image"));

            var tempFile = System.IO.Path.GetTempFileName() + ".xml";
            System.IO.File.WriteAllText(tempFile, feedXml);

            try
            {
                var config = BuildSettingsXml(tempFile);
                var ps = FakePictureSearch.Create(maxPictures: 10, providerConfig: config);
                var result = _provider.GetPictures(ps);

                Assert.AreEqual(2, result.Pictures.Count);
                Assert.AreEqual("https://example.com/img1.jpg", result.Pictures[0].Url);
            }
            finally
            {
                System.IO.File.Delete(tempFile);
            }
        }

        [TestMethod]
        public void GetPictures_FallsBackToThumbnail_WhenNoImageContent()
        {
            // Item has thumbnail but no media:content with medium="image"
            var feedXml = BuildFeed(("https://example.com/img.jpg", "https://example.com/thumb.jpg", null));
            var tempFile = System.IO.Path.GetTempFileName() + ".xml";
            System.IO.File.WriteAllText(tempFile, feedXml);

            try
            {
                var config = BuildSettingsXml(tempFile);
                var ps = FakePictureSearch.Create(maxPictures: 10, providerConfig: config);
                var result = _provider.GetPictures(ps);

                Assert.AreEqual(1, result.Pictures.Count, "Should fall back to thumbnail URL");
                Assert.AreEqual("https://example.com/thumb.jpg", result.Pictures[0].Url);
            }
            finally
            {
                System.IO.File.Delete(tempFile);
            }
        }

        [TestMethod]
        public void GetPictures_SkipsItems_WithNoResolvableUrl()
        {
            // Item with no thumbnail and no media:content — should be skipped
            XNamespace media = MediaNs;
            var emptyFeed = new XDocument(new XElement("rss",
                new XElement("channel",
                    new XElement("item",
                        new XElement(media + "content")) // no url attribute
                ))).ToString();

            var tempFile = System.IO.Path.GetTempFileName() + ".xml";
            System.IO.File.WriteAllText(tempFile, emptyFeed);

            try
            {
                var config = BuildSettingsXml(tempFile);
                var ps = FakePictureSearch.Create(maxPictures: 10, providerConfig: config);
                var result = _provider.GetPictures(ps);

                Assert.AreEqual(0, result.Pictures.Count, "Items with no URL should be skipped, not throw");
            }
            finally
            {
                System.IO.File.Delete(tempFile);
            }
        }

        [TestMethod]
        public void GetPictures_DoesNotThrow_WhenThumbnailElementMissing()
        {
            // Regression test for original NullReferenceException on line 44
            XNamespace media = MediaNs;
            var feed = new XDocument(new XElement("rss",
                new XElement("channel",
                    new XElement("item",
                        new XElement(media + "content",
                            new XAttribute("url", "https://example.com/img.jpg"),
                            new XAttribute("medium", "image")))
                ))).ToString();

            var tempFile = System.IO.Path.GetTempFileName() + ".xml";
            System.IO.File.WriteAllText(tempFile, feed);

            try
            {
                var config = BuildSettingsXml(tempFile);
                var ps = FakePictureSearch.Create(maxPictures: 10, providerConfig: config);
                // Should NOT throw NullReferenceException
                var result = _provider.GetPictures(ps);
                Assert.AreEqual(1, result.Pictures.Count);
            }
            finally
            {
                System.IO.File.Delete(tempFile);
            }
        }

        [TestMethod]
        public void GetPictures_TruncatesId_LongerThan50Chars()
        {
            var longName = new string('x', 60) + ".jpg";
            var feedXml = BuildFeed(($"https://example.com/{longName}", null, "image"));
            var tempFile = System.IO.Path.GetTempFileName() + ".xml";
            System.IO.File.WriteAllText(tempFile, feedXml);

            try
            {
                var config = BuildSettingsXml(tempFile);
                var ps = FakePictureSearch.Create(maxPictures: 10, providerConfig: config);
                var result = _provider.GetPictures(ps);

                Assert.IsTrue(result.Pictures[0].Id.Length <= 50, "Id should be capped at 50 characters");
            }
            finally
            {
                System.IO.File.Delete(tempFile);
            }
        }

        [TestMethod]
        public void GetPictures_RespectsMaxPictureCount()
        {
            var feedXml = BuildFeed(
                ("https://example.com/1.jpg", null, "image"),
                ("https://example.com/2.jpg", null, "image"),
                ("https://example.com/3.jpg", null, "image"));

            var tempFile = System.IO.Path.GetTempFileName() + ".xml";
            System.IO.File.WriteAllText(tempFile, feedXml);

            try
            {
                var config = BuildSettingsXml(tempFile);
                var ps = FakePictureSearch.Create(maxPictures: 2, providerConfig: config);
                var result = _provider.GetPictures(ps);

                Assert.AreEqual(2, result.Pictures.Count);
            }
            finally
            {
                System.IO.File.Delete(tempFile);
            }
        }

        // ── integration tests (live network) ─────────────────────────────────

        [TestMethod]
        [TestCategory("Integration")]
        public void GetPictures_Integration_DeviantArtFeed_ReturnsResults()
        {
            // Always pass explicit URL — the default constructor calls PictureManager.PrimaryScreenResolution
            // which requires a WinForms UI context and crashes outside the main app.
            var config = BuildSettingsXml(
                "https://backend.deviantart.com/rss.xml?q=boost%3Apopular+nature&type=deviation");

            var ps = FakePictureSearch.Create(maxPictures: 5, providerConfig: config);
            PictureList result;
            try
            {
                result = _provider.GetPictures(ps);
            }
            catch (System.Net.WebException ex)
            {
                Assert.Inconclusive($"DeviantArt RSS feed returned a web exception (likely cloud/CI bot protection): {ex.Message}");
                return;
            }

            Assert.IsTrue(result.Pictures.Count > 0, "Expected pictures from DeviantArt RSS");
            Assert.IsTrue(result.Pictures.All(p => !string.IsNullOrEmpty(p.Url)), "All pictures should have a URL");
        }
    }
}
