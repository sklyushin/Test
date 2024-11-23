using Microsoft.AspNetCore.Mvc;
using HockeyEventService.APIs;
using HockeyEventService.APIs.Dtos;
using HockeyEventService.APIs.Errors;
using HockeyEventService.APIs.Common;

namespace HockeyEventService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EventsControllerBase : ControllerBase
{
    protected readonly IEventsService _service;
    public EventsControllerBase(IEventsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Event
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Event>> CreateEvent(EventCreateInput input)
    {
        var event = await _service.CreateEvent(input);
        
    return CreatedAtAction(nameof(Event), new { id = event.Id }, event); }

    /// <summary>
    /// Delete one Event
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteEvent([FromRoute()]
    EventWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteEvent(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Events
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Event>>> Events([FromQuery()]
    EventFindManyArgs filter)
    {
        return Ok(await _service.Events(filter));
    }

    /// <summary>
    /// Meta data about Event records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EventsMeta([FromQuery()]
    EventFindManyArgs filter)
    {
        return Ok(await _service.EventsMeta(filter));
    }

    /// <summary>
    /// Get one Event
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Event>> Event([FromRoute()]
    EventWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Event(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Event
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateEvent([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    EventUpdateInput eventUpdateDto)
    {
        try
        {
            await _service.UpdateEvent(uniqueId, eventUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple ChatRooms records to Event
    /// </summary>
    [HttpPost("{Id}/chatRooms")]
    public async Task<ActionResult> ConnectChatRooms([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    ChatRoomWhereUniqueInput[] chatRoomsId)
    {
        try
        {
            await _service.ConnectChatRooms(uniqueId, chatRoomsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple ChatRooms records from Event
    /// </summary>
    [HttpDelete("{Id}/chatRooms")]
    public async Task<ActionResult> DisconnectChatRooms([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromBody()]
    ChatRoomWhereUniqueInput[] chatRoomsId)
    {
        try
        {
            await _service.DisconnectChatRooms(uniqueId, chatRoomsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple ChatRooms records for Event
    /// </summary>
    [HttpGet("{Id}/chatRooms")]
    public async Task<ActionResult<List<ChatRoom>>> FindChatRooms([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    ChatRoomFindManyArgs filter)
    {
        try
        {
            return Ok(await _service.FindChatRooms(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple ChatRooms records for Event
    /// </summary>
    [HttpPatch("{Id}/chatRooms")]
    public async Task<ActionResult> UpdateChatRooms([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromBody()]
    ChatRoomWhereUniqueInput[] chatRoomsId)
    {
        try
        {
            await _service.UpdateChatRooms(uniqueId, chatRoomsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Place record for Event
    /// </summary>
    [HttpGet("{Id}/place")]
    public async Task<ActionResult<List<Place>>> GetPlace([FromRoute()]
    EventWhereUniqueInput uniqueId)
    {
        var place = await _service.GetPlace(uniqueId);
        return Ok(place);
    }

}
