using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance;

public sealed class TrialBalanceDto
{
    [JsonPropertyName("TotalDebits")]
    public decimal TotalDebits { get; init; }

    [JsonPropertyName("TotalCredits")]
    public decimal TotalCredits { get; init; }

    [JsonPropertyName("IsBalanced")]
    public bool IsBalanced { get; init; }

    [JsonPropertyName("AsOf")]
    public DateTimeOffset? AsOf { get; init; }
}
