using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HockeyEventService.Infrastructure.Models;

[Table("Places")]
public class PlaceDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public List<EventDbModel>? Events { get; set; } = new List<EventDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? Location { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
