using System.Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SkillTracker.Infrastructure.IoC;
using SkillTracker.Profile.Data.DbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Profile Service", Version ="V1"});
});

builder.Services.AddMediatR(typeof(Program));

RegisterServices(builder.Services);
var app = builder.Build();




// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Profile Microservice V1");
    });
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void RegisterServices(IServiceCollection services)
{
    //services.AddDbContext<ProfileDbContext>(options =>
    //{
    //    var ConnectionString = "fdsfsd"; //Configuration.GetConnectionString("");
    //    options.UseCosmos(ConnectionString);

    //});
    DependencyContainer.RegisterServices(services);
}