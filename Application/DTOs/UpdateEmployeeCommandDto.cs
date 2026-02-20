using Domain.Enum;

namespace Application.DTOs;

public class UpdateEmployeeCommandDto
{
    public string FullName { get; set; } = string.Empty;
    public GenderStatus Gender { get; set; }
    public string PersonalEmail { get; set; } = string.Empty;
    public string PlaceOfBirth { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string StreetAddress { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string EmergencyContactName { get; set; } = string.Empty;
    public string EmergencyContactPhone { get; set; } = string.Empty;
    public string EmergencyContactRelationship { get; set; } = string.Empty;
}