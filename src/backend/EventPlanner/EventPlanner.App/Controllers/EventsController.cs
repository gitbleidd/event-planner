using AutoMapper;
using EventPlanner.App.Models;
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
    private readonly IMapper _mapper;

    public EventsController(
        ILogger<EventsController> logger, 
        EventPlannerContext context,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }
    
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EventInfo>>> GetAll()
    {
        var events = await _context.Events.Include(e => e.Type).ToListAsync();

        return _mapper.Map<List<EventInfo>>(events);
    }

    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventInfo>> Get(int id)
    {
        var foundEvent = await _context.Events
            .Include(e => e.Type)
            .FirstOrDefaultAsync(e => e.Id == id);
        
        if (foundEvent is null)
            return NotFound();

        return _mapper.Map<EventInfo>(foundEvent);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EventInfo>> Post(EventSaveInfo saveInfo)
    {
        var eventType = await _context.EventTypes.FirstOrDefaultAsync(e => e.Id == saveInfo.TypeId);
        if (eventType is null)
            return BadRequest("Event type not found");

        var newEvent = _mapper.Map<Event>(saveInfo);
        newEvent.Type = eventType;

        await _context.Events.AddAsync(newEvent);
        await _context.SaveChangesAsync();

        return Created("", _mapper.Map<EventInfo>(newEvent));
    }
    
    [HttpPut("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventInfo>> Change(int id, [FromBody] EventSaveInfo saveInfo)
    {
        var changedEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        var eventType = await _context.EventTypes.FirstOrDefaultAsync(e => e.Id == saveInfo.TypeId);
        
        if (eventType is null)
            return BadRequest("Event type not found");
        if (changedEvent is null)
            return NotFound();

        _mapper.Map(saveInfo, changedEvent);
        changedEvent.Type = eventType;

        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var foundEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        
        if (foundEvent is null)
            return NotFound();

        _context.Events.Remove(foundEvent);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}