using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance;

public sealed class EnvelopeDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("userId")]
    public string UserId { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("bookTag")]
    public string BookTag { get; init; } = string.Empty;

    [JsonPropertyName("linkedAccountIds")]
    public List<string> LinkedAccountIds { get; init; } = [];

    [JsonPropertyName("budgetAmount")]
    public decimal BudgetAmount { get; init; }

    [JsonPropertyName("refillRule")]
    public string? RefillRule { get; init; }

    [JsonPropertyName("rolloverPolicy")]
    public string? RolloverPolicy { get; init; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; init; }
}
