using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enum;

namespace Domain.Entities;

public class EmploymentInformation
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public EmployeeStatus EmploymentStatus { get; set; }
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public string Department { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string SupervisorName { get; set; } = string.Empty;
}