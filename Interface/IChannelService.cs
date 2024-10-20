using ChatAPI.Entities;
using ChatAPI.Models.ChannelsDto;

namespace ChatAPI.Interface
{
    public interface IChannelService
    {
        Channel GetChannelDataById(int channelId);
        int CreateChannel(CreateChannelDto dto);
        void DeleteChannel(int channelId);
        void DeleteAllChannel();
        IEnumerable<ChannelDto> GetAllChannel();
        ChannelDto GetChannelById(int channelId);
        void UpdateChannel(int channelId, UpdateChannelDto dto);
    }
}