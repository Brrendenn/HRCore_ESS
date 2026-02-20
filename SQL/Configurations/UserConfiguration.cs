using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SQL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("user_id");
        
        builder.Property(e => e.EmployeeEmail)
            .HasColumnName("employee_email")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(e => e.PersonalEmail)
            .HasColumnName("personal_email")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(e => e.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(e => e.Role)
            .HasColumnName("user_role")
            .HasConversion<string>()
            .IsRequired();
    }
    
}