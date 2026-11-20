namespace Ziur.Shared.Models.DTOs;

/// <summary>
/// Data Transfer Object for Movement data from Ziur API
/// </summary>
public class MovementDto
{
    /// <summary>
    /// Movement code/identifier
    /// </summary>
    public int Codigo { get; set; }

    /// <summary>
    /// Movement description
    /// </summary>
    public string Descripcion { get; set; } = string.Empty;

    /// <summary>
    /// Indicates if the movement is active
    /// </summary>
    public bool VActiva { get; set; }
}
