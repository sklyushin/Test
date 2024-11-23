using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HockeyEventService.Infrastructure.Models;

[Table("Events")]
public class EventDbModel
{
    public List<ChatRoomDbModel>? ChatRooms { get; set; } = new List<ChatRoomDbModel>();

    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? Participants { get; set; }

    public string? PlaceId { get; set; }

    [ForeignKey(nameof(PlaceId))]
    public PlaceDbModel? Place { get; set; } = null;

    public DateTime? Time { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
