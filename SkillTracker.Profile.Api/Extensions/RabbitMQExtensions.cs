using MassTransit;
using SkillTracker.Common.MessageContracts.Messages;
using SkillTracker.Profile.Api.Utils;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Infrastructure.Consumers;
using Microsoft.Extensions.Logging; // Add this line

namespace SkillTracker.Profile.Api.Extensions
{
    public static class RabbitMQExtensions
    {
        public static void AddRabbitMQ(this WebApplicationBuilder builder, ILogger<Program> logger) // Add ILogger parameter
        {
            var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            var connectionString = configuration["ServiceBusConnectionString"];

            logger.LogInformation($"SB connection string: {connectionString}"); // Log the parsed connection string

            var parser = new RabbitMqConnectionStringParser();
            var rabbitConfig = parser.ParseConnectionString(connectionString);

            logger.LogInformation($"Host: {rabbitConfig.Host}, Port:{rabbitConfig.Port}"); // Log the parsed connection string
            builder.Services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<AddProfileConsumer>(x =>
                {

                });
                x.AddConsumer<UpdateProfileConsumer>(x =>
                {

                });
                x.AddConsumer<RefreshCacheEventConsumer>(x =>
                {

                });
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitConfig.Host, "/", px =>
                    {
                        px.Username(rabbitConfig.Username);
                        px.Password(rabbitConfig.Password);
                    });
                    cfg.ConfigureEndpoints(context);

                });
            });

        }
    }
}
