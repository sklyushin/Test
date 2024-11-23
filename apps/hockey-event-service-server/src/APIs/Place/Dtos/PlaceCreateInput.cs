namespace HockeyEventService.APIs.Dtos;

public class PlaceCreateInput
{
    public DateTime CreatedAt { get; set; }

    public List<Event>? Events { get; set; }

    public string? Id { get; set; }

    public string? Location { get; set; }

    public string? Name { get; set; }

    public DateTime UpdatedAt { get; set; }
}
