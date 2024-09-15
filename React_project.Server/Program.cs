using Microsoft.EntityFrameworkCore;
using React_project.Server.Context;
using React_project.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);
Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        // Add the following line:
        webBuilder.UseSentry(o =>
        {
            o.Dsn = "https://bef8d3224ee8da4a6336d554d3d91049@o4507912401715200.ingest.de.sentry.io/4507954158829648";
            o.Debug = true;
            o.TracesSampleRate = 1.0;
            o.ProfilesSampleRate = 1.0;
        });
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
