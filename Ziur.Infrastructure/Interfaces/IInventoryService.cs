using Ziur.Shared.Models.DTOs;

namespace Ziur.Infrastructure.Interfaces;

public interface IInventoryService
{
    Task<List<MovementDto>> GetMovementsAsync(CancellationToken cancellationToken = default);
}
