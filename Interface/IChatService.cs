using ChatAPI.Models;

namespace ChatAPI.Interface
{
    public interface IChatService
    {
        IEnumerable<ConversationDto> Get { get; set; }

        int SendMessage(SendMessageDto dto, int channelId);
    }
}