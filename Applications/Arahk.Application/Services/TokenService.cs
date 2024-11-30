using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Arahk.Domain.Identity.Entities;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Arahk.Application.Services;

public static class TokenService
{
    private const string SecretKey = "D2B4B52D2AAD4AAA96AA96AA96AA96AA"; // Replace with your actual secret key
    private const string Issuer = "arahk"; // Replace with your app name or URL
    private const string Audience = "arahk-client"; // Replace with your audience

    public static string GenerateUserAccessToken(UserEntity user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username.Value),
            new Claim(JwtRegisteredClaimNames.Email, user.Email.Value),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique token ID
            new Claim("id", user.Id.ToString()),
            // new Claim("role", user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1), // Token validity
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}