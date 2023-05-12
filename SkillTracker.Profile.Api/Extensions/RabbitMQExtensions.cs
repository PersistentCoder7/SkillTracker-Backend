using MassTransit;
using SkillTracker.Common.MessageContracts.Messages;
using SkillTracker.Profile.Api.Utils;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Infrastructure.Consumers;

namespace SkillTracker.Profile.Api.Extensions
{
    public static class RabbitMQExtensions
    {
        public static void AddRabbitMQ(this WebApplicationBuilder builder)
        {
            var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var hostName = Environment.GetEnvironmentVariable("ServiceBusHost");
            var connectionString = configuration["ServiceBusConnectionString"].Replace("<host>", hostName);

            var parser = new RabbitMqConnectionStringParser();
            var rabbitConfig = parser.ParseConnectionString(connectionString);

            builder.Services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<AddProfileConsumer>(x =>
                {
                    
                });
                x.AddConsumer<UpdateProfileConsumer>(x =>
                {
                    
                });
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitConfig.Host,"/",px =>
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
