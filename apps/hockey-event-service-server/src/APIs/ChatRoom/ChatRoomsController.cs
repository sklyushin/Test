using Microsoft.AspNetCore.Mvc;

namespace HockeyEventService.APIs;

[ApiController()]
public class ChatRoomsController : ChatRoomsControllerBase
{
    public ChatRoomsController(IChatRoomsService service)
        : base(service) { }
}
