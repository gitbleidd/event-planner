using AutoMapper;
using EventPlanner.App.Models;
using EventPlanner.App.Services.Interfaces;
using EventPlanner.Data;
using EventPlanner.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.App.Controllers;


[ApiController]
[Route("api/event")]
public class EventsController : ControllerBase
{
    private readonly ILogger<EventsController> _logger;
    private readonly EventPlannerContext _context;
    private readonly IParticipantSelectionService _selectionService;
    private readonly IMapper _mapper;

    public EventsController(
        ILogger<EventsController> logger, 
        EventPlannerContext context,
        IParticipantSelectionService selectionService,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _selectionService = selectionService;
        _mapper = mapper;
    }
    
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EventInfo>>> GetAll()
    {
        var events = await _context.Events
            .Include(e => e.Type)
            .OrderBy(e => e.BeginTime)
            .ToListAsync();

        return _mapper.Map<List<EventInfo>>(events);
    }

    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventInfo>> Get(int id)
    {
        var eventInfo = await _context.Events
            .Include(e => e.Type)
            .FirstOrDefaultAsync(e => e.Id == id);
        
        if (eventInfo is null)
            return NotFound("Event not found");

        return _mapper.Map<EventInfo>(eventInfo);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EventInfo>> Create(EventSaveInfo saveInfo)
    {
        var eventInfo = await _context.EventTypes
            .FirstOrDefaultAsync(e => e.Id == saveInfo.TypeId);
        
        if (eventInfo is null)
            return BadRequest("Event type not found");

        var newEvent = _mapper.Map<Event>(saveInfo);
        newEvent.Type = eventInfo;

        await _context.Events.AddAsync(newEvent);
        await _context.SaveChangesAsync();

        return Created("", _mapper.Map<EventInfo>(newEvent));
    }
    
    [HttpPut("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<EventInfo>> Change(
        int id, 
        [FromBody]EventSaveInfo eventSaveInfo)
    {
        var eventInfo = await _context.Events
            .FirstOrDefaultAsync(e => e.Id == id);
        var eventType = await _context.EventTypes
            .FirstOrDefaultAsync(e => e.Id == eventSaveInfo.TypeId);
        
        if (eventInfo is null)  
            return NotFound("Event not found");
        
        if (eventType is null)
            return BadRequest("Event type not found");
        
        _mapper.Map(eventSaveInfo, eventInfo);
        eventInfo.Type = eventType;

        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(int id)
    {
        var eventInfo = await _context.Events
            .FirstOrDefaultAsync(e => e.Id == id);
        
        if (eventInfo is null)
            return NotFound("Event not found");

        _context.Events.Remove(eventInfo);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Register(EventRegistrationInfo registrationInfo)
    {
        var eventInfo = await _context.Events
            .Include(e => e.Users)
            .FirstOrDefaultAsync(e => e.Id == registrationInfo.EventId);
        var userInfo = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == registrationInfo.UserEmail);

        if (eventInfo is null)
            return NotFound("Event not found");

        if (userInfo is null)
            return NotFound("User not found");

        if (registrationInfo.TakenExtraUsersCount > eventInfo.ExtraSlotsPerUser)
            return BadRequest("Value of extra slots for the user is exceeded");

        if (eventInfo.Users.Any(u => u == userInfo))
            return BadRequest("User already registered");

        var eventRegisteredUsers = new EventUser
        {
            TakenExtraUsersCount = registrationInfo.TakenExtraUsersCount,
            User = userInfo,
            Event = eventInfo,
            Comment = registrationInfo.Comment
        };

        eventInfo.EventUsers.Add(eventRegisteredUsers);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpGet("id/registered-users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<RegisteredUserInfo>>> GetRegisteredUsers(int id)
    {
        var eventInfo = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        if (eventInfo is null)
            return NotFound("Event not found");


        return await _context.EventUsers.Where(e => e.EventId == id)
            .Include(e => User)
            .Select(e => new RegisteredUserInfo(
                e.User.Id,
                e.User.FirstName,
                e.User.LastName,
                e.User.MiddleName,
                e.User.Email,
                e.TakenExtraUsersCount,
                e.Comment
            )).ToListAsync();
    }
    

    [HttpGet("id/participant")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<RegisteredUserInfo>>> GetParticipant(int id)
    {
        var eventInfo = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        
        if (eventInfo is null)
            return NotFound("Event not found");

        
        return await _context.EventUsers
            .Where(e => e.Event.Id == id && e.IsParticipating)
            .Include(e => e.User)
            .Select(e => new RegisteredUserInfo(
                e.User.Id,
                e.User.FirstName,
                e.User.LastName,
                e.User.MiddleName,
                e.User.Email,
                e.TakenExtraUsersCount,
                e.Comment
            )).ToListAsync();
    }


    [HttpPost("id/make-participants")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> MakeParticipants(int id)
    {
        throw new NotImplementedException();
        
        // TODO
        // var eventInfo = await _context.Events
        //     .Include(e => e.Users)
        //     .ThenInclude(e => e.ParticipantEvents)
        //     .Include(e => e.Participants)
        //     .Include(e => e.EventUsers)
        //     .FirstOrDefaultAsync(e => e.Id == id);
        //
        // if (eventInfo is null)
        //     return NotFound("Event not found");
        //
        // if (eventInfo.Participants.Any())
        //     return NoContent();
        //
        // var participants = _selectionService
        //     .GetParticipants(eventInfo, eventInfo.Slots);
        //
        // eventInfo.Participants.AddRange(participants);
        // await _context.SaveChangesAsync();
        //
        // return NoContent();
    }
}