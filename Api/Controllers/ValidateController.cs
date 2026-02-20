using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ValidateController : ControllerBase
{
    protected IActionResult Success(object data, string message = "Success")
    {
        return Ok(new
        {
            Success = true,
            Message = message,
            Data = data
        });
    }

    protected IActionResult Error(string message, int statusCode = 400)
    {
        return StatusCode(statusCode, new
        {
            Success = false,
            Message = message
        });
    }
}