using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Ziur.Client.Features.Inventory.Services;
using IgniteUI.Blazor.Controls;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register IgniteUI modules
builder.Services.AddIgniteUIBlazor();

// Register HttpClient for WebAssembly components
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// Note: InventoryServiceClient is NOT registered here because CatalogPage.razor uses @rendermode InteractiveServer
// which resolves services from the server container, not the WebAssembly client container

await builder.Build().RunAsync();
