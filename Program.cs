using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using slack_api_1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.
builder.Services.AddDbContext<SlackContext>(opt =>
    opt.UseInMemoryDatabase("slackList"));
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "slack-api-one", Version = "v1" });
});

var app = builder.Build();

// Enable Swagger and Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "slack-api-one v1");
});

app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.

// Add authentication and authorization middleware here if needed

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
