using Microsoft.AspNetCore.Mvc;

namespace HockeyEventService.APIs;

[ApiController()]
public class EventsController : EventsControllerBase
{
    public EventsController(IEventsService service)
        : base(service) { }
}
