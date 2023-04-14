using EventPlanner.App.Models;
using EventPlanner.Data;
using EventPlanner.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

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

    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserInfo>> Get(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null)
            return NotFound();

        return new UserInfo()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName
        };
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserInfo>> Get([FromQuery] string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user is null)
            return NotFound();

        return new UserInfo()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName
        };
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserRegisterInfo>> Post(UserRegisterInfo registerInfo)
    {
        var mailAddress = new MailAddress(registerInfo.Email);
        if (!mailAddress.Host.Equals("ibs.ru", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("The Email host is not 'ibs.ru'.");
        }

        if (_context.Users.FirstOrDefault(u => u.Email == registerInfo.Email) is not null)
        {
            return BadRequest("The user with this email already exists");
        }
        
        var user = new User()
        {
            FirstName = registerInfo.FirstName,
            LastName = registerInfo.LastName,
            MiddleName = registerInfo.MiddleName,
            Email = registerInfo.Email.ToLower()
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var createdUser = new UserInfo()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName
        };
        return Created("", createdUser);
    }
}