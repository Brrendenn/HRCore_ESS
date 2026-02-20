using Application.Commands;
using FluentValidation;

namespace Application.Validators;

public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeValidator()
    {
        RuleFor(x => x.FullName)
            .MaximumLength(150).WithMessage("Full name cannot exceed 150 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.FullName)); 
        
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
    }
}