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
    services.AddDbContext<ProfileDbContext>(options =>
    {
        var ConnectionString = @"AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="; //Configuration.GetConnectionString("");
        
        var dbOptions = options.UseCosmos(@"https://localhost:8081/", @"C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", "SkillTracker").Options; 
        //Action<DbContextOptions> PopulateData = async (dbOptions) =>
        //{

        //    await ProfileDbContext.SeedInitalDataSync(dbOptions);

        //};
        //PopulateData(dbOptions);
    });
    DependencyContainer.RegisterServices(services);
}