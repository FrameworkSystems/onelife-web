using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance.Insurance;

public sealed class InsuranceRecordDto
{
    [JsonPropertyName("id")]                public string Id { get; set; } = string.Empty;
    [JsonPropertyName("userId")]            public string UserId { get; set; } = string.Empty;
    [JsonPropertyName("eventType")]         public string EventType { get; set; } = string.Empty;
    [JsonPropertyName("insuranceType")]     public string InsuranceType { get; set; } = string.Empty;
    [JsonPropertyName("amount")]            public decimal Amount { get; set; }
    [JsonPropertyName("isBusinessPremium")] public bool IsBusinessPremium { get; set; }
    [JsonPropertyName("isTotalLoss")]       public bool IsTotalLoss { get; set; }
    [JsonPropertyName("linkedAssetId")]     public string? LinkedAssetId { get; set; }
    [JsonPropertyName("journalEntryId")]    public string? JournalEntryId { get; set; }
    [JsonPropertyName("externalRefId")]     public string? ExternalRefId { get; set; }
    [JsonPropertyName("processedAt")]       public DateTimeOffset ProcessedAt { get; set; }
}

public sealed class HsaFsaSummaryDto
{
    [JsonPropertyName("hsaBalance")]            public decimal HsaBalance { get; set; }
    [JsonPropertyName("fsaBalance")]            public decimal FsaBalance { get; set; }
    [JsonPropertyName("hsaContributionsYtd")]   public decimal HsaContributionsYtd { get; set; }
    [JsonPropertyName("fsaContributionsYtd")]   public decimal FsaContributionsYtd { get; set; }
    [JsonPropertyName("fsaPlanYearEnd")]         public DateTimeOffset? FsaPlanYearEnd { get; set; }
    [JsonPropertyName("fsaDaysRemaining")]       public int? FsaDaysRemaining { get; set; }
    [JsonPropertyName("hsaRecords")]             public List<InsuranceRecordDto> HsaRecords { get; set; } = [];
    [JsonPropertyName("fsaRecords")]             public List<InsuranceRecordDto> FsaRecords { get; set; } = [];
}

public sealed class InsuranceEventRequest
{
    [JsonPropertyName("userId")]            public string UserId { get; set; } = string.Empty;
    [JsonPropertyName("eventType")]         public string EventType { get; set; } = string.Empty;
    [JsonPropertyName("insuranceType")]     public string? InsuranceType { get; set; }
    [JsonPropertyName("amount")]            public decimal Amount { get; set; }
    [JsonPropertyName("isBusinessPremium")] public bool IsBusinessPremium { get; set; }
    [JsonPropertyName("isTotalLoss")]       public bool IsTotalLoss { get; set; }
    [JsonPropertyName("linkedAssetId")]     public string? LinkedAssetId { get; set; }
    [JsonPropertyName("externalRefId")]     public string? ExternalRefId { get; set; }
    [JsonPropertyName("fsaPlanYearEnd")]    public DateTimeOffset? FsaPlanYearEnd { get; set; }
}

public sealed class CategorizationResultDto
{
    [JsonPropertyName("l1PlaidCategory")]        public string? L1PlaidCategory { get; set; }
    [JsonPropertyName("l2MerchantAlias")]        public string? L2MerchantAlias { get; set; }
    [JsonPropertyName("l2RuleConfidenceCount")]  public int L2RuleConfidenceCount { get; set; }
    [JsonPropertyName("l3GptSuggestion")]        public string? L3GptSuggestion { get; set; }
    [JsonPropertyName("l3GptConfidence")]        public double L3GptConfidence { get; set; }
    [JsonPropertyName("l3FromCache")]            public bool L3FromCache { get; set; }
    [JsonPropertyName("l4BookTagSuggestion")]    public string L4BookTagSuggestion { get; set; } = "Personal";
    [JsonPropertyName("l4Confidence")]           public double L4Confidence { get; set; }
    [JsonPropertyName("l4Source")]               public string L4Source { get; set; } = string.Empty;
}
