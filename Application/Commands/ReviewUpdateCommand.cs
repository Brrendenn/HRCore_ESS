using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;
using MediatR;

namespace Application.Commands;

public class ReviewUpdateCommand(ReviewRequestDto decision) : IRequest<string>
{
    private ReviewRequestDto Decision { get; } = decision;

    public class Handler(IEmployeeRepository employeeRepository, IRequestRepository requestRepository, ICurrentUserService currentUserService)
        : IRequestHandler<ReviewUpdateCommand, string>
    {
        public async Task<string> Handle(ReviewUpdateCommand command, CancellationToken cancellationToken)
        {
            var decision = command.Decision;

            var currentHrEmail = currentUserService.Email;
            if(string.IsNullOrEmpty(currentHrEmail)) throw new UnauthorizedAccessException("User not authenticated");

            var request = await requestRepository.GetEmployeeUpdateRequestByIdAsync(decision.RequestId);

            if (request == null) throw new KeyNotFoundException("Request Not Found");

            if (request.Status != RequestStatus.Pending)
                throw new Exception("This request has already been processed.");

            if (request.Employee.EmployeeEmail == currentHrEmail || request.Employee.PersonalEmail == currentHrEmail)
            {
                throw new InvalidOperationException(
                    "Security Violation: You cannot approve your own update request. Another HR member must review this.");
            }

            if (decision.IsApproved)
            {
                if (!string.IsNullOrWhiteSpace(request.NewFullName)) request.Employee.FullName = request.NewFullName;
                if (request.NewGender.HasValue) request.Employee.GenderStatus = request.NewGender.Value;
                if (!string.IsNullOrWhiteSpace(request.NewStreetAddress))
                    request.Employee.StreetAddress = request.NewStreetAddress;
                if (!string.IsNullOrWhiteSpace(request.NewCity)) request.Employee.City = request.NewCity;
                if (!string.IsNullOrWhiteSpace(request.NewProvince)) request.Employee.Province = request.NewProvince;
                if (!string.IsNullOrWhiteSpace(request.NewPostalCode))
                    request.Employee.PostalCode = request.NewPostalCode;
                if (!string.IsNullOrWhiteSpace(request.NewPhoneNumber))
                    request.Employee.PhoneNumber = request.NewPhoneNumber;
                if (!string.IsNullOrWhiteSpace(request.NewPersonalEmail))
                    request.Employee.PersonalEmail = request.NewPersonalEmail;
                if (!string.IsNullOrWhiteSpace(request.NewPlaceOfBirth))
                    request.Employee.PlaceOfBirth = request.NewPlaceOfBirth;
                if (request.NewDateOfBirth.HasValue) request.Employee.DateOfBirth = request.NewDateOfBirth.Value;
                if (request.NewMaritalStatus.HasValue) request.Employee.MaritalStatus = request.NewMaritalStatus.Value;
                
                if (!string.IsNullOrWhiteSpace(request.NewEmergencyContactName) ||
                    !string.IsNullOrWhiteSpace(request.NewEmergencyContactPhone))
                {
                    var emergencyContact = request.Employee.EmergencyContacts.FirstOrDefault();
                    
                    if (emergencyContact == null)
                    {
                        emergencyContact = new EmergencyContact { EmployeeId = request.Employee.Id };
                        request.Employee.EmergencyContacts.Add(emergencyContact);
                    }

                    if (!string.IsNullOrWhiteSpace(request.NewEmergencyContactName))
                        emergencyContact.Name = request.NewEmergencyContactName;

                    if (!string.IsNullOrWhiteSpace(request.NewEmergencyContactPhone))
                        emergencyContact.PhoneNumber = request.NewEmergencyContactPhone;

                    if (!string.IsNullOrWhiteSpace(request.NewEmergencyContactRelationship))
                        emergencyContact.Relationship = request.NewEmergencyContactRelationship;
                }

                request.Status = RequestStatus.Approved;
                request.HrReason = decision.Reason ?? "Approved by HR";

                await employeeRepository.UpdateEmployeeAsync(request.Employee);
                await requestRepository.UpdateRequestStatusAsync(request);
            }
            else
            {
                request.Status = RequestStatus.Rejected;
                request.HrReason = decision.Reason ?? "Rejected by HR";

                await requestRepository.UpdateRequestStatusAsync(request);
            }

            return decision.IsApproved ? "Request Approved. Profile Updated" : "Request Rejected";
        }
    }
}