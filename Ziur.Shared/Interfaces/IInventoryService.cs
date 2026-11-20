namespace Ziur.Shared.Interfaces;

using Ziur.Shared.Models.DTOs;

/// <summary>
/// Interface for inventory service operations
/// </summary>
public interface IInventoryService
{
    /// <summary>
    /// Gets all movements from the inventory
    /// </summary>
    /// <returns>List of movements</returns>
    Task<List<MovementDto>> GetMovementsAsync();
}
