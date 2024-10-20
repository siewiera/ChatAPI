using ChatAPI.Models.ConversationsDto;
using ChatAPI.Models.MessagesDto;

namespace ChatAPI.Interface
{
    public interface IChatService
    {
        int AddMessage(int channelId, int userId, AddMessageDto dto);
        List<MessageDto> GetAllMessageByChannelId(int channelId);
        List<MessageDto> GetAllMessageByChannelIdAndUserId(int channelId, int userId);
    }
}