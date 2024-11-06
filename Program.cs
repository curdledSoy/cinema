using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using cinema.Helpers;
using cinema.Data;
using cinema.Data.Repo.Interfaces;
using cinema.Data.Repo;
using cinema.Data.Repositories;
using cinema.Services;
using cinema.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CinemaBookingContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PSQLConnection"))
);

//DI Model Repositories
builder.Services.AddScoped<ICinemaRepo, CinemaRepo>();
builder.Services.AddScoped<ITheaterRepo, TheaterRepo>();
builder.Services.AddScoped<IMovieRepo, MovieRepo>();
builder.Services.AddScoped<IScreeningRepo, ScreeningRepo>();

//DI Services
builder.Services.AddScoped<ICinemaService, CinemaService>();

//DTO Mapping
builder.Services.AddAutoMapper(typeof(DTOMapper));

//OpenAPI 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Development environment: Enable Swagger and proxy requests to Vite dev server
    app.UseSwagger();
    app.UseSwaggerUI();

    ViteHelper.EnsureViteDevServerRunning();

    app.UseSpa(spa =>
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:5173"); // Vite dev server URL
        spa.Options.SourcePath = "clientapp";
    });

    // Stop Vite dev server on app shutdown
    var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
    lifetime.ApplicationStopping.Register(ViteHelper.StopViteDevServer);
}
else
{
    // Production environment: Enable HTTPS and serve static files from wwwroot
    app.UseHsts();
    app.UseHttpsRedirection();
    app.UseSpaStaticFiles();
    app.UseSpa(spa =>
    {
        spa.Options.SourcePath = "wwwroot"; // Serve built SPA files from wwwroot
    });
}

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapFallbackToFile("index.html");

app.Run();
