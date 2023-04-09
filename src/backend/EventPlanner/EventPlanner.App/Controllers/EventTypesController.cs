using AutoMapper;
using EventPlanner.App.Models;
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
    private readonly IMapper _mapper;

    public EventTypesController(
        ILogger<EventsController> logger, 
        EventPlannerContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EventTypeInfo>>> GetAll()
    {
        var eventTypes = await _context.EventTypes.ToListAsync();

        return _mapper.Map<List<EventTypeInfo>>(eventTypes);
    }
}