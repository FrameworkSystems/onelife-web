using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance.Business;

public sealed class InventoryItemDto
{
    [JsonPropertyName("id")] public string Id { get; init; } = string.Empty;
    [JsonPropertyName("description")] public string Description { get; init; } = string.Empty;
    [JsonPropertyName("salePrice")] public decimal SalePrice { get; init; }
    [JsonPropertyName("cost")] public decimal Cost { get; init; }
    [JsonPropertyName("quantityOnHand")] public decimal QuantityOnHand { get; init; }
    [JsonPropertyName("reorderPoint")] public decimal ReorderPoint { get; init; }
    [JsonPropertyName("isLowStock")] public bool IsLowStock { get; init; }
}
