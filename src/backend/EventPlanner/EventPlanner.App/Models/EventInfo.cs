using EventPlanner.Data.Entities;

namespace EventPlanner.App.Models;

public record EventInfo(
    int Id,
    EventTypeInfo? Type,
    string Name,
    string OrganizerName,
    string LocationName,
    decimal? Cost,
    string Description,
    DateTimeOffset BeginTime,
    DateTimeOffset EndTime,
    DateTimeOffset RegistrationEndTime,
    int? Slots,
    int ExtraSlotsPerUser,
    string Resources);