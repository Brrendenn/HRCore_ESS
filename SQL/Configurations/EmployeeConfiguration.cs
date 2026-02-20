using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SQL.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("emp_id");
        
        builder.Property(e => e.FullName)
            .HasColumnName("emp_name")
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(e => e.GenderStatus)
            .HasColumnName("emp_gender")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(e => e.PersonalEmail)
            .HasColumnName("emp_personal_email")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(e => e.EmployeeEmail)
            .HasColumnName("emp_work_email")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(e => e.Nik)
            .HasColumnName("emp_nik")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(e => e.PlaceOfBirth)
            .HasColumnName("emp_POB")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(e => e.DateOfBirth)
            .HasColumnName("emp_DOB")
            .HasColumnType("date")
            .IsRequired();
        
        builder.Property(e => e.MaritalStatus)
            .HasColumnName("emp_marital_status")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(e => e.StreetAddress)
            .HasColumnName("emp_st_address")
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(e => e.City)
            .HasColumnName("emp_city")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(e => e.Province)
            .HasColumnName("emp_province")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(e => e.PostalCode)
            .HasColumnName("emp_postal_code")
            .HasMaxLength(15)
            .IsRequired();
        
        builder.Property(e => e.PhoneNumber)
            .HasColumnName("emp_phone")
            .HasMaxLength(25)
            .IsRequired();
            
    }
}