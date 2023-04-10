using EventPlanner.Data.Security;

namespace EventPlanner.App.Authentication;

public class ClaimsValidationService
{
    private readonly ILogger<ClaimsValidationService> _logger;
    private readonly HttpContext _httpContext;

    public ClaimsValidationService(ILogger<ClaimsValidationService> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContext = httpContextAccessor.HttpContext
                       ?? throw new InvalidOperationException("HttpContext not initialized");
    }

    public int? GetUserId()
    {
        var claim = _httpContext.User.FindFirst(x => x.Type == SecurityConstants.ClaimNames.UserId);
        if (claim == null)
            return null;

        if (!int.TryParse(claim.Value, out int userId))
        {
            _logger.LogError("Couldn't parse JWT id");
            return null;
        }

        return userId;
    }
}