using Microsoft.AspNetCore.Mvc;

namespace HockeyEventService.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
