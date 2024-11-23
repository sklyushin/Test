using HockeyEventService.APIs.Dtos;
using HockeyEventService.Infrastructure.Models;

namespace HockeyEventService.APIs.Extensions;

public static class ChatRoomsExtensions
{
    public static ChatRoom ToDto(this ChatRoomDbModel model)
    {
        return new ChatRoom
        {
            CreatedAt = model.CreatedAt,
            Event = model.EventId,
            Id = model.Id,
            Messages = model.Messages,
            Participants = model.Participants,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ChatRoomDbModel ToModel(
        this ChatRoomUpdateInput updateDto,
        ChatRoomWhereUniqueInput uniqueId
    )
    {
        var chatRoom = new ChatRoomDbModel
        {
            Id = uniqueId.Id,
            Messages = updateDto.Messages,
            Participants = updateDto.Participants
        };

        if (updateDto.CreatedAt != null)
        {
            chatRoom.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Event != null)
        {
            chatRoom.EventId = updateDto.Event;
        }
        if (updateDto.UpdatedAt != null)
        {
            chatRoom.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return chatRoom;
    }
}
