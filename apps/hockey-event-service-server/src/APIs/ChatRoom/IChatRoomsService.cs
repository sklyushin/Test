using HockeyEventService.APIs.Common;
using HockeyEventService.APIs.Dtos;

namespace HockeyEventService.APIs;

public interface IChatRoomsService
{
    /// <summary>
    /// Create one ChatRoom
    /// </summary>
    public Task<ChatRoom> CreateChatRoom(ChatRoomCreateInput chatroom);

    /// <summary>
    /// Delete one ChatRoom
    /// </summary>
    public Task DeleteChatRoom(ChatRoomWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many ChatRooms
    /// </summary>
    public Task<List<ChatRoom>> ChatRooms(ChatRoomFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about ChatRoom records
    /// </summary>
    public Task<MetadataDto> ChatRoomsMeta(ChatRoomFindManyArgs findManyArgs);

    /// <summary>
    /// Get one ChatRoom
    /// </summary>
    public Task<ChatRoom> ChatRoom(ChatRoomWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one ChatRoom
    /// </summary>
    public Task UpdateChatRoom(ChatRoomWhereUniqueInput uniqueId, ChatRoomUpdateInput updateDto);

    /// <summary>
    /// Get a Event record for ChatRoom
    /// </summary>
    public Task<Event> GetEvent(ChatRoomWhereUniqueInput uniqueId);
}
