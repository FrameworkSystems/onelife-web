using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance;

public sealed class JournalEntryLineDto
{
    [JsonPropertyName("Id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("AccountId")]
    public string AccountId { get; init; } = string.Empty;

    [JsonPropertyName("DebitAmount")]
    public decimal DebitAmount { get; init; }

    [JsonPropertyName("CreditAmount")]
    public decimal CreditAmount { get; init; }

    [JsonPropertyName("BookTag")]
    public string? BookTag { get; init; }
}

public sealed class JournalEntryDto
{
    [JsonPropertyName("Id")]
    public string? Id { get; init; }

    [JsonPropertyName("UserId")]
    public string UserId { get; init; } = string.Empty;

    [JsonPropertyName("PostedAt")]
    public DateTimeOffset? PostedAt { get; init; }

    [JsonPropertyName("Description")]
    public string? Description { get; init; }

    [JsonPropertyName("Lines")]
    public List<JournalEntryLineDto> Lines { get; init; } = [];
}
