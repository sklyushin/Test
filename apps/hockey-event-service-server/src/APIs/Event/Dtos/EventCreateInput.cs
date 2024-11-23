namespace HockeyEventService.APIs.Dtos;

public class EventCreateInput
{
    public List<ChatRoom>? ChatRooms { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public string? Participants { get; set; }

    public Place? Place { get; set; }

    public DateTime? Time { get; set; }

    public DateTime UpdatedAt { get; set; }
}
