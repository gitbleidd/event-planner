using EventPlanner.App.Models;
using EventPlanner.Data;
using EventPlanner.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.App.Controllers;

[ApiController]
[Route("api/user")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly EventPlannerContext _context;

    public UsersController(ILogger<UsersController> logger, EventPlannerContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<User>> Get()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<User>>> GetAll()
    {
        var users = await _context.Users.Select(u => new User
        {
            Id = u.Id,
            Email = u.Email,
            FirstName = u.FirstName,
            LastName = u.LastName,
            MiddleName = u.MiddleName,

        }).ToListAsync();

        return users;
    }
    
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<User>> Register(User user)
    {
        var userInfo = new UserInfo()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName,
            Email = user.Email
        };

        _context.Users.Add(userInfo);
        await _context.SaveChangesAsync();
        
        return Created("", userInfo);
    }
}