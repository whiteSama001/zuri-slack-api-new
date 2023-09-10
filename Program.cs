using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using slack_api_1.Data;
using slack_api_1.Models;

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

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SlackContext>();

    var slack_name = "Whitesama";
    var track = "backend";
    DateTime currentDate = DateTime.Now;
    DayOfWeek currentDayOfWeek = currentDate.DayOfWeek;
    string dayOfWeekString = currentDayOfWeek.ToString();
    DateTime utcDateTime = DateTime.UtcNow;
    string formattedUtcDateTime = utcDateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");

    if (!context.slackData.Any())
    {
        context.slackData.Add(new slackModel
        {
            slack_name = slack_name,
            current_day = dayOfWeekString,
            utc_time = formattedUtcDateTime,
            track = track,
            github_file_url = "https://github.com/whiteSama001/zuri-slack-api/blob/main/Program.cs",
            github_repo_url = "https://github.com/whiteSama001/zuri-slack-api",
            status_code = 200,
        });
        context.SaveChanges();
    }
}

app.Run();
