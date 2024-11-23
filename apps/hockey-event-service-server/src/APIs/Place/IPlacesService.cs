using HockeyEventService.APIs.Common;
using HockeyEventService.APIs.Dtos;

namespace HockeyEventService.APIs;

public interface IPlacesService
{
    /// <summary>
    /// Create one Place
    /// </summary>
    public Task<Place> CreatePlace(PlaceCreateInput place);

    /// <summary>
    /// Delete one Place
    /// </summary>
    public Task DeletePlace(PlaceWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Places
    /// </summary>
    public Task<List<Place>> Places(PlaceFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Place records
    /// </summary>
    public Task<MetadataDto> PlacesMeta(PlaceFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Place
    /// </summary>
    public Task<Place> Place(PlaceWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Place
    /// </summary>
    public Task UpdatePlace(PlaceWhereUniqueInput uniqueId, PlaceUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Events records to Place
    /// </summary>
    public Task ConnectEvents(PlaceWhereUniqueInput uniqueId, EventWhereUniqueInput[] eventsId);

    /// <summary>
    /// Disconnect multiple Events records from Place
    /// </summary>
    public Task DisconnectEvents(PlaceWhereUniqueInput uniqueId, EventWhereUniqueInput[] eventsId);

    /// <summary>
    /// Find multiple Events records for Place
    /// </summary>
    public Task<List<Event>> FindEvents(
        PlaceWhereUniqueInput uniqueId,
        EventFindManyArgs EventFindManyArgs
    );

    /// <summary>
    /// Update multiple Events records for Place
    /// </summary>
    public Task UpdateEvents(PlaceWhereUniqueInput uniqueId, EventWhereUniqueInput[] eventsId);
}
