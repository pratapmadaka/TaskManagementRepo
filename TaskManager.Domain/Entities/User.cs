using Newtonsoft.Json;

namespace TaskManager.Domain.Entities;

public class User
{
    [JsonProperty("id")]
    public String Id { get; set; } = Guid.NewGuid().ToString();

    [JsonProperty("email")]
    public String Email { get; set; } = default!;

    [JsonProperty("passwordHash")]
    public String PasswordHash { get; set; } = default!;

    [JsonProperty("fullName")]
    public String FullName { get; set; } = default!;

    [JsonProperty("role")]
    public String Role { get; set; } = "User";

    [JsonProperty("tenantId")]
    public String TenentID { get; set; } = default!;

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}