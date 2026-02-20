using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace SQL.Repositories;

public class EmployeeRepository(AppDbContext dbContext) : IEmployeeRepository
{

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await dbContext.Users.AnyAsync(u => u.EmployeeEmail == email);
    }

    public async Task AddEmployeeAsync(User user, Employee employee)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.Employees.AddAsync(employee);
        
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        dbContext.Employees.Update(employee);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task<List<EmployeeUpdateRequest>> GetPendingUpdateRequestsAsync()
    {
        return await dbContext.EmployeeUpdateRequests
            .Include(r => r.Employee)
            .Where(r => r.Status == RequestStatus.Pending)
            .ToListAsync();
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await dbContext.Employees
            .AsNoTracking()
            .Where(e => e.IsActive == true)
            .ToListAsync();
    }

    public async Task<Employee?> GetByEmailAsync(string email)
    {
        return await dbContext.Employees
            .Include(e => e.EmploymentInformation)
            .Include(e => e.EmergencyContacts)
            .FirstOrDefaultAsync(u => u.EmployeeEmail == email);
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await dbContext.Employees
            .Include(e => e.EmploymentInformation)
            .Include(e => e.EmergencyContacts)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}