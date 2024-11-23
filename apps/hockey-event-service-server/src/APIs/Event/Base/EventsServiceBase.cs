using HockeyEventService.APIs;
using HockeyEventService.Infrastructure;
using HockeyEventService.APIs.Dtos;
using HockeyEventService.Infrastructure.Models;
using HockeyEventService.APIs.Errors;
using HockeyEventService.APIs.Extensions;
using HockeyEventService.APIs.Common;
using Microsoft.EntityFrameworkCore;

namespace HockeyEventService.APIs;

public abstract class EventsServiceBase : IEventsService
{
    protected readonly HockeyEventServiceDbContext _context;
    public EventsServiceBase(HockeyEventServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Event
    /// </summary>
    public async Task<Event> CreateEvent(EventCreateInput createDto)
    {
        var event = new EventDbModel
                    {
                CreatedAt = createDto.CreatedAt,
Description = createDto.Description,
Participants = createDto.Participants,
Time = createDto.Time,
UpdatedAt = createDto.UpdatedAt
};
      
            if (createDto.Id != null){
              event.Id = createDto.Id;
}
            if (createDto.ChatRooms != null)
              {
                  event.ChatRooms = await _context
                      .ChatRooms.Where(chatRoom => createDto.ChatRooms.Select(t => t.Id).Contains(chatRoom.Id))
                      .ToListAsync();
}

if (createDto.Place != null)
            {
                event.Place = await _context
                    .Places.Where(place => createDto.Place.Id == place.Id)
                    .FirstOrDefaultAsync();
}

_context.Events.Add(event);
await _context.SaveChangesAsync();

var result = await _context.FindAsync<EventDbModel>(event.Id);
      
              if (result == null)
              {
    throw new NotFoundException();
}
      
              return result.ToDto();
}

/// <summary>
/// Delete one Event
/// </summary>
public async Task DeleteEvent(EventWhereUniqueInput uniqueId)
{
    var event = await _context.Events.FindAsync(uniqueId.Id);
    if (event == null)
        {
        throw new NotFoundException();
    }

    _context.Events.Remove(event);
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find many Events
/// </summary>
public async Task<List<Event>> Events(EventFindManyArgs findManyArgs)
{
    var events = await _context
          .Events
  .Include(x => x.Place).Include(x => x.ChatRooms)
  .ApplyWhere(findManyArgs.Where)
  .ApplySkip(findManyArgs.Skip)
  .ApplyTake(findManyArgs.Take)
  .ApplyOrderBy(findManyArgs.SortBy)
  .ToListAsync();
    return events.ConvertAll(event => event.ToDto());
}

/// <summary>
/// Meta data about Event records
/// </summary>
public async Task<MetadataDto> EventsMeta(EventFindManyArgs findManyArgs)
{

    var count = await _context
.Events
.ApplyWhere(findManyArgs.Where)
.CountAsync();

    return new MetadataDto { Count = count };
}

/// <summary>
/// Get one Event
/// </summary>
public async Task<Event> Event(EventWhereUniqueInput uniqueId)
{
    var events = await this.Events(
              new EventFindManyArgs { Where = new EventWhereInput { Id = uniqueId.Id } }
  );
    var event = events.FirstOrDefault();
    if (event == null)
      {
        throw new NotFoundException();
    }

    return event;
}

/// <summary>
/// Update one Event
/// </summary>
public async Task UpdateEvent(EventWhereUniqueInput uniqueId, EventUpdateInput updateDto)
{
    var event = updateDto.ToModel(uniqueId);

    if (updateDto.ChatRooms != null)
    {
                  event.ChatRooms = await _context
                      .ChatRooms.Where(chatRoom => updateDto.ChatRooms.Select(t => t).Contains(chatRoom.Id))
                      .ToListAsync();
    }

    if (updateDto.Place != null)
    {
                event.Place = await _context
                    .Places.Where(place => updateDto.Place == place.Id)
                    .FirstOrDefaultAsync();
    }

    _context.Entry(event).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_context.Events.Any(e => e.Id == event.Id))
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
/// Connect multiple ChatRooms records to Event
/// </summary>
public async Task ConnectChatRooms(EventWhereUniqueInput uniqueId, ChatRoomWhereUniqueInput[] childrenIds)
{
    var parent = await _context
          .Events.Include(x => x.ChatRooms)
  .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .ChatRooms.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();
    if (children.Count == 0)
    {
        throw new NotFoundException();
    }

    var childrenToConnect = children.Except(parent.ChatRooms);

    foreach (var child in childrenToConnect)
    {
        parent.ChatRooms.Add(child);
    }

    await _context.SaveChangesAsync();
}

/// <summary>
/// Disconnect multiple ChatRooms records from Event
/// </summary>
public async Task DisconnectChatRooms(EventWhereUniqueInput uniqueId, ChatRoomWhereUniqueInput[] childrenIds)
{
    var parent = await _context
                            .Events.Include(x => x.ChatRooms)
                    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .ChatRooms.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();

    foreach (var child in children)
    {
        parent.ChatRooms?.Remove(child);
    }
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find multiple ChatRooms records for Event
/// </summary>
public async Task<List<ChatRoom>> FindChatRooms(EventWhereUniqueInput uniqueId, ChatRoomFindManyArgs eventFindManyArgs)
{
    var chatRooms = await _context
          .ChatRooms
  .Where(m => m.EventId == uniqueId.Id)
  .ApplyWhere(eventFindManyArgs.Where)
  .ApplySkip(eventFindManyArgs.Skip)
  .ApplyTake(eventFindManyArgs.Take)
  .ApplyOrderBy(eventFindManyArgs.SortBy)
  .ToListAsync();

    return chatRooms.Select(x => x.ToDto()).ToList();
}

/// <summary>
/// Update multiple ChatRooms records for Event
/// </summary>
public async Task UpdateChatRooms(EventWhereUniqueInput uniqueId, ChatRoomWhereUniqueInput[] childrenIds)
{
    var event = await _context
            .Events.Include(t => t.ChatRooms)
    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (event == null)
      {
        throw new NotFoundException();
    }

    var children = await _context
      .ChatRooms.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
      .ToListAsync();

    if (children.Count == 0)
    {
        throw new NotFoundException();
    }
  
      event.ChatRooms = children;
    await _context.SaveChangesAsync();
}

/// <summary>
/// Get a Place record for Event
/// </summary>
public async Task<Place> GetPlace(EventWhereUniqueInput uniqueId)
{
    var event = await _context
          .Events.Where(event => event.Id == uniqueId.Id)
  .Include(event => event.Place)
  .FirstOrDefaultAsync();
    if (event == null)
  {
        throw new NotFoundException();
    }
    return event.Place.ToDto();
}

}
