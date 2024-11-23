using HockeyEventService.APIs.Dtos;
using HockeyEventService.APIs.Common;

namespace HockeyEventService.APIs;

public interface IEventsService
{
    /// <summary>
    /// Create one Event
    /// </summary>
    public Task<Event> CreateEvent(EventCreateInput event);
    /// <summary>
    /// Delete one Event
    /// </summary>
    public Task DeleteEvent(EventWhereUniqueInput uniqueId);
    /// <summary>
    /// Find many Events
    /// </summary>
    public Task<List<Event>> Events(EventFindManyArgs findManyArgs);
    /// <summary>
    /// Meta data about Event records
    /// </summary>
    public Task<MetadataDto> EventsMeta(EventFindManyArgs findManyArgs);
    /// <summary>
    /// Get one Event
    /// </summary>
    public Task<Event> Event(EventWhereUniqueInput uniqueId);
    /// <summary>
    /// Update one Event
    /// </summary>
    public Task UpdateEvent(EventWhereUniqueInput uniqueId, EventUpdateInput updateDto);
    /// <summary>
    /// Connect multiple ChatRooms records to Event
    /// </summary>
    public Task ConnectChatRooms(EventWhereUniqueInput uniqueId, ChatRoomWhereUniqueInput[] chatRoomsId);
    /// <summary>
    /// Disconnect multiple ChatRooms records from Event
    /// </summary>
    public Task DisconnectChatRooms(EventWhereUniqueInput uniqueId, ChatRoomWhereUniqueInput[] chatRoomsId);
    /// <summary>
    /// Find multiple ChatRooms records for Event
    /// </summary>
    public Task<List<ChatRoom>> FindChatRooms(EventWhereUniqueInput uniqueId, ChatRoomFindManyArgs ChatRoomFindManyArgs);
    /// <summary>
    /// Update multiple ChatRooms records for Event
    /// </summary>
    public Task UpdateChatRooms(EventWhereUniqueInput uniqueId, ChatRoomWhereUniqueInput[] chatRoomsId);
    /// <summary>
    /// Get a Place record for Event
    /// </summary>
    public Task<Place> GetPlace(EventWhereUniqueInput uniqueId);
}
