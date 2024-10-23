using ChatAPI.Models.ConversationsDto;
using ChatAPI.Models.MessagesDto;

namespace ChatAPI.Interface
{
    public interface IChatService
    {
        int AddMessage(int channelId, int userId, AddMessageDto dto);
        List<MessageDto> GetAllChannelMessages(int channelId);
        List<MessageDto> GetAllChannelMessageByUserId(int channelId, int userId);
        void DeleteChannelMessageById(int channelId, int messageId);
        void DeleteAllChannelMessages(int channelId);
    }
}