using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance.Assets;

public sealed class DepreciationScheduleDto
{
    [JsonPropertyName("assetId")]   public string AssetId { get; set; } = string.Empty;
    [JsonPropertyName("entries")]   public List<DepreciationScheduleEntryDto> Entries { get; set; } = [];
}

public sealed class DepreciationScheduleEntryDto
{
    [JsonPropertyName("period")]                  public int Period { get; set; }
    [JsonPropertyName("periodLabel")]             public string PeriodLabel { get; set; } = string.Empty;
    [JsonPropertyName("openingBalance")]          public decimal OpeningBalance { get; set; }
    [JsonPropertyName("depreciationAmount")]      public decimal DepreciationAmount { get; set; }
    [JsonPropertyName("closingBalance")]          public decimal ClosingBalance { get; set; }
    [JsonPropertyName("cumulativeDepreciation")]  public decimal CumulativeDepreciation { get; set; }
}
