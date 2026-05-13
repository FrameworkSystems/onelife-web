using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance;

public sealed class AccountDto
{
    [JsonPropertyName("Id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("UserId")]
    public string UserId { get; init; } = string.Empty;

    [JsonPropertyName("AccountType")]
    public string AccountType { get; init; } = string.Empty;

    [JsonPropertyName("AccountNumber")]
    public string AccountNumber { get; init; } = string.Empty;

    [JsonPropertyName("DisplayName")]
    public string DisplayName { get; init; } = string.Empty;

    [JsonPropertyName("Balance")]
    public decimal Balance { get; init; }

    [JsonPropertyName("IsPosting")]
    public bool IsPosting { get; init; }

    [JsonPropertyName("CogsIntent")]
    public bool CogsIntent { get; init; }

    [JsonPropertyName("DepreciationMethod")]
    public string? DepreciationMethod { get; init; }

    [JsonPropertyName("UsefulLifeYears")]
    public int? UsefulLifeYears { get; init; }
}
