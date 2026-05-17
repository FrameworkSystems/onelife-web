using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance.BankConnect;

public sealed class PlaidConnectionDto
{
    [JsonPropertyName("id")]               public string Id { get; set; } = string.Empty;
    [JsonPropertyName("institutionName")]  public string InstitutionName { get; set; } = string.Empty;
    [JsonPropertyName("institutionId")]    public string InstitutionId { get; set; } = string.Empty;
    [JsonPropertyName("itemId")]           public string ItemId { get; set; } = string.Empty;
    [JsonPropertyName("status")]           public string Status { get; set; } = string.Empty;
    [JsonPropertyName("lastSync")]         public DateTimeOffset? LastSync { get; set; }
}

public sealed class ExchangeTokenRequest
{
    [JsonPropertyName("publicToken")]      public string PublicToken { get; set; } = string.Empty;
    [JsonPropertyName("institutionId")]    public string InstitutionId { get; set; } = string.Empty;
    [JsonPropertyName("institutionName")]  public string InstitutionName { get; set; } = string.Empty;
    [JsonPropertyName("userId")]           public string UserId { get; set; } = string.Empty;
}
