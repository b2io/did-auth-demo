using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DidAuthDemo.IssuerApi.Services;

public interface ITokenGeneratorService
{
    string GenerateToken(string did);
}

public class TokenGeneratorService : ITokenGeneratorService
{
    private readonly IConfiguration _configuration;

    public TokenGeneratorService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string did)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, did),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
