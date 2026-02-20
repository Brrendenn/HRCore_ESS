using Domain.Entities;
using Domain.Enum;

namespace Application.Interfaces;

public interface IEmployeeRepository
{
    Task<bool> IsEmailUniqueAsync(string email);
    Task AddEmployeeAsync(User user, Employee employee);
    Task<List<Employee>> GetAllAsync();
    Task<Employee?> GetByEmailAsync(string email);
    Task<Employee?> GetByIdAsync(int id);
    Task UpdateEmployeeAsync(Employee employee);
}