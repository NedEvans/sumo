using Sumo.Components;
using Sumo.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add DbContext with connection string=====================================
var dbServer = Environment.GetEnvironmentVariable("dbServer");
var dbName = Environment.GetEnvironmentVariable("dbName");
var dbPassword = Environment.GetEnvironmentVariable("dbPassword");

var connString = $"Data Source={dbServer}; Initial Catalog={dbName};User ID=sa;Password={dbPassword}; Encrypt=False; TrustServerCertificate=True";

builder.Services.AddDbContext<SumoDbContext>(options =>
    options.UseSqlServer(
        connString,
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,                // how many retries
            maxRetryDelay: TimeSpan.FromSeconds(20), // wait between
            errorNumbersToAdd: null          // custom SQL errors, usually leave null
        )
    ));
//==========================================================================



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
