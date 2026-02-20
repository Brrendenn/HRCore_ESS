using Application.Interfaces;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SQL;

public class AppDbContext : DbContext, IApplicationDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeUpdateRequest> EmployeeUpdateRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //Configuration
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        //Define Relationships
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.EmploymentInformation)
            .WithOne(e => e.Employee)
            .HasForeignKey<EmploymentInformation>(e => e.EmployeeId);
        
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.EmergencyContacts)
            .WithOne(ec => ec.Employee)
            .HasForeignKey(ec => ec.EmployeeId);
    }
}