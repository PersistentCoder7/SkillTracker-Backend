using MassTransit;
using SkillTracker.Common.MessageContracts.Messages;
using SkillTracker.Profile.Api.Utils;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Infrastructure.Consumers;
using Microsoft.Extensions.Logging; // Add this line
using Azure.Messaging.ServiceBus;

namespace SkillTracker.Profile.Api.Extensions
{
    public static class RabbitMQExtensions
    {
        public static void AddRabbitMQ(this WebApplicationBuilder builder, ILogger<Program> logger) // Add ILogger parameter
        {
            var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            var connectionString = configuration["ServiceBusConnectionString"];

            logger.LogInformation($"SB connection string: {connectionString}"); // Log the parsed connection string

            if (builder.Environment.EnvironmentName.Equals("Production")) // Check if message broker is "Cloud"
            {
                builder.Services.AddMassTransit(x =>
                {
                   ConfigureSBConsumers(x);
                   x.UsingAzureServiceBus((context, cfg) =>
                    {
                        cfg.Host(connectionString);
                        // Configure the queues for your consumers
                        //cfg.ReceiveEndpoint("add-profile-queue", e =>
                        //{
                        //    e.Consumer<AddProfileConsumer>(context);
                        //    e.AutoStart = true;
                        //});

                        //cfg.ReceiveEndpoint("update-profile-queue", e =>
                        //{
                        //    e.Consumer<UpdateProfileConsumer>(context);
                        //    e.AutoStart = true;
                        //});

                        //cfg.ReceiveEndpoint("refresh-cache-queue", e =>
                        //{
                        //    e.Consumer<RefreshCacheEventConsumer>(context);
                        //    e.AutoStart = true;
                        //});
                        cfg.ConfigureEndpoints(context);
                    });
                    
                });
            }
            else // Default to RabbitMQ if message broker is not "Cloud"
            {
                var parser = new RabbitMqConnectionStringParser();
                var rabbitConfig = parser.ParseConnectionString(connectionString);

                logger.LogInformation($"Host: {rabbitConfig.Host}, Port: {rabbitConfig.Port}"); // Log the parsed connection string
                builder.Services.AddMassTransit(x =>
                {
                    ConfigureSBConsumers(x);
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

        private static void ConfigureSBConsumers(IBusRegistrationConfigurator x)
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumer<AddProfileConsumer>(x => { });
            x.AddConsumer<UpdateProfileConsumer>(x => { });
            x.AddConsumer<RefreshCacheEventConsumer>(x => { });
        }
    }
}
