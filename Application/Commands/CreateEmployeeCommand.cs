using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;
using MediatR;

namespace Application.Commands;

public class CreateEmployeeCommand(CreateEmployeeRequestDto requestDto) : IRequest<string>
{
    public CreateEmployeeRequestDto RequestDto { get; } = requestDto;

    public class Handler(IEmployeeRepository employeeRepository, IPasswordHasher passwordHasher) : IRequestHandler<CreateEmployeeCommand, string>
    {
        public async Task<string> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var dto = command.RequestDto;
            
            if (!await employeeRepository.IsEmailUniqueAsync(dto.EmployeeEmail)) 
            {
                throw new Exception($"The email '{dto.EmployeeEmail}' is already in use");
            }
            
            var newUser = new User
            {
                EmployeeEmail = dto.EmployeeEmail, 
                PersonalEmail = dto.PersonalEmail,
                Role = UserRole.Intern, 
                PasswordHash = passwordHasher.Hash(dto.DefaultPassword),
            };
            
            var newEmployee = new Employee
            {
                FullName = dto.FullName,
                GenderStatus = dto.Gender,
                EmployeeEmail = dto.EmployeeEmail,       
                PersonalEmail = dto.PersonalEmail,
                Nik = dto.Nik,
                PlaceOfBirth = dto.PlaceOfBirth,
                DateOfBirth = dto.DateOfBirth,
                MaritalStatus = dto.MaritalStatus,
                StreetAddress = dto.StreetAddress,   
                City = dto.City,
                Province = dto.Province,
                PostalCode = dto.PostalCode,
                PhoneNumber = dto.PhoneNumber,
                IsActive = true,
                
                EmergencyContacts = new List<EmergencyContact>() 
            };
            
            if (dto.EmploymentInformation != null)
            {
                newEmployee.EmploymentInformation = new EmploymentInformation
                {
                    EmploymentStatus = dto.EmploymentInformation.EmploymentStatus,
                    StartDate = dto.EmploymentInformation.StartDate,
                    EmploymentType = dto.EmploymentInformation.EmploymentType,
                    Department = dto.EmploymentInformation.Department,
                    Position = dto.EmploymentInformation.Position
                };
            }
            
            if (dto.EmergencyContacts.Any())
            {
                foreach (var contactDto in dto.EmergencyContacts)
                {
                    newEmployee.EmergencyContacts.Add(new EmergencyContact
                    {
                        Name = contactDto.Name,
                        Relationship = contactDto.Relationship,
                        PhoneNumber = contactDto.PhoneNumber
                    });
                }
            }
            
            await employeeRepository.AddEmployeeAsync(newUser, newEmployee);
            return $"Success, Employee '{newEmployee.FullName}' has been onboarded";  
        }
    }
}