using HockeyEventService.Infrastructure;

namespace HockeyEventService.APIs;

public class ChatRoomsService : ChatRoomsServiceBase
{
    public ChatRoomsService(HockeyEventServiceDbContext context)
        : base(context) { }
}
