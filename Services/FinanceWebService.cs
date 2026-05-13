using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using OneLife.Web.Models.Finance;

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
}
