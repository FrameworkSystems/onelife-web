using System.Text.Json.Serialization;

namespace OneLife.Web.Models.Finance.Agents;

public class AgentQueryRequest
{
    [JsonPropertyName("userId")]  public string UserId { get; set; } = string.Empty;
    [JsonPropertyName("query")]   public string Query { get; set; } = string.Empty;
    [JsonPropertyName("context")] public Dictionary<string, string> Context { get; set; } = [];
}

public class AgentResponse
{
    [JsonPropertyName("agentName")]   public string AgentName { get; set; } = string.Empty;
    [JsonPropertyName("answer")]      public string Answer { get; set; } = string.Empty;
    [JsonPropertyName("disclaimer")]  public string? Disclaimer { get; set; }
    [JsonPropertyName("generatedAt")] public DateTimeOffset GeneratedAt { get; set; }
}

public class AgentListResponse
{
    [JsonPropertyName("agents")] public List<string> Agents { get; set; } = [];
}
