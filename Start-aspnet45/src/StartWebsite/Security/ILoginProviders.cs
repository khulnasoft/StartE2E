namespace Start.Security
{
    public interface ILoginProviders
    {
        ILoginProviderCredentials Facebook { get; }
        ILoginProviderCredentials Google { get; }
        ILoginProviderCredentials Khulnasoft { get; }
        ILoginProviderCredentials Twitter { get; }
    }
}