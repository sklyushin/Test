using Microsoft.AspNetCore.Mvc;

namespace HockeyEventService.APIs;

[ApiController()]
public class PlacesController : PlacesControllerBase
{
    public PlacesController(IPlacesService service)
        : base(service) { }
}
