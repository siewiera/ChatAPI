using ChatAPI.Models;

namespace ChatAPI.Interface
{
    public interface IChannelService
    {
        int CreateChannel(CreateChannelDto dto);
        void DeleteChannel(int id);
        IEnumerable<ChannelDto> GetAllChannel();
        ChannelDto GetChannelById(int id);
        void UpdateChannel(int id, UpdateChannelDto dto);
    }
}