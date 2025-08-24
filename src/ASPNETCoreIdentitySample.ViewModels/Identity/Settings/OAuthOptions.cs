namespace ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
public sealed class OAuthOptions
{
    public GoogleOptions Google { get; set; } = new();
    public MicrosoftOptions Microsoft { get; set; } = new();
    public GitHubOptions GitHub { get; set; } = new();
}
