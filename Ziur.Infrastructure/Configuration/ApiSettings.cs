namespace Ziur.Infrastructure.Configuration;

/// <summary>
/// Configuration settings for external API (Ziur)
/// </summary>
public class ApiSettings
{
    public const string SectionName = "ZiurApiSettings";
    
    /// <summary>
    /// Base URL for the Ziur API
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Bearer token for authentication
    /// </summary>
    public string Token { get; set; } = string.Empty;
}
