using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;
using MediatR;

namespace Application.Commands;

public class UpdateEmployeeCommand(UpdateEmployeeCommandDto commandDto) : IRequest<EmployeeProfileResponseDto>
{
    public string FullName { get; set; } = commandDto.FullName;
    public GenderStatus? Gender { get; set; } = commandDto.Gender;
    public string StreetAddress { get; set; } = commandDto.StreetAddress;
    public string City { get; set; } = commandDto.City;
    public string Province { get; set; } =  commandDto.Province;
    public string PostalCode { get; set; } = commandDto.PostalCode;
    public string PhoneNumber { get; set; } = commandDto.PhoneNumber;
    public string PersonalEmail { get; set; } = commandDto.PersonalEmail;
    public string PlaceOfBirth { get; set; } = commandDto.PlaceOfBirth;
    public DateOnly? DateOfBirth { get; set; } = commandDto.DateOfBirth;
    public MaritalStatus? MaritalStatus { get; set; } = commandDto.MaritalStatus;
    public string EmergencyContactName { get; set; } = commandDto.EmergencyContactName;
    public string? EmergencyContactPhone { get; set; } = commandDto.EmergencyContactPhone;
    public string? EmergencyContactRelationship { get; set; } = commandDto.EmergencyContactRelationship;

    public class Handler(IEmployeeRepository employeeRepository, IRequestRepository requestRepository, ICurrentUserService currentUserService) : IRequestHandler<UpdateEmployeeCommand, EmployeeProfileResponseDto>
    {
        public async Task<EmployeeProfileResponseDto> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var email = currentUserService.Email;
            if (string.IsNullOrEmpty(email)) throw new UnauthorizedAccessException("User not authenticated");

            var employee = await employeeRepository.GetByEmailAsync(email);

            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found");
            }
            
            var request = new EmployeeUpdateRequest
            {
                EmployeeId = employee.Id,
                NewFullName = string.IsNullOrWhiteSpace(command.FullName) ? null : command.FullName,
                NewGender = command.Gender ?? employee.GenderStatus,
                NewStreetAddress = string.IsNullOrWhiteSpace(command.StreetAddress) ? null : command.StreetAddress,
                NewCity = string.IsNullOrWhiteSpace(command.City) ? null : command.City,
                NewProvince =  string.IsNullOrWhiteSpace(command.Province) ? null : command.Province,
                NewPostalCode = string.IsNullOrWhiteSpace(command.PostalCode) ? null : command.PostalCode,
                NewPhoneNumber = string.IsNullOrWhiteSpace(command.PhoneNumber) ? null : command.PhoneNumber,
                NewPersonalEmail = string.IsNullOrWhiteSpace(command.PersonalEmail) ? null : command.PersonalEmail,
                NewPlaceOfBirth = string.IsNullOrWhiteSpace(command.PlaceOfBirth) ? null : command.PlaceOfBirth,
                NewDateOfBirth = command.DateOfBirth ?? employee.DateOfBirth,
                NewMaritalStatus = command.MaritalStatus ?? employee.MaritalStatus,
                NewEmergencyContactName = string.IsNullOrWhiteSpace(command.EmergencyContactName) ? null : command.EmergencyContactName,
                NewEmergencyContactPhone = string.IsNullOrWhiteSpace(command.EmergencyContactPhone) ? null : command.EmergencyContactPhone,
                NewEmergencyContactRelationship = command.EmergencyContactRelationship ?? string.Empty,
                Status = RequestStatus.Pending,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
            };

            await requestRepository.SubmitUpdateRequestAsync(request);
            
            return new EmployeeProfileResponseDto
            {
                EmployeeId = employee.Id,
                
                FullName = !string.IsNullOrWhiteSpace(request.NewFullName) ? request.NewFullName : employee.FullName,
                Gender = request.NewGender ?? employee.GenderStatus,
                StreetAddress = !string.IsNullOrWhiteSpace(request.NewStreetAddress) ? request.NewStreetAddress : employee.StreetAddress,
                City = !string.IsNullOrWhiteSpace(request.NewCity) ? request.NewCity : employee.City,
                Province = !string.IsNullOrWhiteSpace(request.NewProvince) ? request.NewProvince : employee.Province,
                PostalCode = !string.IsNullOrWhiteSpace(request.NewPostalCode) ? request.NewPostalCode : employee.PostalCode,
                PhoneNumber = !string.IsNullOrWhiteSpace(request.NewPhoneNumber) ? request.NewPhoneNumber : employee.PhoneNumber,
                PersonalEmail = !string.IsNullOrWhiteSpace(request.NewPersonalEmail) ? request.NewPersonalEmail : employee.PersonalEmail,
                PlaceOfBirth = !string.IsNullOrWhiteSpace(request.NewPlaceOfBirth) ? request.NewPlaceOfBirth : employee.PlaceOfBirth,
                DateOfBirth = request.NewDateOfBirth ?? employee.DateOfBirth,
                MaritalStatus = request.NewMaritalStatus ?? employee.MaritalStatus,
                EmergencyContactName = !string.IsNullOrWhiteSpace(request.NewEmergencyContactName) ? request.NewEmergencyContactName : employee.EmergencyContacts.FirstOrDefault()?.Name ?? "",
                EmergencyContactPhone = !string.IsNullOrWhiteSpace(request.NewEmergencyContactPhone) ? request.NewEmergencyContactPhone : "",
                Relationship = !string.IsNullOrWhiteSpace(request.NewEmergencyContactRelationship) ? request.NewEmergencyContactRelationship : string.Empty,

                // Strictly Unchangeable Data (Read directly from employee database)
                EmployeeEmail = employee.EmployeeEmail,
                Nik = employee.Nik,
                IsActive = employee.IsActive,
                EmployeeStatus = employee.EmploymentInformation?.EmploymentStatus ?? EmployeeStatus.Active,
                StartDate = employee.EmploymentInformation?.StartDate ?? DateOnly.MinValue,
                EmploymentType = employee.EmploymentInformation?.EmploymentType ?? EmploymentType.Fulltime,
                Department = employee.EmploymentInformation?.Department ?? "",
                Position = employee.EmploymentInformation?.Position ?? "",
                SupervisorName = employee.EmploymentInformation?.SupervisorName ?? ""
            };
        }
    }
}