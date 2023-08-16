using NLog;
using LoggerService.Extensions;
using Uinversity.Extensions;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

//builder.WebHost.UseUrls(new[] { "http://0.0.0.0:5041" });

// Add services to the container.
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureExceptionManager();
builder.Services.ConfigureRedisCache(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.ConfigureCustomExceptionMiddleware();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();

app.Run();
