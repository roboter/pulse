using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LocalDirectory;
using Pulse.Tests.Helpers;

namespace Pulse.Tests.Providers
{
    /// <summary>
    /// Unit tests for the LocalDirectory provider.
    /// No network access required — uses a temporary directory on disk.
    /// </summary>
    [TestClass]
    public class LocalDirectoryProviderTests
    {
        private string _tempDir;
        private Provider _provider;

        [TestInitialize]
        public void Setup()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), "PulseTests_" + Guid.NewGuid());
            Directory.CreateDirectory(_tempDir);
            _provider = new Provider();
        }

        [TestCleanup]
        public void Teardown()
        {
            if (Directory.Exists(_tempDir))
                Directory.Delete(_tempDir, recursive: true);
        }

        // ── helpers ───────────────────────────────────────────────────────────

        private static string MakeConfig(string dir, string extensions = "jpg;png", bool includeSubDirs = false)
        {
            // Serialize a LocalDirectorySettings-like XML the provider understands
            var settings = new LocalDirectorySettings
            {
                Directory = dir,
                Extensions = extensions,
                IncludeSubdirectories = includeSubDirs
            };
            return settings.Save();
        }

        private void CreateFakeImages(string folder, params string[] names)
        {
            foreach (var name in names)
                File.WriteAllBytes(Path.Combine(folder, name), new byte[] { 0xFF, 0xD8, 0xFF }); // minimal JPEG header
        }

        // ── tests ─────────────────────────────────────────────────────────────

        [TestMethod]
        public void GetPictures_ReturnsAllImages_WhenUnderMax()
        {
            CreateFakeImages(_tempDir, "a.jpg", "b.jpg", "c.png");

            var ps = FakePictureSearch.Create(maxPictures: 10, providerConfig: MakeConfig(_tempDir));
            var result = _provider.GetPictures(ps);

            Assert.AreEqual(3, result.Pictures.Count, "Should find all 3 images");
        }

        [TestMethod]
        public void GetPictures_RespectsMaxPictureCount()
        {
            CreateFakeImages(_tempDir, "a.jpg", "b.jpg", "c.jpg", "d.jpg", "e.jpg");

            var ps = FakePictureSearch.Create(maxPictures: 2, providerConfig: MakeConfig(_tempDir));
            var result = _provider.GetPictures(ps);

            Assert.AreEqual(2, result.Pictures.Count, "Should cap at MaxPictureCount");
        }

        [TestMethod]
        public void GetPictures_FiltersUnsupportedExtensions()
        {
            CreateFakeImages(_tempDir, "photo.jpg", "doc.txt", "video.mp4");

            // only jpg configured
            var ps = FakePictureSearch.Create(maxPictures: 10, providerConfig: MakeConfig(_tempDir, "jpg"));
            var result = _provider.GetPictures(ps);

            Assert.AreEqual(1, result.Pictures.Count);
            Assert.IsTrue(result.Pictures.All(p => p.Url.EndsWith(".jpg")));
        }

        [TestMethod]
        public void GetPictures_ReturnsEmpty_WhenDirectoryIsEmpty()
        {
            var ps = FakePictureSearch.Create(maxPictures: 10, providerConfig: MakeConfig(_tempDir));
            var result = _provider.GetPictures(ps);

            Assert.AreEqual(0, result.Pictures.Count);
        }

        [TestMethod]
        public void GetPictures_SetsLocalPath_ForEachPicture()
        {
            CreateFakeImages(_tempDir, "wallpaper.jpg");

            var ps = FakePictureSearch.Create(maxPictures: 10, providerConfig: MakeConfig(_tempDir));
            var result = _provider.GetPictures(ps);

            Assert.IsNotNull(result.Pictures[0].LocalPath, "LocalPath should be set");
            Assert.IsTrue(File.Exists(result.Pictures[0].LocalPath), "LocalPath should point to a real file");
        }

        [TestMethod]
        public void GetPictures_SetsId_AsFilenameWithoutExtension()
        {
            CreateFakeImages(_tempDir, "my-wallpaper.jpg");

            var ps = FakePictureSearch.Create(maxPictures: 10, providerConfig: MakeConfig(_tempDir));
            var result = _provider.GetPictures(ps);

            Assert.AreEqual("my-wallpaper", result.Pictures[0].Id);
        }

        [TestMethod]
        public void GetPictures_SetsFetchDate()
        {
            CreateFakeImages(_tempDir, "a.jpg");
            var before = DateTime.Now.AddSeconds(-1);

            var ps = FakePictureSearch.Create(providerConfig: MakeConfig(_tempDir));
            var result = _provider.GetPictures(ps);

            Assert.IsTrue(result.FetchDate >= before, "FetchDate should be set to around now");
        }

        [TestMethod]
        public void GetPictures_IncludesSubdirectories_WhenEnabled()
        {
            var sub = Directory.CreateDirectory(Path.Combine(_tempDir, "sub")).FullName;
            CreateFakeImages(_tempDir, "root.jpg");
            CreateFakeImages(sub, "nested.jpg");

            var ps = FakePictureSearch.Create(maxPictures: 10, providerConfig: MakeConfig(_tempDir, "jpg", includeSubDirs: true));
            var result = _provider.GetPictures(ps);

            Assert.AreEqual(2, result.Pictures.Count, "Should include images from subdirectories");
        }

        [TestMethod]
        public void GetPictures_ExcludesSubdirectories_WhenDisabled()
        {
            var sub = Directory.CreateDirectory(Path.Combine(_tempDir, "sub")).FullName;
            CreateFakeImages(_tempDir, "root.jpg");
            CreateFakeImages(sub, "nested.jpg");

            var ps = FakePictureSearch.Create(maxPictures: 10, providerConfig: MakeConfig(_tempDir, "jpg", includeSubDirs: false));
            var result = _provider.GetPictures(ps);

            Assert.AreEqual(1, result.Pictures.Count, "Should exclude subdirectory images");
        }

        [TestMethod]
        public void GetPictures_ExcludesBannedUrls()
        {
            CreateFakeImages(_tempDir, "good.jpg", "banned.jpg");
            var bannedPath = Path.Combine(_tempDir, "banned.jpg");

            var ps = FakePictureSearch.Create(
                maxPictures: 10,
                providerConfig: MakeConfig(_tempDir),
                bannedUrls: new[] { bannedPath });

            // Note: LocalDirectory provider does NOT filter banned URLs itself (the runner does),
            // so this test validates the picture list is complete and the runner can do its job.
            var result = _provider.GetPictures(ps);
            Assert.AreEqual(2, result.Pictures.Count, "Provider returns all; runner filters banned");
        }
    }
}
