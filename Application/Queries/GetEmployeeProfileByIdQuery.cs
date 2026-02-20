using Application.DTOs;
using Application.Interfaces;
using Domain.Enum;
using MediatR;

namespace Application.Queries;

public class GetEmployeeProfileByIdQuery(int employeeId) : IRequest<EmployeeProfileResponseDto>
{
    public int EmployeeId { get; set; } = employeeId;
    
    public class Handler(IEmployeeRepository employeeRepository) : IRequestHandler<GetEmployeeProfileByIdQuery, EmployeeProfileResponseDto>
    {
        public async Task<EmployeeProfileResponseDto> Handle(GetEmployeeProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var profile = await employeeRepository.GetByIdAsync(request.EmployeeId);

            if (profile == null) throw new KeyNotFoundException("No Profile found");

            return new EmployeeProfileResponseDto
            {
                FullName = profile.FullName,
                Gender = profile.GenderStatus,
                PersonalEmail = profile.PersonalEmail,
                EmployeeEmail = profile.EmployeeEmail,
                PhoneNumber = profile.PhoneNumber,
                Nik = profile.Nik,
                PlaceOfBirth = profile.PlaceOfBirth,
                DateOfBirth = profile.DateOfBirth,
                MaritalStatus = profile.MaritalStatus,
                StreetAddress =  profile.StreetAddress,
                City =  profile.City,
                Province = profile.Province,
                PostalCode = profile.PostalCode,
                IsActive = profile.IsActive,
                
                EmployeeId = profile.EmploymentInformation?.EmployeeId ?? 0,
                EmployeeStatus = profile.EmploymentInformation?.EmploymentStatus ?? EmployeeStatus.Inactive,
                StartDate = profile.EmploymentInformation?.StartDate ?? DateOnly.FromDateTime(DateTime.Now),
                EmploymentType = profile.EmploymentInformation?.EmploymentType ?? EmploymentType.Unknown,
                Department = profile.EmploymentInformation?.Department ?? string.Empty,
                Position = profile.EmploymentInformation?.Position ?? string.Empty,
                SupervisorName = profile.EmploymentInformation?.SupervisorName ?? string.Empty,
                
                EmergencyContactName = profile.EmergencyContacts?.FirstOrDefault()?.Name ?? string.Empty,
                EmergencyContactPhone = profile.EmergencyContacts?.FirstOrDefault()?.PhoneNumber ?? string.Empty,
                Relationship = profile.EmergencyContacts?.FirstOrDefault()?.Relationship ?? string.Empty,
            };
        }
    }
}