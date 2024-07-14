namespace Infrastructure.Authentication;

internal sealed class JwtOptions
{
    public string Authority { get; set; } = string.Empty;

    public string AuthorizedParty { get; set; } = string.Empty;
}
