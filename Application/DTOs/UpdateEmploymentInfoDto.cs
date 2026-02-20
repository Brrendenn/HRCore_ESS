using Domain.Enum;

namespace Application.DTOs;

public class UpdateEmploymentInfoDto
{
    public EmployeeStatus EmploymentStatus { get; set; }
    public DateTime? StartDate { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public string Department { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string SupervisorName { get; set; } = string.Empty;
}