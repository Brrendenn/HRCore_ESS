using Domain.Enum;

namespace Application.DTOs;

public class EmployeeUpdateQueryDto
{
    public int RequestId { get; set; }
    public int EmployeeId { get; set; }
    public string RequesterName { get; set; } = string.Empty;
    public string NewFullName { get; set; } =  string.Empty;
    public GenderStatus NewGender { get; set; }
    public string NewPersonalEmail { get; set; } = string.Empty;
    public string NewPlaceOfBirth { get; set; } = string.Empty;
    public DateOnly NewDateOfBirth { get; set; }
    public MaritalStatus NewMaritalStatus { get; set; }
    public string NewStreetAddress { get; set; } = string.Empty;
    public string NewCity { get; set; } = string.Empty;
    public string NewProvince { get; set; } = string.Empty;
    public string NewPostalCode { get; set; } = string.Empty;
    public string NewPhoneNumber { get; set; } = string.Empty;
    public RequestStatus Status { get; set; }
    public string? HrReason { get; set; }
    public string NewEmergencyContactName { get; set; } = string.Empty;
    public string NewEmergencyContactPhone { get; set; } = string.Empty;
    public string NewEmergencyContactRelationship { get; set; } = string.Empty;
    public DateOnly CreatedAt { get; set; }
}