using HockeyEventService.APIs;
using HockeyEventService.APIs.Common;
using HockeyEventService.APIs.Dtos;
using HockeyEventService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace HockeyEventService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PlacesControllerBase : ControllerBase
{
    protected readonly IPlacesService _service;

    public PlacesControllerBase(IPlacesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Place
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Place>> CreatePlace(PlaceCreateInput input)
    {
        var place = await _service.CreatePlace(input);

        return CreatedAtAction(nameof(Place), new { id = place.Id }, place);
    }

    /// <summary>
    /// Delete one Place
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeletePlace([FromRoute()] PlaceWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeletePlace(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Places
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Place>>> Places([FromQuery()] PlaceFindManyArgs filter)
    {
        return Ok(await _service.Places(filter));
    }

    /// <summary>
    /// Meta data about Place records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PlacesMeta([FromQuery()] PlaceFindManyArgs filter)
    {
        return Ok(await _service.PlacesMeta(filter));
    }

    /// <summary>
    /// Get one Place
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Place>> Place([FromRoute()] PlaceWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Place(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Place
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdatePlace(
        [FromRoute()] PlaceWhereUniqueInput uniqueId,
        [FromQuery()] PlaceUpdateInput placeUpdateDto
    )
    {
        try
        {
            await _service.UpdatePlace(uniqueId, placeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Events records to Place
    /// </summary>
    [HttpPost("{Id}/events")]
    public async Task<ActionResult> ConnectEvents(
        [FromRoute()] PlaceWhereUniqueInput uniqueId,
        [FromQuery()] EventWhereUniqueInput[] eventsId
    )
    {
        try
        {
            await _service.ConnectEvents(uniqueId, eventsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Events records from Place
    /// </summary>
    [HttpDelete("{Id}/events")]
    public async Task<ActionResult> DisconnectEvents(
        [FromRoute()] PlaceWhereUniqueInput uniqueId,
        [FromBody()] EventWhereUniqueInput[] eventsId
    )
    {
        try
        {
            await _service.DisconnectEvents(uniqueId, eventsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Events records for Place
    /// </summary>
    [HttpGet("{Id}/events")]
    public async Task<ActionResult<List<Event>>> FindEvents(
        [FromRoute()] PlaceWhereUniqueInput uniqueId,
        [FromQuery()] EventFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindEvents(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Events records for Place
    /// </summary>
    [HttpPatch("{Id}/events")]
    public async Task<ActionResult> UpdateEvents(
        [FromRoute()] PlaceWhereUniqueInput uniqueId,
        [FromBody()] EventWhereUniqueInput[] eventsId
    )
    {
        try
        {
            await _service.UpdateEvents(uniqueId, eventsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
