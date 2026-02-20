using Domain.Enum;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string EmployeeEmail { get; set; } = string.Empty;
    public string PersonalEmail { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}