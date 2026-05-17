using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance.Business;

public sealed class ScheduleCDto
{
    [JsonPropertyName("taxYear")] public int TaxYear { get; init; }
    [JsonPropertyName("disclaimer")] public string Disclaimer { get; init; } = string.Empty;
    [JsonPropertyName("lines")] public List<ScheduleCLineDto> Lines { get; init; } = [];
}

public sealed class ScheduleCLineDto
{
    [JsonPropertyName("lineNumber")] public string LineNumber { get; init; } = string.Empty;
    [JsonPropertyName("description")] public string Description { get; init; } = string.Empty;
    [JsonPropertyName("amount")] public decimal Amount { get; init; }
}
