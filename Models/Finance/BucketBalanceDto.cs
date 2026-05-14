using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance;

public sealed class BucketBalanceDto
{
    [JsonPropertyName("envelopeId")]
    public string EnvelopeId { get; init; } = string.Empty;

    [JsonPropertyName("balance")]
    public decimal Balance { get; init; }

    [JsonPropertyName("bookTag")]
    public string BookTag { get; init; } = string.Empty;

    [JsonPropertyName("computedAt")]
    public DateTimeOffset? ComputedAt { get; init; }
}
