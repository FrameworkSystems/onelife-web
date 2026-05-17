using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance.BankConnect;

public sealed class TransactionDto
{
    [JsonPropertyName("id")]                   public string Id { get; set; } = string.Empty;
    [JsonPropertyName("postedAt")]             public DateTimeOffset PostedAt { get; set; }
    [JsonPropertyName("description")]          public string Description { get; set; } = string.Empty;
    [JsonPropertyName("amount")]               public decimal Amount { get; set; }
    [JsonPropertyName("bookTag")]              public string? BookTag { get; set; }
    [JsonPropertyName("reconciliationStatus")] public string? ReconciliationStatus { get; set; }
    [JsonPropertyName("merchantName")]         public string? MerchantName { get; set; }
}
