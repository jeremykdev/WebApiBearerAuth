using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiBearerAuth.Models;

namespace WebApiBearerAuth.Services;

public class TokenService : ITokenService
{
    protected const string Secret = "THISISTHESECRETVALUE8675309ITNEEDSTOBELONG";

    public const string Issuer = "Test System";

    public const string Audience = "EchoApi";

    public static SecurityKey GetSecurityKey()
    {
        var bytes = Encoding.UTF8.GetBytes(Secret);
        SymmetricSecurityKey key = new(bytes);
        return key;
    }

    public string GetToken(User user)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        List<Claim> claims = [];
        claims.Add(new("sub", user.UserName));

        foreach (var r in user.Roles ?? [])
            claims.Add(new(ClaimTypes.Role, r));        

        SigningCredentials signingCredentials = new(GetSecurityKey(), SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(issuer: Issuer, audience: Audience, claims: claims, expires: DateTime.UtcNow.AddHours(6), signingCredentials: signingCredentials);

        JwtSecurityTokenHandler handler = new();
        return handler.WriteToken(token);
    }
}
