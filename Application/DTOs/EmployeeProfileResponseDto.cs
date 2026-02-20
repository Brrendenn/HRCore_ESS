using Domain.Enum;

namespace Application.DTOs;

public class EmployeeProfileResponseDto
{
    //Personal Information & Address
    public string FullName { get; set; } = string.Empty;
    public GenderStatus Gender { get; set; }
    public string PersonalEmail { get; set; } = string.Empty;
    public string EmployeeEmail { get; set; } =  string.Empty;
    public string StreetAddress { get; set; } =  string.Empty;
    public string City { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } =   string.Empty;
    public string Nik { get; set; } =  string.Empty;
    public string PlaceOfBirth { get; set; } =   string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public bool IsActive { get; set; }
    
    //Employment Information
    public int EmployeeId { get; set; }
    public EmployeeStatus EmployeeStatus { get; set; }
    public DateOnly StartDate { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public string Department { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string SupervisorName { get; set; } = string.Empty;
    
    //Emergency Contact
    public string EmergencyContactName { get; set; } = string.Empty;
    public string EmergencyContactPhone { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
    
}