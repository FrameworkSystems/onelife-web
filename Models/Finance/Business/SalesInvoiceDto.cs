using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance.Business;

public sealed class SalesInvoiceDto
{
    [JsonPropertyName("id")] public string Id { get; init; } = string.Empty;
    [JsonPropertyName("invoiceNumber")] public string InvoiceNumber { get; init; } = string.Empty;
    [JsonPropertyName("customerId")] public string CustomerId { get; init; } = string.Empty;
    [JsonPropertyName("customerName")] public string CustomerName { get; init; } = string.Empty;
    [JsonPropertyName("status")] public string Status { get; init; } = string.Empty;
    [JsonPropertyName("subTotal")] public decimal SubTotal { get; init; }
    [JsonPropertyName("taxRate")] public decimal TaxRate { get; init; }
    [JsonPropertyName("taxAmount")] public decimal TaxAmount { get; init; }
    [JsonPropertyName("total")] public decimal Total { get; init; }
    [JsonPropertyName("journalEntryId")] public string? JournalEntryId { get; init; }
}
