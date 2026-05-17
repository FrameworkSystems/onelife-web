using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance.Vault;

public class VaultConsentRecord
{
    [JsonPropertyName("id")]            public string Id { get; set; } = string.Empty;
    [JsonPropertyName("userId")]        public string UserId { get; set; } = string.Empty;
    [JsonPropertyName("scopes")]        public List<string> Scopes { get; set; } = [];
    [JsonPropertyName("grantedAt")]     public DateTimeOffset GrantedAt { get; set; }
    [JsonPropertyName("revokedScopes")] public List<string> RevokedScopes { get; set; } = [];
}

public class VaultAuditEvent
{
    [JsonPropertyName("entryId")]       public string EntryId { get; set; } = string.Empty;
    [JsonPropertyName("operationType")] public string OperationType { get; set; } = string.Empty;
    [JsonPropertyName("occurredAt")]    public DateTimeOffset OccurredAt { get; set; }
}

public class SetVaultConsentRequest
{
    [JsonPropertyName("userId")]  public string UserId { get; set; } = string.Empty;
    [JsonPropertyName("scope")]   public string Scope { get; set; } = string.Empty;
    [JsonPropertyName("granted")] public bool Granted { get; set; }
}
