using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using OneLife.Web.Models.Finance;
using OneLife.Web.Models.Finance.Assets;
using OneLife.Web.Models.Finance.BankConnect;
using OneLife.Web.Models.Finance.Business;
using OneLife.Web.Models.Finance.Insurance;
using OneLife.Web.Models.Finance.Vault;
using OneLife.Web.Models.Finance.Agents;

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

    // Bank Connect

    public async Task<string?> GetLinkTokenAsync(CancellationToken ct = default)
    {
        var result = await _http.GetFromJsonAsync<Dictionary<string, string>>(
            $"/api/finance/bank-connect/link-token?userId={Uri.EscapeDataString(UserId)}", ct);
        return result?.GetValueOrDefault("linkToken");
    }

    public async Task<PlaidConnectionDto?> ExchangeTokenAsync(ExchangeTokenRequest req, CancellationToken ct = default)
    {
        req.UserId = UserId;
        var resp = await _http.PostAsJsonAsync("/api/finance/bank-connect/exchange-token", req, ct);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<PlaidConnectionDto>(cancellationToken: ct);
    }

    public async Task<List<PlaidConnectionDto>> GetConnectionsAsync(CancellationToken ct = default)
    {
        var result = await _http.GetFromJsonAsync<List<PlaidConnectionDto>>(
            $"/api/finance/bank-connect/connections?userId={Uri.EscapeDataString(UserId)}", ct);
        return result ?? [];
    }

    public async Task DeleteConnectionAsync(string connectionId, CancellationToken ct = default)
    {
        var resp = await _http.DeleteAsync(
            $"/api/finance/bank-connect/connections/{Uri.EscapeDataString(connectionId)}?userId={Uri.EscapeDataString(UserId)}", ct);
        resp.EnsureSuccessStatusCode();
    }

    public async Task<List<ReconciliationDto>> GetPendingMatchesAsync(CancellationToken ct = default)
    {
        var result = await _http.GetFromJsonAsync<List<ReconciliationDto>>(
            $"/api/finance/reconciliation/pending?userId={Uri.EscapeDataString(UserId)}", ct);
        return result ?? [];
    }

    public async Task<ReconciliationDto?> ConfirmMatchAsync(string recordId, ConfirmMatchRequest req, CancellationToken ct = default)
    {
        req.UserId = UserId;
        var resp = await _http.PostAsJsonAsync($"/api/finance/reconciliation/{Uri.EscapeDataString(recordId)}/confirm", req, ct);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<ReconciliationDto>(cancellationToken: ct);
    }

    public async Task<List<TransactionDto>> GetTransactionsAsync(string? bookTag = null, DateTimeOffset? from = null, DateTimeOffset? to = null, CancellationToken ct = default)
    {
        var path = $"/api/finance/transactions?userId={Uri.EscapeDataString(UserId)}";
        if (bookTag is not null) path += $"&bookTag={Uri.EscapeDataString(bookTag)}";
        if (from.HasValue) path += $"&from={Uri.EscapeDataString(from.Value.ToString("o"))}";
        if (to.HasValue) path += $"&to={Uri.EscapeDataString(to.Value.ToString("o"))}";
        var result = await _http.GetFromJsonAsync<List<TransactionDto>>(path, ct);
        return result ?? [];
    }

    // Phase 6: Insurance Sync
    public async Task<List<InsuranceRecordDto>> GetPremiumsAsync(CancellationToken ct = default)
    {
        var result = await _http.GetFromJsonAsync<List<InsuranceRecordDto>>(
            $"/api/finance/insurance/premiums?userId={Uri.EscapeDataString(UserId)}", ct);
        return result ?? [];
    }

    public async Task<List<InsuranceRecordDto>> GetClaimsAsync(CancellationToken ct = default)
    {
        var result = await _http.GetFromJsonAsync<List<InsuranceRecordDto>>(
            $"/api/finance/insurance/claims?userId={Uri.EscapeDataString(UserId)}", ct);
        return result ?? [];
    }

    public async Task<HsaFsaSummaryDto?> GetHsaFsaAsync(CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<HsaFsaSummaryDto>(
            $"/api/finance/insurance/hsa-fsa?userId={Uri.EscapeDataString(UserId)}", ct);
    }

    public async Task<InsuranceRecordDto?> PostInsuranceEventAsync(InsuranceEventRequest req, CancellationToken ct = default)
    {
        req.UserId = UserId;
        var resp = await _http.PostAsJsonAsync("/api/finance/insurance/event", req, ct);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<InsuranceRecordDto>(cancellationToken: ct);
    }

    // Phase 6: AI Categorization
    public async Task<CategorizationResultDto?> GetTransactionCategorizationAsync(string recordId, CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<CategorizationResultDto>(
            $"/api/finance/transactions/{Uri.EscapeDataString(recordId)}/categorization?userId={Uri.EscapeDataString(UserId)}", ct);
    }

    // Phase 7: Vault Consent
    public async Task<VaultConsentRecord?> GetVaultConsentAsync(CancellationToken ct = default)
        => await _http.GetFromJsonAsync<VaultConsentRecord>(
            $"/api/finance/vault/consent?userId={Uri.EscapeDataString(UserId)}", ct);

    public async Task<VaultConsentRecord?> SetVaultConsentAsync(string scope, bool granted, CancellationToken ct = default)
    {
        var req = new SetVaultConsentRequest { UserId = UserId, Scope = scope, Granted = granted };
        var resp = await _http.PostAsJsonAsync("/api/finance/vault/consent", req, ct);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<VaultConsentRecord>(cancellationToken: ct);
    }

    public async Task<List<VaultAuditEvent>> GetVaultAuditLogAsync(int limit = 50, CancellationToken ct = default)
    {
        var result = await _http.GetFromJsonAsync<List<VaultAuditEvent>>(
            $"/api/finance/vault/audit-log?userId={Uri.EscapeDataString(UserId)}&limit={limit}", ct);
        return result ?? [];
    }

    // Phase 7: Finance Agents
    public async Task<List<string>> ListAgentsAsync(CancellationToken ct = default)
    {
        var result = await _http.GetFromJsonAsync<AgentListResponse>("/api/finance/agents", ct);
        return result?.Agents ?? [];
    }

    public async Task<AgentResponse?> QueryAgentAsync(string agentName, string query, CancellationToken ct = default)
    {
        var req = new AgentQueryRequest { UserId = UserId, Query = query };
        var resp = await _http.PostAsJsonAsync($"/api/finance/agents/{Uri.EscapeDataString(agentName)}/query", req, ct);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<AgentResponse>(cancellationToken: ct);
    }
}
