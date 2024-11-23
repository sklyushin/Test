using HockeyEventService.APIs;
using HockeyEventService.Infrastructure;
using HockeyEventService.APIs.Dtos;
using HockeyEventService.Infrastructure.Models;
using HockeyEventService.APIs.Errors;
using HockeyEventService.APIs.Extensions;
using HockeyEventService.APIs.Common;
using Microsoft.EntityFrameworkCore;

namespace HockeyEventService.APIs;

public abstract class ChatRoomsServiceBase : IChatRoomsService
{
    protected readonly HockeyEventServiceDbContext _context;
    public ChatRoomsServiceBase(HockeyEventServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one ChatRoom
    /// </summary>
    public async Task<ChatRoom> CreateChatRoom(ChatRoomCreateInput createDto)
    {
        var chatRoom = new ChatRoomDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Messages = createDto.Messages,
            Participants = createDto.Participants,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            chatRoom.Id = createDto.Id;
        }
        if (createDto.Event != null)
        {
            chatRoom.Event = await _context
                .Events.Where(event => createDto.Event.Id == event.Id)
                    .FirstOrDefaultAsync();
            }

_context.ChatRooms.Add(chatRoom);
              await _context.SaveChangesAsync();

var result = await _context.FindAsync<ChatRoomDbModel>(chatRoom.Id);
      
              if (result == null)
              {
                  throw new NotFoundException();
              }
      
              return result.ToDto();}

    /// <summary>
    /// Delete one ChatRoom
    /// </summary>
    public async Task DeleteChatRoom(ChatRoomWhereUniqueInput uniqueId)
{
    var chatRoom = await _context.ChatRooms.FindAsync(uniqueId.Id);
    if (chatRoom == null)
    {
        throw new NotFoundException();
    }

    _context.ChatRooms.Remove(chatRoom);
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find many ChatRooms
/// </summary>
public async Task<List<ChatRoom>> ChatRooms(ChatRoomFindManyArgs findManyArgs)
{
    var chatRooms = await _context
          .ChatRooms
  .Include(x => x.Event)
  .ApplyWhere(findManyArgs.Where)
  .ApplySkip(findManyArgs.Skip)
  .ApplyTake(findManyArgs.Take)
  .ApplyOrderBy(findManyArgs.SortBy)
  .ToListAsync();
    return chatRooms.ConvertAll(chatRoom => chatRoom.ToDto());
}

/// <summary>
/// Meta data about ChatRoom records
/// </summary>
public async Task<MetadataDto> ChatRoomsMeta(ChatRoomFindManyArgs findManyArgs)
{

    var count = await _context
.ChatRooms
.ApplyWhere(findManyArgs.Where)
.CountAsync();

    return new MetadataDto { Count = count };
}

/// <summary>
/// Get one ChatRoom
/// </summary>
public async Task<ChatRoom> ChatRoom(ChatRoomWhereUniqueInput uniqueId)
{
    var chatRooms = await this.ChatRooms(
              new ChatRoomFindManyArgs { Where = new ChatRoomWhereInput { Id = uniqueId.Id } }
  );
    var chatRoom = chatRooms.FirstOrDefault();
    if (chatRoom == null)
    {
        throw new NotFoundException();
    }

    return chatRoom;
}

/// <summary>
/// Update one ChatRoom
/// </summary>
public async Task UpdateChatRoom(ChatRoomWhereUniqueInput uniqueId, ChatRoomUpdateInput updateDto)
{
    var chatRoom = updateDto.ToModel(uniqueId);

    if (updateDto.Event != null)
    {
        chatRoom.Event = await _context
            .Events.Where(event => updateDto.Event == event.Id)
            .FirstOrDefaultAsync();
    }

    _context.Entry(chatRoom).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_context.ChatRooms.Any(e => e.Id == chatRoom.Id))
        {
            throw new NotFoundException();
        }
        else
        {
            throw;
        }
    }
}

/// <summary>
/// Get a Event record for ChatRoom
/// </summary>
public async Task<Event> GetEvent(ChatRoomWhereUniqueInput uniqueId)
{
    var chatRoom = await _context
          .ChatRooms.Where(chatRoom => chatRoom.Id == uniqueId.Id)
  .Include(chatRoom => chatRoom.Event)
  .FirstOrDefaultAsync();
    if (chatRoom == null)
    {
        throw new NotFoundException();
    }
    return chatRoom.Event.ToDto();
}

}
