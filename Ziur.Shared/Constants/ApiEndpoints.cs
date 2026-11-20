namespace Ziur.Shared.Constants;
/// <summary>
/// API endpoint constants
/// </summary>
public static class ApiEndpoints
{
    public const string BaseApi = "/api";

    public static class Inventory
    {
        public const string GetMovements = $"{BaseApi}/inventory/movements";
    }
}
