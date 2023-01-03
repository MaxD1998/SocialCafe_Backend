using Microsoft.Extensions.Configuration;

namespace ApplicationCore.Helpers;

public static class ConfigHelper
{
    public static IConfigurationRoot SetConfings()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();
    }
}