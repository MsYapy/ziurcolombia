using Ziur.Shared.Models.DTOs;
using Ziur.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ziur.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;
    private readonly ILogger<InventoryController> _logger;

    public InventoryController(IInventoryService inventoryService, ILogger<InventoryController> logger)
    {
        _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets all movements from the Ziur API
    /// </summary>
    /// <returns>List of movements</returns>
    [HttpGet("movements")]
    public async Task<ActionResult<List<MovementDto>>> GetMovements(CancellationToken cancellationToken)
    {
        try
        {
            var movements = await _inventoryService.GetMovementsAsync(cancellationToken);
            return Ok(movements);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Request cancelled by client");
            return StatusCode(499); // Client Closed Request
        }
        catch (ApplicationException ex)
        {
            _logger.LogWarning(ex, "Application error while fetching movements");
            return BadRequest(new { error = "Error al obtener los movimientos. Por favor, intente más tarde." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching movements");
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { error = "Un error inesperado ocurrió. Por favor, intente más tarde." });
        }
    }
}
