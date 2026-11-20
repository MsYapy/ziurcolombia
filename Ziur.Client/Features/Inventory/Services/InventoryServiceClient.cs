using Ziur.Shared.Models.DTOs;
using System.Net.Http.Json;

namespace Ziur.Client.Features.Inventory.Services;

/// <summary>
/// Proxy service that communicates with the server-side InventoryService
/// </summary>
public class InventoryServiceClient
{
    private readonly HttpClient _httpClient;

    public InventoryServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<List<MovementDto>> GetMovementsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // Call the server endpoint
            var movements = await _httpClient.GetFromJsonAsync<List<MovementDto>>(
                "api/inventory/movements", 
                cancellationToken);
            
            return movements ?? new List<MovementDto>();
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            // Self-cancellation - let it propagate silently
            throw;
        }
        catch (HttpRequestException httpEx)
        {
            System.Diagnostics.Debug.WriteLine($"HTTP Error: {httpEx.Message}");
            throw new ApplicationException("Error al consumir la API de inventario. Por favor, intente más tarde.", httpEx);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Unexpected error: {ex.Message}");
            throw new ApplicationException("Un error inesperado ocurrió. Por favor, intente más tarde.", ex);
        }
    }
}
