namespace Start.Security
{
    public class ConfigurationLoginProviders : ILoginProviders
    {
        public ConfigurationLoginProviders()
        {
            Facebook = GetProvider("Facebook");
            Google = GetProvider("Google");
            Khulnasoft = GetProvider("Khulnasoft");
            Twitter = GetProvider("Twitter");
        }

        private ILoginProviderCredentials GetProvider(string providerName)
        {
            return new ConfigurationLoginProviderCredentials(providerName);
        }

        public ILoginProviderCredentials Facebook { get; private set; }
        public ILoginProviderCredentials Google { get; private set; }
        public ILoginProviderCredentials Khulnasoft { get; private set; }
        public ILoginProviderCredentials Twitter { get; private set; }
    }
}