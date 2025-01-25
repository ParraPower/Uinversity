using University.Extensions;
using UniversityData.Extensions;
using EventBusModule.DI;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseUrls(new[] { "http://0.0.0.0:5041" });

// Add services to the container.
builder.Services.EventBusModuleDIAdd();
builder.Services.ConfigureExceptionManager();
builder.Services.ConfigureRedisCache(builder.Configuration);
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureUniversityLoggerService(builder.Configuration);
builder.Services.ConfigureDataManagers();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureMediatR();
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

app.ApplyDatabaseMigrations();

app.Run();
