using Domain.Enum;

namespace Application.DTOs;

public class CreateEmployeeRequestDto
{
    public string EmployeeEmail { get; set; } = string.Empty;
    public string DefaultPassword { get; set; } = "CompanyPass123";
    public string FullName { get; set; } = string.Empty;
    public GenderStatus Gender { get; set; }
    public string PersonalEmail { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Nik { get; set; } = string.Empty;
    public string PlaceOfBirth { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string StreetAddress { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public CreateEmploymentInfoDto? EmploymentInformation { get; set; }
    public List<CreateEmergencyContactDto> EmergencyContacts { get; set; } = new();
}

public class CreateEmploymentInfoDto
{
    public EmployeeStatus EmploymentStatus { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public EmploymentType EmploymentType { get; set; }
    public string Department { get; set; } = string.Empty;
    public string Position { get; set; } =  string.Empty;
    public string SupervisorName { get; set; } = string.Empty;
}

public class CreateEmergencyContactDto
{
    public string Name { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}