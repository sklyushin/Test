namespace HockeyEventService.APIs.Dtos;

public class ChatRoomUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Event { get; set; }

    public string? Id { get; set; }

    public string? Messages { get; set; }

    public string? Participants { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
