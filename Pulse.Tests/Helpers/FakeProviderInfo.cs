using Pulse.Base;

namespace Pulse.Tests.Helpers
{
    /// <summary>
    /// Minimal fake ActiveProviderInfo so providers can be constructed without
    /// loading the full ProviderManager / plugin system.
    /// </summary>
    public static class FakeProviderInfo
    {
        public static ActiveProviderInfo Create(string providerConfig = "")
        {
            return new ActiveProviderInfo("Test")
            {
                ProviderConfig = providerConfig ?? string.Empty,
                Active = true
            };
        }
    }
}
