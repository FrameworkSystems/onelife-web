using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance.Business;

public sealed class CustomerDto
{
    [JsonPropertyName("id")] public string Id { get; init; } = string.Empty;
    [JsonPropertyName("name")] public string Name { get; init; } = string.Empty;
    [JsonPropertyName("email")] public string Email { get; init; } = string.Empty;
    [JsonPropertyName("phone")] public string Phone { get; init; } = string.Empty;
    [JsonPropertyName("address")] public string Address { get; init; } = string.Empty;
}
