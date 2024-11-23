using HockeyEventService.APIs.Dtos;
using HockeyEventService.Infrastructure.Models;

namespace HockeyEventService.APIs.Extensions;

public static class EventsExtensions
{
    public static Event ToDto(this EventDbModel model)
    {
        return new Event
        {
            ChatRooms = model.ChatRooms?.Select(x => x.Id).ToList(),
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            Id = model.Id,
            Participants = model.Participants,
            Place = model.PlaceId,
            Time = model.Time,
            UpdatedAt = model.UpdatedAt,

        };
    }

    public static EventDbModel ToModel(this EventUpdateInput updateDto, EventWhereUniqueInput uniqueId)
    {
        var event = new EventDbModel { 
               Id = uniqueId.Id,
Description = updateDto.Description,
Participants = updateDto.Participants,
Time = updateDto.Time
};

     if(updateDto.CreatedAt != null) {
     event.CreatedAt = updateDto.CreatedAt.Value;
}
if(updateDto.Place != null) {
     event.PlaceId = updateDto.Place;
}
if(updateDto.UpdatedAt != null) {
     event.UpdatedAt = updateDto.UpdatedAt.Value;
}

    return event; }

}
