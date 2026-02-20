using Application.DTOs;
using Application.Commands;
using Application.Queries;
using Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class EmployeeController(IMediator mediator) : ValidateController
{

    [HttpPut("me")]
    [Authorize]
    public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommandDto commandDto)
    {

        var command = new UpdateEmployeeCommand(commandDto);
        var result = await mediator.Send(command);
        return Success(result, "Employee Updated successfully");
    }
    
    [HttpPut("employment-info/{id}")]
    [Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> UpdateEmploymentInformation(int id, [FromBody] UpdateEmploymentInfoDto commandDto)
    {
        var command = new UpdateEmployeeInfoCommand(id, commandDto);
        var result = await mediator.Send(command);
        return Success(result, "Employee Employment Information Updated Successfully");
    }

    [HttpGet]
    [Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> GetAllEmployees()
    {
        var query = new GetEmployeeListQuery();
        var employees = await mediator.Send(query);
        return Success(employees, "Employee List Showed Successfully");
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMyProfile()
    {
        var query = new GetMyProfileQuery();
        var profile = await mediator.Send(query);
        return Success(profile, "Employee Profile Retrieved Successfully");
    }

    [HttpGet("requests")]
    [Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> GetEmployeeRequests([FromQuery] RequestStatus? status)
    {
        var query = new GetUpdateRequestQuery(status);
        var result = await mediator.Send(query);
        return Success(result, "Employee Request Retrieved Successfully");
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> GetEmployeeProfileById(int id)
    {
        var query = new GetEmployeeProfileByIdQuery(id);
        var result = await mediator.Send(query);
        return Success(result, "Employee Profile Retrieved Successfully");
    }
    
    [HttpPost("review-update")]
    [Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> ReviewUpdate([FromBody] ReviewRequestDto decision)
    {
        var command = new ReviewUpdateCommand(decision);
        var result = await mediator.Send(command);
        return Success(result, "Review Processed Successfully");
    }
    
    [HttpPost("create")]
    [Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequestDto requestDto)
    {
        var command = new CreateEmployeeCommand(requestDto);
        var result = await mediator.Send(command);
        return Success(result, "Employee Created successfully");
    }
}