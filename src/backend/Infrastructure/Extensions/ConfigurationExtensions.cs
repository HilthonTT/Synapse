using Microsoft.Extensions.Configuration;

namespace Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static string GetConnectionStringOrThrow(this IConfiguration configuration, string name)
    {
        string? connectionString = configuration.GetConnectionString(name);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"Connection string of name '{name}' was not provided in the appsettings.json");
        }

        return connectionString;
    }

    public static string GetValueOrThrow(this IConfiguration configuration, string key)
    {
        string? value = configuration[key];
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException(
                $"The value of key '{key}' was not provided in the appsettings.json");
        }

        return value;
    }
}
