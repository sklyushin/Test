using HockeyEventService.APIs;
using HockeyEventService.Infrastructure;
using HockeyEventService.APIs.Dtos;
using HockeyEventService.Infrastructure.Models;
using HockeyEventService.APIs.Errors;
using HockeyEventService.APIs.Extensions;
using HockeyEventService.APIs.Common;
using Microsoft.EntityFrameworkCore;

namespace HockeyEventService.APIs;

public abstract class PlacesServiceBase : IPlacesService
{
    protected readonly HockeyEventServiceDbContext _context;
    public PlacesServiceBase(HockeyEventServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Place
    /// </summary>
    public async Task<Place> CreatePlace(PlaceCreateInput createDto)
    {
        var place = new PlaceDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Location = createDto.Location,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            place.Id = createDto.Id;
        }
        if (createDto.Events != null)
        {
            place.Events = await _context
                .Events.Where(event => createDto.Events.Select(t => t.Id).Contains(event.Id))
                      .ToListAsync();
              }

_context.Places.Add(place);
              await _context.SaveChangesAsync();

var result = await _context.FindAsync<PlaceDbModel>(place.Id);
      
              if (result == null)
              {
                  throw new NotFoundException();
              }
      
              return result.ToDto();}

    /// <summary>
    /// Delete one Place
    /// </summary>
    public async Task DeletePlace(PlaceWhereUniqueInput uniqueId)
{
    var place = await _context.Places.FindAsync(uniqueId.Id);
    if (place == null)
    {
        throw new NotFoundException();
    }

    _context.Places.Remove(place);
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find many Places
/// </summary>
public async Task<List<Place>> Places(PlaceFindManyArgs findManyArgs)
{
    var places = await _context
          .Places
  .Include(x => x.Events)
  .ApplyWhere(findManyArgs.Where)
  .ApplySkip(findManyArgs.Skip)
  .ApplyTake(findManyArgs.Take)
  .ApplyOrderBy(findManyArgs.SortBy)
  .ToListAsync();
    return places.ConvertAll(place => place.ToDto());
}

/// <summary>
/// Meta data about Place records
/// </summary>
public async Task<MetadataDto> PlacesMeta(PlaceFindManyArgs findManyArgs)
{

    var count = await _context
.Places
.ApplyWhere(findManyArgs.Where)
.CountAsync();

    return new MetadataDto { Count = count };
}

/// <summary>
/// Get one Place
/// </summary>
public async Task<Place> Place(PlaceWhereUniqueInput uniqueId)
{
    var places = await this.Places(
              new PlaceFindManyArgs { Where = new PlaceWhereInput { Id = uniqueId.Id } }
  );
    var place = places.FirstOrDefault();
    if (place == null)
    {
        throw new NotFoundException();
    }

    return place;
}

/// <summary>
/// Update one Place
/// </summary>
public async Task UpdatePlace(PlaceWhereUniqueInput uniqueId, PlaceUpdateInput updateDto)
{
    var place = updateDto.ToModel(uniqueId);

    if (updateDto.Events != null)
    {
        place.Events = await _context
            .Events.Where(event => updateDto.Events.Select(t => t).Contains(event.Id))
            .ToListAsync();
    }

    _context.Entry(place).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_context.Places.Any(e => e.Id == place.Id))
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
/// Connect multiple Events records to Place
/// </summary>
public async Task ConnectEvents(PlaceWhereUniqueInput uniqueId, EventWhereUniqueInput[] childrenIds)
{
    var parent = await _context
          .Places.Include(x => x.Events)
  .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Events.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();
    if (children.Count == 0)
    {
        throw new NotFoundException();
    }

    var childrenToConnect = children.Except(parent.Events);

    foreach (var child in childrenToConnect)
    {
        parent.Events.Add(child);
    }

    await _context.SaveChangesAsync();
}

/// <summary>
/// Disconnect multiple Events records from Place
/// </summary>
public async Task DisconnectEvents(PlaceWhereUniqueInput uniqueId, EventWhereUniqueInput[] childrenIds)
{
    var parent = await _context
                            .Places.Include(x => x.Events)
                    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Events.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();

    foreach (var child in children)
    {
        parent.Events?.Remove(child);
    }
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find multiple Events records for Place
/// </summary>
public async Task<List<Event>> FindEvents(PlaceWhereUniqueInput uniqueId, EventFindManyArgs placeFindManyArgs)
{
    var events = await _context
          .Events
  .Where(m => m.PlaceId == uniqueId.Id)
  .ApplyWhere(placeFindManyArgs.Where)
  .ApplySkip(placeFindManyArgs.Skip)
  .ApplyTake(placeFindManyArgs.Take)
  .ApplyOrderBy(placeFindManyArgs.SortBy)
  .ToListAsync();

    return events.Select(x => x.ToDto()).ToList();
}

/// <summary>
/// Update multiple Events records for Place
/// </summary>
public async Task UpdateEvents(PlaceWhereUniqueInput uniqueId, EventWhereUniqueInput[] childrenIds)
{
    var place = await _context
            .Places.Include(t => t.Events)
    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (place == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Events.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
      .ToListAsync();

    if (children.Count == 0)
    {
        throw new NotFoundException();
    }

    place.Events = children;
    await _context.SaveChangesAsync();
}

}
