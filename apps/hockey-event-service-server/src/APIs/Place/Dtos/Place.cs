namespace HockeyEventService.APIs.Dtos;

public class Place
{
    public DateTime CreatedAt { get; set; }

    public List<string>? Events { get; set; }

    public string Id { get; set; }

    public string? Location { get; set; }

    public string? Name { get; set; }

    public DateTime UpdatedAt { get; set; }
}
