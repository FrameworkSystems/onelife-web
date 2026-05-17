using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance.Assets;

public sealed class NetWorthDto
{
    [JsonPropertyName("netWorth")]        public decimal NetWorth { get; set; }
    [JsonPropertyName("cash")]            public decimal Cash { get; set; }
    [JsonPropertyName("assets")]          public decimal Assets { get; set; }
    [JsonPropertyName("liabilities")]     public decimal Liabilities { get; set; }
    [JsonPropertyName("assetBreakdown")]  public List<AssetBreakdownItemDto> AssetBreakdown { get; set; } = [];
}

public sealed class AssetBreakdownItemDto
{
    [JsonPropertyName("id")]     public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")]   public string Name { get; set; } = string.Empty;
    [JsonPropertyName("value")]  public decimal Value { get; set; }
}
