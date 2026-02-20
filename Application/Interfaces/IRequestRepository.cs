using Domain.Entities;
using Domain.Enum;

namespace Application.Interfaces;

public interface IRequestRepository
{
    Task UpdateRequestStatusAsync(EmployeeUpdateRequest request);
    
    Task<List<EmployeeUpdateRequest>> GetEmployeeUpdateRequestAsync(RequestStatus? status);
    
    Task SubmitUpdateRequestAsync(EmployeeUpdateRequest request);
    
    Task<EmployeeUpdateRequest?> GetEmployeeUpdateRequestByIdAsync(int id);
}