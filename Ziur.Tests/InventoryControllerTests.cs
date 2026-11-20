using Xunit;
using Microsoft.AspNetCore.Mvc;
using Ziur.Controllers.Api;
using Ziur.Infrastructure.Interfaces;
using Ziur.Shared.Models.DTOs;
using Microsoft.Extensions.Logging;
using Moq;

namespace Ziur.Tests;

/// <summary>
/// Unit tests for InventoryController API endpoints
/// </summary>
public class InventoryControllerTests
{
    [Fact]
    public async Task GetMovements_ReturnsOkResult_WhenDataExists()
    {
        // Arrange
        var mockService = new Mock<IInventoryService>();
        var mockLogger = Mock.Of<ILogger<InventoryController>>();
        
        var movements = new List<MovementDto>
        {
            new MovementDto { Codigo = 1, Descripcion = "Test 1", VActiva = true },
            new MovementDto { Codigo = 2, Descripcion = "Test 2", VActiva = false }
        };

        mockService.Setup(s => s.GetMovementsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(movements);

        var controller = new InventoryController(mockService.Object, mockLogger);

        // Act
        var result = await controller.GetMovements(CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedMovements = Assert.IsAssignableFrom<List<MovementDto>>(okResult.Value);
        Assert.Equal(2, returnedMovements.Count);
    }

    [Fact]
    public async Task GetMovements_ReturnsBadRequest_WhenApplicationExceptionThrown()
    {
        // Arrange
        var mockService = new Mock<IInventoryService>();
        var mockLogger = Mock.Of<ILogger<InventoryController>>();

        mockService.Setup(s => s.GetMovementsAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ApplicationException("API Error"));

        var controller = new InventoryController(mockService.Object, mockLogger);

        // Act
        var result = await controller.GetMovements(CancellationToken.None);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.NotNull(badRequestResult.Value);
    }

    [Fact]
    public async Task GetMovements_Returns499_WhenRequestCancelled()
    {
        // Arrange
        var mockService = new Mock<IInventoryService>();
        var mockLogger = Mock.Of<ILogger<InventoryController>>();
        var cts = new CancellationTokenSource();
        cts.Cancel();

        mockService.Setup(s => s.GetMovementsAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new OperationCanceledException());

        var controller = new InventoryController(mockService.Object, mockLogger);

        // Act
        var result = await controller.GetMovements(cts.Token);

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result.Result);
        Assert.Equal(499, statusCodeResult.StatusCode);
    }
}
