using Domain.Entities;
using Domain.Enum;
using Application.Interfaces;

namespace SQL.Seeder;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context, IPasswordHasher passwordHasher)
    {
        if (context.Users.Any())
        {
            return; 
        }
        
        var hrUser = new User
        {
            EmployeeEmail = "Brandon@company.com",
            PersonalEmail = "brandon.personal@gmail.com",
            Role = UserRole.Supervisor,
            PasswordHash = passwordHasher.Hash("AdminPass123!") 
        };

        var empUser = new User
        {
            EmployeeEmail = "Owen@company.com",
            PersonalEmail = "owen.personal@gmail.com",
            Role = UserRole.Intern,
            PasswordHash = passwordHasher.Hash("WorkerPass123!")
        };

        context.Users.AddRange(hrUser, empUser);
        
        var hrDetails = new Employee 
        { 
            FullName = "Brandon", 
            GenderStatus = GenderStatus.Male,
            PersonalEmail = hrUser.PersonalEmail,
            EmployeeEmail = hrUser.EmployeeEmail, 
            Nik = "3171234567890001",
            PlaceOfBirth = "Jakarta",
            DateOfBirth = new DateTime(1998, 5, 15),
            MaritalStatus = MaritalStatus.Single,
            StreetAddress = "Jl. Sudirman No. 1", 
            City = "Jakarta",
            Province = "DKI Jakarta",
            PostalCode = "10220",
            PhoneNumber = "08123456789",
            IsActive = true,
            
            EmploymentInformation = new EmploymentInformation
            {
                EmploymentStatus = EmployeeStatus.Active,
                EmploymentType =  EmploymentType.Fulltime,
                StartDate = new DateTime(2024, 1, 1),
                Department = "Human Resources",
                Position = "HR Manager",
                SupervisorName = "Alexander Tyas Aji Wardhana"
            },
            
            EmergencyContacts = new List<EmergencyContact>
            {
                new EmergencyContact { Name = "Sarah", Relationship = "Sister", PhoneNumber = "08199998888" }
            }
        };
        
        var empDetails = new Employee 
        { 
            FullName = "Owen", 
            GenderStatus =  GenderStatus.Male,
            PersonalEmail = empUser.PersonalEmail,
            EmployeeEmail = empUser.EmployeeEmail, 
            Nik = "3271234567890002",
            PlaceOfBirth = "Bandung",
            DateOfBirth = new DateTime(2002, 8, 20),
            MaritalStatus = MaritalStatus.Divorced,
            StreetAddress = "Jl. Dago No. 42", 
            City = "Bandung",
            Province = "Jawa Barat",
            PostalCode = "40135",
            PhoneNumber = "08987654321",
            IsActive = true,
            
            EmploymentInformation = new EmploymentInformation
            {
                EmploymentStatus = EmployeeStatus.Active,
                EmploymentType =  EmploymentType.Intern,
                StartDate = new DateTime(2026, 1, 10),
                Department = "IT",
                Position = "Application Developer",
                SupervisorName = "Alexander Tyas Aji Wardhana"
            },
            
            EmergencyContacts = new List<EmergencyContact>
            {
                new EmergencyContact { Name = "Martha", Relationship = "Mother", PhoneNumber = "08555555555" }
            }
        };

        context.Employees.AddRange(hrDetails, empDetails);

        // 3. Save everything to SQL!
        await context.SaveChangesAsync();
    }
}