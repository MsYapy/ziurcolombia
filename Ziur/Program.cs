using System.Net.Http.Headers;
using Ziur.Client.Features.Inventory.Services;
using Ziur.Components;
using Ziur.Infrastructure.Configuration;
using Ziur.Infrastructure.Services;
using Ziur.Components;
using Ziur.Infrastructure.Configuration;
using Ziur.Infrastructure.Services;

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

// Register InventoryServiceClient for InteractiveServer components
// The HttpClient needs to be configured with the server's base address
builder.Services.AddScoped<InventoryServiceClient>(sp =>
{
    // Get the IHttpContextAccessor to access the current request's host
    var httpContextAccessor = sp.GetService<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient();

    // If we have an HTTP context (during a request), use it to get the base URL
    if (httpContextAccessor?.HttpContext != null)
    {
        var request = httpContextAccessor.HttpContext.Request;
        var baseUrl = $"{request.Scheme}://{request.Host}";
        httpClient.BaseAddress = new Uri(baseUrl);
    }
    else
    {
        // Fallback to configured URL or default
        var applicationUrl = builder.Configuration["ASPNETCORE_URLS"];
        if (!string.IsNullOrEmpty(applicationUrl))
        {
            // Take the first URL if multiple are configured
            var firstUrl = applicationUrl.Split(';')[0];
            httpClient.BaseAddress = new Uri(firstUrl);
        }
        else
        {
            // Default fallback
            httpClient.BaseAddress = new Uri("https://localhost:7164");
        }
    }

    return new InventoryServiceClient(httpClient);
});

// Add HttpContextAccessor for accessing the current request's context
builder.Services.AddHttpContextAccessor();

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
