using Ziur.Components;
using Ziur.Infrastructure.Services;
using Ziur.Infrastructure.Configuration;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Configure API settings
var apiSettings = builder.Configuration.GetSection(ApiSettings.SectionName).Get<ApiSettings>() 
    ?? throw new InvalidOperationException("API settings are not configured.");

// Use secure configuration - credentials should never be in appsettings
// Instead, use user secrets (development) or Azure Key Vault/environment variables (production)
var url = apiSettings.BaseUrl;
var token = apiSettings.Token;

if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(token))
{
    throw new InvalidOperationException("BaseUrl and Token must be configured in API settings.");
}

builder.Services.AddHttpClient<InventoryService>(client =>
{
    client.BaseAddress = new Uri(url);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    // Add security headers
    client.DefaultRequestHeaders.Add("X-Content-Type-Options", "nosniff");
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add controllers for API endpoints
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

// Map API endpoints
app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Ziur.Client._Imports).Assembly);

app.Run();

// Make Program class accessible for integration testing
