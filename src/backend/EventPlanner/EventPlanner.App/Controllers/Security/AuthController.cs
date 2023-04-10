using EventPlanner.App.Authentication;
using EventPlanner.App.Models.Account;
using EventPlanner.Data;
using EventPlanner.Data.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using EventPlanner.Data.Entities;

namespace EventPlanner.App.Controllers.Security;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly EventPlannerContext _context;
    private readonly JwtManager _jwtManager;

    public AuthController(ILogger<AuthController> logger, EventPlannerContext context, JwtManager jwtManager)
    {
        _logger = logger;
        _context = context;
        _jwtManager = jwtManager;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthInfo authInfo)
    {
        string email = authInfo.Email.ToLower();
        var admin = await _context.Admins
            .Include(admin => admin.User)
            .FirstOrDefaultAsync(admin => admin.User.Email == email);

        if (admin == null)
        {
            return Unauthorized("Invalid login or email");
        }

        if (!BCrypt.Net.BCrypt.Verify(authInfo.Password, admin.Password))
        {
            return Unauthorized("Invalid login or email");
        }

        var tokens = _jwtManager.GenerateTokens(admin.Id.ToString());
        await AddRefreshTokenToDbAsync(tokens.RefreshToken, tokens.Jti, admin.Id);

        var authResponse = new AuthResponse
        {
            Id = admin.Id,
            Email = email,
            FirstName = admin.User.FirstName,
            LastName = admin.User.LastName,
            MiddleName = admin.User.LastName,
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
        };

        return Ok(authResponse);
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RefreshTokenRequest>> Refresh([FromBody] RefreshTokenRequest refreshTokenRequest)
    {
        var parsedToken = _jwtManager.GetParsedJwtToken(refreshTokenRequest.AccessToken);

        var utcExpiryDate =
            long.Parse(parsedToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
        
        var expiryDate = _jwtManager.UnixTimeStampToDateTime(utcExpiryDate);
        if (expiryDate > DateTime.UtcNow)
        {
            return BadRequest("Cannot refresh access token since it has not expired");
        }

        var storedRefreshToken =
            await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshTokenRequest.RefreshToken);
        if (storedRefreshToken == null)
        {
            return BadRequest("Refresh token doesnt exist");
        }

        if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
        {
            return BadRequest("Refresh token has expired: user needs to relogin");
        }

        if (storedRefreshToken.IsUsed)
        {
            return BadRequest("Token has been used");
        }

        if (storedRefreshToken.IsRevoked)
        {
            return BadRequest("Token has been revoked");
        }

        var jti = parsedToken.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti);
        if (jti == null || storedRefreshToken.JwtId != jti.Value)
        {
            return BadRequest("No JTI claim");
        }

        if (!int.TryParse(parsedToken.Claims.First(x => x.Type == SecurityConstants.ClaimNames.UserId).Value,
                out int userId))
        {
            return BadRequest("No JTI claim");
        }

        var tokens = _jwtManager.GenerateTokens(userId.ToString());
        storedRefreshToken.IsUsed = true;
        _context.RefreshTokens.Update(storedRefreshToken);
        await AddRefreshTokenToDbAsync(tokens.RefreshToken, tokens.Jti, userId);
        await _context.SaveChangesAsync();

        return Ok(new RefreshTokenRequest
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken
        });
    }

    private async Task<RefreshToken> AddRefreshTokenToDbAsync(string refreshToken, string jti, int userId)
    {
        var dbRefreshToken = new RefreshToken()
        {
            JwtId = jti,
            IsUsed = false,
            UserId = userId,
            AddedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(30),
            IsRevoked = false,
            Token = refreshToken
        };

        await _context.RefreshTokens.AddAsync(dbRefreshToken);
        await _context.SaveChangesAsync();

        return dbRefreshToken;
    }
}