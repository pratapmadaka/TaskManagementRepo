namespace TaskManager.Domain.Entities;

public class User
{
    public String Id { get; set; } = Guid.NewGuid().ToString();
    public String Email { get; set; } = default!;
    public String PasswordHAsh { get; set; } = default!;

    public String FullName { get; set; } = default!;

    public String Role { get; set; } = default!;

    public String TenentID { get; set; } = default!;
}