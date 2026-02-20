using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace SQL.Authentication;

public class JwtProvider(IConfiguration configuration) : IJwtProvider
{
    public string Generate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.EmployeeEmail),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };
        var jwtKey = configuration["JWT:Key"] ??
                     throw new InvalidOperationException("JWT Key not found");
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(60),
            Issuer = configuration["JWT:Issuer"],
            Audience = configuration["JWT:Audience"],
            SigningCredentials =  credentials,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}