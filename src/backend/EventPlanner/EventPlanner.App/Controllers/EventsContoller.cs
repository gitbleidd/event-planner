using System.Net.Mail;
using EventPlanner.App.Models;
using EventPlanner.Data;
using EventPlanner.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.App.Controllers;

[ApiController]
[Route("api/event")]
[Authorize]
public class EventsContoller : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly EventPlannerContext _context;

    public EventsContoller(ILogger<UsersController> logger, EventPlannerContext context)
    {
        _logger = logger;
        _context = context;
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