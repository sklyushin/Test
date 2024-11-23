using HockeyEventService.Infrastructure;

namespace HockeyEventService.APIs;

public class EventsService : EventsServiceBase
{
    public EventsService(HockeyEventServiceDbContext context)
        : base(context) { }
}
