using System.Security.Claims;
using Application.Interfaces;

namespace Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public string? Email => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
    public string UserId => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
}