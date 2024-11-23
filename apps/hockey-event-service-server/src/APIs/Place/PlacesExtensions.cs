using HockeyEventService.APIs.Dtos;
using HockeyEventService.Infrastructure.Models;

namespace HockeyEventService.APIs.Extensions;

public static class PlacesExtensions
{
    public static Place ToDto(this PlaceDbModel model)
    {
        return new Place
        {
            CreatedAt = model.CreatedAt,
            Events = model.Events?.Select(x => x.Id).ToList(),
            Id = model.Id,
            Location = model.Location,
            Name = model.Name,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PlaceDbModel ToModel(
        this PlaceUpdateInput updateDto,
        PlaceWhereUniqueInput uniqueId
    )
    {
        var place = new PlaceDbModel
        {
            Id = uniqueId.Id,
            Location = updateDto.Location,
            Name = updateDto.Name
        };

        if (updateDto.CreatedAt != null)
        {
            place.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            place.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return place;
    }
}
