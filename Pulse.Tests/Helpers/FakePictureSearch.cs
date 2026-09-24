using System.Collections.Generic;
using Pulse.Base;

namespace Pulse.Tests.Helpers
{
    /// <summary>
    /// Factory for building PictureSearch instances for tests.
    /// </summary>
    public static class FakePictureSearch
    {
        /// <summary>
        /// Creates a basic PictureSearch with sane defaults.
        /// </summary>
        /// <param name="maxPictures">Max pictures to fetch (default 5 to keep tests fast)</param>
        /// <param name="providerConfig">Optional provider-specific XML config string</param>
        /// <param name="bannedUrls">Optional banned URLs list</param>
        public static PictureSearch Create(
            int maxPictures = 5,
            string providerConfig = "",
            IEnumerable<string> bannedUrls = null)
        {
            var ps = new PictureSearch
            {
                MaxPictureCount = maxPictures,
                SearchProvider = FakeProviderInfo.Create(providerConfig)
            };

            if (bannedUrls != null)
                ps.BannedURLs.AddRange(bannedUrls);

            return ps;
        }
    }
}
