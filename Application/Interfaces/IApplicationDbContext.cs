using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeUpdateRequest> EmployeeUpdateRequests { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}