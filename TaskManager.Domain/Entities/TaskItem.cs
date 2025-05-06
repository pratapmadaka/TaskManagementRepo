using Newtonsoft.Json;

namespace TaskManager.Domain.Entities;

public class TaskItem
{
    [JsonProperty("id")]
    public String Id { get; set; } = Guid.NewGuid().ToString();
    [JsonProperty("tenantId")]
    public String TenantId { get; set; } = default!;
    [JsonProperty("title")]
    public string Title { get; set; } = default!;
    [JsonProperty("description")]
    public string Description { get; set; } = default!;
    [JsonProperty("isCompleted")]
    public bool IsCompleted { get; set; } = false;
    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}