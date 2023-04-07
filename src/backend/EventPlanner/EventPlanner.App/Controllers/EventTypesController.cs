using EventPlanner.Data;
using EventPlanner.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.App.Controllers;

[ApiController]
[Route("api/event-type")]
public class EventTypesController : ControllerBase
{
    private readonly ILogger<EventsController> _logger;
    private readonly EventPlannerContext _context;
    
    public EventTypesController(ILogger<EventsController> logger, EventPlannerContext context)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EventType>>> GetAll()
    {
        var eventTypes = await _context.EventTypes.ToListAsync();

        return eventTypes;
    }
}