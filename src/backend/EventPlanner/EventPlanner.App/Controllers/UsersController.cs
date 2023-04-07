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

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<UserInfo>>> GetAll()
    {
        var users = await _context.Users.Select(u => new UserInfo
        {
            Id = u.Id,
            Email = u.Email,
            FirstName = u.FirstName,
            LastName = u.LastName,
            MiddleName = u.MiddleName,

        }).ToListAsync();

        return users;
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
        
        var user = new User()
        {
            FirstName = registerInfo.FirstName,
            LastName = registerInfo.LastName,
            MiddleName = registerInfo.MiddleName,
            Email = registerInfo.Email
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