using Microsoft.EntityFrameworkCore;
using React_project.Server.Context;
using React_project.Server.Repositories;
using Sentry.AspNet;
using Sentry.Profiling;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
// Configure Sentry
SentrySdk.Init(o =>
{
    o.Dsn = "https://bef8d3224ee8da4a6336d554d3d91049@o4507912401715200.ingest.de.sentry.io/4507954158829648";
    o.Environment = env.IsDevelopment() ? "Development" : "Production";
    // When configuring for the first time, to see what the SDK is doing:
    o.Debug = true;
    // Set TracesSampleRate to 1.0 to capture 100%
    // of transactions for tracing.
    // We recommend adjusting this value in production
    o.TracesSampleRate = 1.0;
    // If you are using EF (and installed the NuGet package):
    o.AddEntityFramework();
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

LifeCicleRepositories.LifeCicleRepositoriesConfiguration(builder.Services);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddControllers();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
