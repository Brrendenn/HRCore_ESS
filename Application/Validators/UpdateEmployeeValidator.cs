using Application.Commands;
using FluentValidation;
using System;

namespace Application.Validators;

public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeValidator()
    {
        RuleFor(x => x.FullName)
            .MaximumLength(150).WithMessage("Full name cannot exceed 150 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.FullName));

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Invalid gender value.")
            .When(x => x.Gender.HasValue);
            
        RuleFor(x => x.PersonalEmail)
            .MaximumLength(150).WithMessage("Personal email cannot exceed 150 characters.")
            .EmailAddress().WithMessage("Invalid email format.")
            .When(x => !string.IsNullOrWhiteSpace(x.PersonalEmail));

        RuleFor(x => x.PlaceOfBirth)
            .MaximumLength(100).WithMessage("Place of birth cannot exceed 100 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.PlaceOfBirth));

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Today).WithMessage("Date of birth cannot be in the future.")
            .When(x => x.DateOfBirth.HasValue);

        RuleFor(x => x.MaritalStatus)
            .IsInEnum().WithMessage("Invalid marital status value.")
            .When(x => x.MaritalStatus.HasValue);
        
        RuleFor(x => x.StreetAddress)
            .MaximumLength(150).WithMessage("Street address cannot exceed 150 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.StreetAddress));
        
        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage("City cannot exceed 100 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.City));
        
        RuleFor(x => x.Province)
            .MaximumLength(50).WithMessage("Province cannot exceed 50 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Province));
        
        RuleFor(x => x.PostalCode)
            .MaximumLength(15).WithMessage("Postal code cannot exceed 15 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.PostalCode));
        
        RuleFor(x => x.PhoneNumber)
            .MaximumLength(25).WithMessage("Phone number cannot exceed 25 characters.")
            .Matches(@"^\+?[0-9\s\-]+$").WithMessage("Invalid phone number format.")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber)); 
        
        RuleFor(x => x.EmergencyContactName)
            .MaximumLength(150).WithMessage("Emergency contact name cannot exceed 150 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.EmergencyContactName));

        RuleFor(x => x.EmergencyContactPhone)
            .MaximumLength(25).WithMessage("Emergency contact phone cannot exceed 25 characters.")
            .Matches(@"^\+?[0-9\s\-]+$").WithMessage("Invalid emergency contact phone format.")
            .When(x => !string.IsNullOrWhiteSpace(x.EmergencyContactPhone));

        RuleFor(x => x.EmergencyContactRelationship)
            .MaximumLength(50).WithMessage("Emergency contact relationship cannot exceed 50 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.EmergencyContactRelationship));
    }
}