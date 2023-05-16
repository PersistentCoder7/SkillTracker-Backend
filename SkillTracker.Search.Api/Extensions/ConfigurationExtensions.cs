namespace SkillTracker.Search.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void TweakConfiguration(this WebApplicationBuilder builder)
        {

            // Retrieve the existing configuration from the builder
            var existingConfiguration = builder.Configuration;
            var environmentName = builder.Environment.EnvironmentName;
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables("ST_")
                .AddConfiguration(existingConfiguration)
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .Build();

            // Register the Configuration object as a service
            builder.Services.AddSingleton<IConfiguration>(configuration);
        }
    }
}
