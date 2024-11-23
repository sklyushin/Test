using Microsoft.AspNetCore.Mvc;
using HockeyEventService.APIs;
using HockeyEventService.APIs.Dtos;
using HockeyEventService.APIs.Errors;
using HockeyEventService.APIs.Common;

namespace HockeyEventService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ChatRoomsControllerBase : ControllerBase
{
    protected readonly IChatRoomsService _service;
    public ChatRoomsControllerBase(IChatRoomsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one ChatRoom
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<ChatRoom>> CreateChatRoom(ChatRoomCreateInput input)
    {
        var chatRoom = await _service.CreateChatRoom(input);

        return CreatedAtAction(nameof(ChatRoom), new { id = chatRoom.Id }, chatRoom);
    }

    /// <summary>
    /// Delete one ChatRoom
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteChatRoom([FromRoute()]
    ChatRoomWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteChatRoom(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many ChatRooms
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<ChatRoom>>> ChatRooms([FromQuery()]
    ChatRoomFindManyArgs filter)
    {
        return Ok(await _service.ChatRooms(filter));
    }

    /// <summary>
    /// Meta data about ChatRoom records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ChatRoomsMeta([FromQuery()]
    ChatRoomFindManyArgs filter)
    {
        return Ok(await _service.ChatRoomsMeta(filter));
    }

    /// <summary>
    /// Get one ChatRoom
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<ChatRoom>> ChatRoom([FromRoute()]
    ChatRoomWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.ChatRoom(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one ChatRoom
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateChatRoom([FromRoute()]
    ChatRoomWhereUniqueInput uniqueId, [FromQuery()]
    ChatRoomUpdateInput chatRoomUpdateDto)
    {
        try
        {
            await _service.UpdateChatRoom(uniqueId, chatRoomUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Event record for ChatRoom
    /// </summary>
    [HttpGet("{Id}/event")]
    public async Task<ActionResult<List<Event>>> GetEvent([FromRoute()]
    ChatRoomWhereUniqueInput uniqueId)
    {
        var event = await _service.GetEvent(uniqueId);
            return Ok(event); }

}
