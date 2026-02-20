using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;
using MediatR;

namespace Application.Commands;
public class UpdateEmployeeInfoCommand(int employeeId, UpdateEmploymentInfoDto commandDto) : IRequest<string>
{
    public int EmployeeId { get; set; } = employeeId;
    public EmployeeStatus? EmploymentStatus { get; set; } = commandDto.EmploymentStatus;
    public DateTime? StartDate { get; set; } = commandDto.StartDate;
    public EmploymentType? EmploymentType { get; set; } = commandDto.EmploymentType;
    public string Department { get; set; } = commandDto.Department;
    public string Position { get; set; } = commandDto.Position;
    public string SupervisorName { get; set; } = commandDto.SupervisorName;

    public class Handler(IEmployeeRepository employeeRepository) : IRequestHandler<UpdateEmployeeInfoCommand, string>
    {
        public async Task<string> Handle(UpdateEmployeeInfoCommand command, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.GetByIdAsync(command.EmployeeId);
            
            if (employee == null) throw new KeyNotFoundException("Employee not found");

            if (employee.EmploymentInformation == null)
            {
                employee.EmploymentInformation = new EmploymentInformation()
                {
                    EmployeeId = employee.Id,
                };
            }
            
            employee.EmploymentInformation.EmploymentStatus = command.EmploymentStatus ?? employee.EmploymentInformation.EmploymentStatus;
            employee.EmploymentInformation.StartDate = command.StartDate ?? employee.EmploymentInformation.StartDate;
            employee.EmploymentInformation.EmploymentType = command.EmploymentType ?? employee.EmploymentInformation.EmploymentType;
            employee.EmploymentInformation.Department = !string.IsNullOrWhiteSpace(command.Department) ? command.Department : employee.EmploymentInformation.Department;
            employee.EmploymentInformation.Position = !string.IsNullOrWhiteSpace(command.Position) ? command.Position : employee.EmploymentInformation.Position;
            employee.EmploymentInformation.SupervisorName = !string.IsNullOrWhiteSpace(command.SupervisorName) ? command.SupervisorName : employee.EmploymentInformation.SupervisorName;
            
            await employeeRepository.UpdateEmployeeAsync(employee);
            
            return "Employee Employment Information Updated Successfully";
        }
    }
}