using HockeyEventService.Infrastructure;

namespace HockeyEventService.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(HockeyEventServiceDbContext context)
        : base(context) { }
}
