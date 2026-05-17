using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance.Business;

public sealed class PLStatementDto
{
    [JsonPropertyName("period")] public string Period { get; init; } = string.Empty;
    [JsonPropertyName("revenueLines")] public List<PLLineDto> RevenueLines { get; init; } = [];
    [JsonPropertyName("cogsLines")] public List<PLLineDto> CogsLines { get; init; } = [];
    [JsonPropertyName("grossProfit")] public decimal GrossProfit { get; init; }
    [JsonPropertyName("expenseLines")] public List<PLLineDto> ExpenseLines { get; init; } = [];
    [JsonPropertyName("totalExpenses")] public decimal TotalExpenses { get; init; }
    [JsonPropertyName("netIncome")] public decimal NetIncome { get; init; }
}

public sealed class PLLineDto
{
    [JsonPropertyName("description")] public string Description { get; init; } = string.Empty;
    [JsonPropertyName("amount")] public decimal Amount { get; init; }
}
