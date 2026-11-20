using Ziur.Shared.Models.DTOs;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Ziur.Infrastructure.Interfaces;

namespace Ziur.Infrastructure.Services;

public class InventoryService : IInventoryService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<InventoryService> _logger;
    private static readonly string[] AllowedContentTypes = { "application/json" };

    public InventoryService(HttpClient httpClient, ILogger<InventoryService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<MovementDto>> GetMovementsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Fetching movements from Ziur API");
            
            var response = await _httpClient.GetAsync("DocumentosFillsCombos", cancellationToken);

            _logger.LogDebug("API Response Status: {StatusCode}", response.StatusCode);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("API request failed with status code {StatusCode}", response.StatusCode);
                throw new HttpRequestException($"API request failed with status code {response.StatusCode}.");
            }

            // Validate content type for security
            var contentType = response.Content.Headers.ContentType?.MediaType ?? string.Empty;
            if (!AllowedContentTypes.Contains(contentType))
            {
                _logger.LogError("Invalid content type received: {ContentType}", contentType);
                throw new InvalidOperationException("Invalid response content type.");
            }

            var movements = await response.Content.ReadFromJsonAsync<List<MovementDto>>(cancellationToken);
            
            _logger.LogInformation("Successfully fetched {Count} movements", movements?.Count ?? 0);
            
            return movements ?? new List<MovementDto>();
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation("Request was cancelled");
            throw;
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError(httpEx, "HTTP error while fetching movements from Ziur API");
            throw new ApplicationException("Error al consumir la API de Ziur. Por favor, intente más tarde.", httpEx);
        }
        catch (InvalidOperationException opEx)
        {
            _logger.LogError(opEx, "Invalid operation while processing API response");
            throw new ApplicationException("Error al procesar la respuesta de la API.", opEx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching movements");
            throw new ApplicationException("Un error inesperado ocurrió. Por favor, intente más tarde.", ex);
        }
    }
}
