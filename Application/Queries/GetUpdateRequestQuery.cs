using Application.DTOs;
using Application.Interfaces;
using Domain.Enum;
using MediatR;

namespace Application.Queries;

public class GetUpdateRequestQuery(RequestStatus? status) : IRequest<List<EmployeeUpdateQueryDto>>
{
    public RequestStatus? Status { get; set; } = status;
    
    public class Handler(IRequestRepository requestRepository) : IRequestHandler<GetUpdateRequestQuery, List<EmployeeUpdateQueryDto>>
    {
        public async Task<List<EmployeeUpdateQueryDto>> Handle(GetUpdateRequestQuery request,
            CancellationToken cancellationToken)
        {
            var domainRequests = await requestRepository.GetEmployeeUpdateRequestAsync(request.Status);

        
            var dtos = domainRequests.Select(r => new EmployeeUpdateQueryDto
            {
                RequestId = r.Id,
                EmployeeId = r.EmployeeId,
                RequesterName = r.Employee.FullName,
                NewFullName = r.NewFullName ?? string.Empty,
                NewGender = r.NewGender ?? GenderStatus.Female,
                NewPersonalEmail = r.NewPersonalEmail ?? string.Empty,
                NewPlaceOfBirth = r.NewPlaceOfBirth ?? string.Empty,
                NewDateOfBirth = r.NewDateOfBirth ?? DateOnly.MinValue,
                NewMaritalStatus = r.NewMaritalStatus ?? MaritalStatus.Single,
                NewStreetAddress = r.NewStreetAddress ?? string.Empty,
                NewCity = r.NewCity ?? string.Empty,
                NewProvince = r.NewProvince ?? string.Empty,
                NewPostalCode = r.NewPostalCode ?? string.Empty,
                NewPhoneNumber = r.NewPhoneNumber ?? string.Empty,
                Status = r.Status,
                HrReason = r.HrReason,
                CreatedAt = r.CreatedAt,
                NewEmergencyContactName = r.NewEmergencyContactName ?? string.Empty,
                NewEmergencyContactPhone = r.NewEmergencyContactPhone ?? string.Empty,
                NewEmergencyContactRelationship = r.NewEmergencyContactRelationship ?? string.Empty,
            }).ToList();
        
            return dtos;
        }
    }
}