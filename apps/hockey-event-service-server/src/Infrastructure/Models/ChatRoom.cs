using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HockeyEventService.Infrastructure.Models;

[Table("ChatRooms")]
public class ChatRoomDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? EventId { get; set; }

    [ForeignKey(nameof(EventId))]
    public EventDbModel? Event { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? Messages { get; set; }

    public string? Participants { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
