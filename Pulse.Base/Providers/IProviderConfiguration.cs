namespace Pulse.Base
{
    public interface IProviderConfigurationEditor : IProviderConfiguration
    {
        bool IsOK { get; set; }

        void HostMe(object parent);
    }

    public interface IProviderConfiguration
    {
        void LoadConfiguration(string config);
        string SaveConfiguration();
    }
}
