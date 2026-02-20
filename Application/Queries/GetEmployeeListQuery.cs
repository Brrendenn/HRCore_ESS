using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.Queries;

public class GetEmployeeListQuery : IRequest<List<GetEmployeeListDto>>
{
    public class Handler(IEmployeeRepository employeeRepository) : IRequestHandler<GetEmployeeListQuery, List<GetEmployeeListDto>>
    {
        public async Task<List<GetEmployeeListDto>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            var employees =  await employeeRepository.GetAllAsync();

            var dtos = employees.Select(e => new GetEmployeeListDto
            {
                Id = e.Id,
                FullName = e.FullName,
                Email = e.EmployeeEmail,
                Address = e.StreetAddress,
                PhoneNumber =  e.PhoneNumber,
                IsActive =  e.IsActive
            }).ToList();

            return dtos;
        }
    }
}