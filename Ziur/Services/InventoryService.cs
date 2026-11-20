using Ziur.Shared.Models.DTOs;
using System.Net.Http.Json;

namespace Ziur.Infrastructure.Services;

public class InventoryService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<InventoryService> _logger;
    private static readonly string[] AllowedContentTypes = { "application/json" };

    public InventoryService(HttpClient httpClient, ILogger<InventoryService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


}