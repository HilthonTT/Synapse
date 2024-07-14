using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Infrastructure.Authentication;

internal sealed class JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions) : IPostConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        options.Authority = _jwtOptions.Authority;
        options.TokenValidationParameters = new()
        {
            ValidateAudience = false,
            NameClaimType = ClaimTypes.NameIdentifier,
        };
    }
}
