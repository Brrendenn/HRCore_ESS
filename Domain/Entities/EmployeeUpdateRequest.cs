using Domain.Enum;

namespace Domain.Entities;

public class EmployeeUpdateRequest
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    
    public string? NewFullName { get; set; } = string.Empty;
    public GenderStatus? NewGender { get; set; }
    public string? NewPersonalEmail { get; set; } = string.Empty;
    public string? NewPlaceOfBirth { get; set; } = string.Empty;
    public DateTime? NewDateOfBirth { get; set; }
    public MaritalStatus? NewMaritalStatus { get; set; }
    public string? NewStreetAddress { get; set; } = string.Empty;
    public string? NewCity { get; set; } = string.Empty;
    public string? NewProvince { get; set; } = string.Empty;
    public string? NewPostalCode { get; set; } = string.Empty;
    public string? NewPhoneNumber { get; set; } = string.Empty;
    
    
    public string? NewEmergencyContactName { get; set; } = string.Empty;
    public string? NewEmergencyContactPhone { get; set; } = string.Empty;
    public string? NewEmergencyContactRelationship { get; set; } = string.Empty;

    public RequestStatus Status { get; set; }
    public string HrReason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}