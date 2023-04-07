using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EventPlanner.App.Settings;
using EventPlanner.Data.Security;
using Microsoft.IdentityModel.Tokens;

namespace EventPlanner.App.Authentication;

public class JwtManager
{
    private readonly SecuritySettings _settings;
    private readonly JwtSecurityTokenHandler _jwtTokenHandler;

    public JwtManager(SecuritySettings settings)
    {
        _settings = settings;
        _jwtTokenHandler = new JwtSecurityTokenHandler();
    }

    public Tokens GenerateTokens(string userId)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var tokenKey = Encoding.ASCII.GetBytes(_settings.JwtSecret);

        string jti = Guid.NewGuid().ToString();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(SecurityConstants.ClaimNames.UserId, userId),
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(JwtRegisteredClaimNames.Aud, _settings.JwtAudience),
                new Claim(JwtRegisteredClaimNames.Iss, _settings.JwtIssuer)
            }),
            Expires = DateTime.UtcNow.AddDays(_settings.JwtExpireDays),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = jwtTokenHandler.CreateToken(tokenDescriptor);
        string accessToken = jwtTokenHandler.WriteToken(token);
        string refreshToken = GenerateRefreshToken();
        return new Tokens
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Jti = jti,
        };
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public JwtSecurityToken GetParsedJwtToken(string accessToken)
    {
        return _jwtTokenHandler.ReadJwtToken(accessToken);
    }

    public DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
        return dateTime;
    }
}