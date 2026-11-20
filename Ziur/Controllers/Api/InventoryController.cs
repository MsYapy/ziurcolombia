using Ziur.Shared.Models.DTOs;
using Ziur.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ziur.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly InventoryService _inventoryService;
    private readonly ILogger<InventoryController> _logger;

    public InventoryController(InventoryService inventoryService, ILogger<InventoryController> logger)
    {
        _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

  
}
