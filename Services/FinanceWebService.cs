using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using OneLife.Web.Models.Finance;
using OneLife.Web.Models.Finance.Assets;
using OneLife.Web.Models.Finance.Business;

namespace OneLife.Web.Services;

public sealed class FinanceWebService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    public FinanceWebService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
    }

    private string UserId => _config["DevUserId"] ?? "dev-user";

    public async Task<List<AccountDto>> GetAccountsAsync(CancellationToken ct = default)
    {
        var result = await _http.GetFromJsonAsync<List<AccountDto>>(
            $"/api/finance/accounts?userId={Uri.EscapeDataString(UserId)}", ct);
        return result ?? [];
    }

    public async Task<TrialBalanceDto?> GetTrialBalanceAsync(CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<TrialBalanceDto>(
            $"/api/finance/trial-balance?userId={Uri.EscapeDataString(UserId)}", ct);
    }

    public async Task<string?> PostJournalEntryAsync(JournalEntryDto entry, CancellationToken ct = default)
    {
        var payload = new JournalEntryDto
        {
            Id = entry.Id,
            UserId = UserId,
            PostedAt = entry.PostedAt,
            Description = entry.Description,
            Lines = entry.Lines
        };
        var json = JsonSerializer.Serialize(payload);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var resp = await _http.PostAsync("/api/finance/journal", content, ct);
        resp.EnsureSuccessStatusCode();
        using var doc = await JsonDocument.ParseAsync(await resp.Content.ReadAsStreamAsync(ct), cancellationToken: ct);
        return doc.RootElement.TryGetProperty("entryId", out var id) ? id.GetString() : null;
    }

    public async Task<List<EnvelopeDto>> GetEnvelopesAsync(CancellationToken ct = default)
    {
        var result = await _http.GetFromJsonAsync<List<EnvelopeDto>>(
            $"/api/finance/envelopes?userId={Uri.EscapeDataString(UserId)}", ct);
        return result ?? [];
    }

    public async Task<EnvelopeDto?> CreateEnvelopeAsync(CreateEnvelopeRequest req, CancellationToken ct = default)
    {
        var resp = await _http.PostAsJsonAsync("/api/finance/envelopes", req, ct);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<EnvelopeDto>(cancellationToken: ct);
    }

    public async Task<BucketBalanceDto?> GetEnvelopeBalanceAsync(string envelopeId, CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<BucketBalanceDto>(
            $"/api/finance/envelopes/{Uri.EscapeDataString(envelopeId)}/balance?userId={Uri.EscapeDataString(UserId)}", ct);
    }

    public async Task LearnBookTagAsync(string merchantName, string confirmedBookTag, CancellationToken ct = default)
    {
        var req = new { merchantName, confirmedBookTag };
        var resp = await _http.PostAsJsonAsync(
            $"/api/finance/booktag/learn?userId={Uri.EscapeDataString(UserId)}", req, ct);
        resp.EnsureSuccessStatusCode();
    }

    public async Task<string?> SuggestBookTagAsync(string merchantName, CancellationToken ct = default)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<Dictionary<string, string>>(
                $"/api/finance/booktag/suggest?userId={Uri.EscapeDataString(UserId)}&merchantName={Uri.EscapeDataString(merchantName)}", ct);
            return result?.GetValueOrDefault("suggestedBookTag");
        }
        catch { return null; }
    }

    public async Task<BusinessDashboardDto?> GetBusinessDashboardAsync(CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<BusinessDashboardDto>(
            $"/api/finance/business/dashboard?userId={Uri.EscapeDataString(UserId)}", ct);
    }

    public async Task<PLStatementDto?> GetPLStatementAsync(DateTimeOffset from, DateTimeOffset to, CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<PLStatementDto>(
            $"/api/finance/business/pl-statement?userId={Uri.EscapeDataString(UserId)}&from={Uri.EscapeDataString(from.ToString("o"))}&to={Uri.EscapeDataString(to.ToString("o"))}", ct);
    }

    public async Task<ScheduleCDto?> GetScheduleCAsync(int taxYear, CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<ScheduleCDto>(
            $"/api/finance/business/schedule-c?userId={Uri.EscapeDataString(UserId)}&taxYear={taxYear}", ct);
    }

    public async Task<List<SalesInvoiceDto>> GetInvoicesAsync(string? status = null, CancellationToken ct = default)
    {
        var path = $"/api/finance/business/invoices?userId={Uri.EscapeDataString(UserId)}";
        if (status is not null) path += $"&status={Uri.EscapeDataString(status)}";
        var result = await _http.GetFromJsonAsync<List<SalesInvoiceDto>>(path, ct);
        return result ?? [];
    }

    public async Task CreateInvoiceAsync(object req, CancellationToken ct = default)
    {
        var resp = await _http.PostAsJsonAsync($"/api/finance/business/invoices?userId={Uri.EscapeDataString(UserId)}", req, ct);
        resp.EnsureSuccessStatusCode();
    }

    public async Task<bool> RecordReceiptAsync(string invoiceId, CancellationToken ct = default)
    {
        var req = new { invoiceId, paidAt = DateTimeOffset.UtcNow };
        var resp = await _http.PostAsJsonAsync($"/api/finance/business/receipts?userId={Uri.EscapeDataString(UserId)}", req, ct);
        return resp.IsSuccessStatusCode;
    }

    public async Task<List<InventoryItemDto>> GetInventoryAsync(CancellationToken ct = default)
    {
        var result = await _http.GetFromJsonAsync<List<InventoryItemDto>>(
            $"/api/finance/business/inventory?userId={Uri.EscapeDataString(UserId)}", ct);
        return result ?? [];
    }

    public async Task CreateInventoryItemAsync(object req, CancellationToken ct = default)
    {
        var resp = await _http.PostAsJsonAsync($"/api/finance/business/inventory?userId={Uri.EscapeDataString(UserId)}", req, ct);
        resp.EnsureSuccessStatusCode();
    }

    public async Task<List<CustomerDto>> GetCustomersAsync(CancellationToken ct = default)
    {
        var result = await _http.GetFromJsonAsync<List<CustomerDto>>(
            $"/api/finance/business/customers?userId={Uri.EscapeDataString(UserId)}", ct);
        return result ?? [];
    }

    public async Task<List<AssetDto>> GetAssetsAsync(string? bookTag = null, bool includeDisposed = false, CancellationToken ct = default)
    {
        var path = $"/api/finance/assets?userId={Uri.EscapeDataString(UserId)}&includeDisposed={includeDisposed}";
        if (bookTag is not null) path += $"&bookTag={Uri.EscapeDataString(bookTag)}";
        var result = await _http.GetFromJsonAsync<List<AssetDto>>(path, ct);
        return result ?? [];
    }

    public async Task<AssetDto?> CreateAssetAsync(CreateAssetRequest req, CancellationToken ct = default)
    {
        req.UserId = UserId;
        var resp = await _http.PostAsJsonAsync("/api/finance/assets", req, ct);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<AssetDto>(cancellationToken: ct);
    }

    public async Task<AssetDto?> DisposeAssetAsync(string assetId, DisposeAssetRequest req, CancellationToken ct = default)
    {
        req.UserId = UserId;
        var resp = await _http.PostAsJsonAsync($"/api/finance/assets/{Uri.EscapeDataString(assetId)}/dispose", req, ct);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<AssetDto>(cancellationToken: ct);
    }

    public async Task<DepreciationScheduleDto?> GetDepreciationScheduleAsync(string assetId, CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<DepreciationScheduleDto>(
            $"/api/finance/assets/{Uri.EscapeDataString(assetId)}/depreciation-schedule?userId={Uri.EscapeDataString(UserId)}", ct);
    }

    public async Task<NetWorthDto?> GetNetWorthAsync(string? bookTag = null, CancellationToken ct = default)
    {
        var path = $"/api/finance/net-worth?userId={Uri.EscapeDataString(UserId)}";
        if (bookTag is not null) path += $"&bookTag={Uri.EscapeDataString(bookTag)}";
        return await _http.GetFromJsonAsync<NetWorthDto>(path, ct);
    }
}
