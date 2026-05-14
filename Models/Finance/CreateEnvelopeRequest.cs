using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance;

public sealed class CreateEnvelopeRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("bookTag")]
    public string BookTag { get; set; } = "Personal";

    [JsonPropertyName("linkedAccountIds")]
    public List<string> LinkedAccountIds { get; set; } = [];

    [JsonPropertyName("budgetAmount")]
    public decimal BudgetAmount { get; set; }

    [JsonPropertyName("refillRule")]
    public string? RefillRule { get; set; }

    [JsonPropertyName("rolloverPolicy")]
    public string? RolloverPolicy { get; set; }
}
