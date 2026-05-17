using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance.Assets;

public sealed class AssetDto
{
    [JsonPropertyName("id")]                   public string Id { get; set; } = string.Empty;
    [JsonPropertyName("userId")]               public string UserId { get; set; } = string.Empty;
    [JsonPropertyName("name")]                 public string Name { get; set; } = string.Empty;
    [JsonPropertyName("description")]          public string? Description { get; set; }
    [JsonPropertyName("category")]             public string Category { get; set; } = string.Empty;
    [JsonPropertyName("purchasePrice")]        public decimal PurchasePrice { get; set; }
    [JsonPropertyName("purchaseDate")]         public DateTimeOffset PurchaseDate { get; set; }
    [JsonPropertyName("depreciationMethod")]   public string DepreciationMethod { get; set; } = string.Empty;
    [JsonPropertyName("usefulLifeYears")]      public int? UsefulLifeYears { get; set; }
    [JsonPropertyName("macrsClass")]           public string? MacrsClass { get; set; }
    [JsonPropertyName("bookTag")]              public string BookTag { get; set; } = string.Empty;
    [JsonPropertyName("status")]               public string Status { get; set; } = string.Empty;
    [JsonPropertyName("currentBookValue")]     public decimal CurrentBookValue { get; set; }
    [JsonPropertyName("accumulatedDepreciation")] public decimal AccumulatedDepreciation { get; set; }
    [JsonPropertyName("lastDepreciatedAt")]    public DateTimeOffset? LastDepreciatedAt { get; set; }
    [JsonPropertyName("disposalDate")]         public DateTimeOffset? DisposalDate { get; set; }
    [JsonPropertyName("gainOrLoss")]           public decimal? GainOrLoss { get; set; }
    [JsonPropertyName("linkedInsurancePolicyId")] public string? LinkedInsurancePolicyId { get; set; }
    [JsonPropertyName("insuranceValue")]       public decimal? InsuranceValue { get; set; }
}

public sealed class CreateAssetRequest
{
    [JsonPropertyName("userId")]               public string UserId { get; set; } = string.Empty;
    [JsonPropertyName("name")]                 public string Name { get; set; } = string.Empty;
    [JsonPropertyName("description")]          public string? Description { get; set; }
    [JsonPropertyName("category")]             public string Category { get; set; } = string.Empty;
    [JsonPropertyName("purchasePrice")]        public decimal PurchasePrice { get; set; }
    [JsonPropertyName("purchaseDate")]         public DateTimeOffset PurchaseDate { get; set; }
    [JsonPropertyName("depreciationMethod")]   public string DepreciationMethod { get; set; } = "StraightLine";
    [JsonPropertyName("usefulLifeYears")]      public int? UsefulLifeYears { get; set; }
    [JsonPropertyName("macrsClass")]           public string? MacrsClass { get; set; }
    [JsonPropertyName("bookTag")]              public string BookTag { get; set; } = "Personal";
    [JsonPropertyName("linkedInsurancePolicyId")] public string? LinkedInsurancePolicyId { get; set; }
    [JsonPropertyName("insuranceValue")]       public decimal? InsuranceValue { get; set; }
}

public sealed class DisposeAssetRequest
{
    [JsonPropertyName("userId")]               public string UserId { get; set; } = string.Empty;
    [JsonPropertyName("disposalType")]         public string DisposalType { get; set; } = "Sale";
    [JsonPropertyName("salePrice")]            public decimal SalePrice { get; set; }
    [JsonPropertyName("disposalDate")]         public DateTimeOffset DisposalDate { get; set; }
}
