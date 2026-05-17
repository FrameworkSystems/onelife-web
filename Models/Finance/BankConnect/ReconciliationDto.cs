using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance.BankConnect;

public sealed class ReconciliationDto
{
    [JsonPropertyName("id")]                       public string Id { get; set; } = string.Empty;
    [JsonPropertyName("userId")]                   public string UserId { get; set; } = string.Empty;
    [JsonPropertyName("plaidTransactionId")]       public string PlaidTransactionId { get; set; } = string.Empty;
    [JsonPropertyName("amount")]                   public decimal Amount { get; set; }
    [JsonPropertyName("transactionDate")]          public DateTimeOffset TransactionDate { get; set; }
    [JsonPropertyName("merchantName")]             public string MerchantName { get; set; } = string.Empty;
    [JsonPropertyName("plaidCategory")]            public string? PlaidCategory { get; set; }
    [JsonPropertyName("source")]                   public string Source { get; set; } = string.Empty;
    [JsonPropertyName("status")]                   public string Status { get; set; } = string.Empty;
    [JsonPropertyName("confidence")]               public double? Confidence { get; set; }
    [JsonPropertyName("matchedToJournalEntryId")]  public string? MatchedToJournalEntryId { get; set; }
    [JsonPropertyName("confirmedBookTag")]          public string? ConfirmedBookTag { get; set; }
    [JsonPropertyName("requiresSplitConfirmation")] public bool RequiresSplitConfirmation { get; set; }
}

public sealed class ConfirmMatchRequest
{
    [JsonPropertyName("userId")]           public string UserId { get; set; } = string.Empty;
    [JsonPropertyName("confirmedBookTag")] public string? ConfirmedBookTag { get; set; }
    [JsonPropertyName("action")]           public string Action { get; set; } = "merge";
    [JsonPropertyName("personalPct")]      public decimal? PersonalPct { get; set; }
}
