namespace OneLife.Web.Models.Finance;

public sealed class ApiResponse<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public string? CorrelationId { get; init; }
    public string? Error { get; init; }
}
