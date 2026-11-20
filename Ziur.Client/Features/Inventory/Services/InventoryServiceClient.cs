using System.Net.Http.Json;
namespace Ziur.Client.Features.Inventory.Services;


    public class InventoryServiceClient
    {
    private readonly HttpClient _httpClient;
    public InventoryServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

    }

