namespace EventPlanner.App.Models;

public record EventSaveInfo(
    int TypeId,
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