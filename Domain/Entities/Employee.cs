using Domain.Enum;

namespace Domain.Entities;

public class Employee
{
    public int Id { get; set; } 
    public string FullName { get; set; } = string.Empty;
    public GenderStatus GenderStatus { get; set; }
    public string PersonalEmail { get; set; } =  string.Empty;
    public string EmployeeEmail { get; set; } =   string.Empty;
    public string Nik { get; set; } = string.Empty;
    public string PlaceOfBirth { get; set; } =   string.Empty;
    public DateOnly DateOfBirth { get; set; } =   DateOnly.FromDateTime(DateTime.Now);
    public MaritalStatus MaritalStatus { get; set; }
    public string StreetAddress { get; set; } =  string.Empty;
    public string City { get; set; } =   string.Empty;
    public string Province { get; set; } =   string.Empty;
    public string PostalCode { get; set; } =   string.Empty;
    public string PhoneNumber { get; set; } =   string.Empty;
    public bool IsActive { get; set; }
    public EmploymentInformation?  EmploymentInformation { get; set; }
    public ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();
}