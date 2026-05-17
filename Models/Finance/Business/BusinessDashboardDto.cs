using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance.Business;

public sealed class BusinessDashboardDto
{
    [JsonPropertyName("totalRevenueMtd")] public decimal TotalRevenueMtd { get; init; }
    [JsonPropertyName("totalCogs")] public decimal TotalCogs { get; init; }
    [JsonPropertyName("grossProfit")] public decimal GrossProfit { get; init; }
    [JsonPropertyName("netIncome")] public decimal NetIncome { get; init; }
    [JsonPropertyName("revenuePaceFraction")] public decimal? RevenuePaceFraction { get; init; }
    [JsonPropertyName("revenueGoalMtd")] public decimal? RevenueGoalMtd { get; init; }
    [JsonPropertyName("runwayDays")] public int? RunwayDays { get; init; }
    [JsonPropertyName("recentSales")] public List<RecentSaleDto> RecentSales { get; init; } = [];
    [JsonPropertyName("salesTaxAlert")] public string? SalesTaxAlert { get; init; }
}

public sealed class RecentSaleDto
{
    [JsonPropertyName("invoiceNo")] public string InvoiceNo { get; init; } = string.Empty;
    [JsonPropertyName("customerName")] public string CustomerName { get; init; } = string.Empty;
    [JsonPropertyName("total")] public decimal Total { get; init; }
    [JsonPropertyName("date")] public DateTimeOffset Date { get; init; }
}
