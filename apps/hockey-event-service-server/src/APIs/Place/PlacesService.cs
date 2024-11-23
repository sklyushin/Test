using HockeyEventService.Infrastructure;

namespace HockeyEventService.APIs;

public class PlacesService : PlacesServiceBase
{
    public PlacesService(HockeyEventServiceDbContext context)
        : base(context) { }
}
