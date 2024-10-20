using AutoMapper;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Interface;
using ChatAPI.Models.ChannelsDto;

namespace ChatAPI.Services
{
    public class ChannelService : IChannelService
    {
        private readonly ChatDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ChannelService> _logger;
        private readonly BCryptHash _bCryptHash;

        public ChannelService(ChatDbContext dbContext, IMapper mapper, ILogger<ChannelService> logger, BCryptHash bCryptHash)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _bCryptHash = bCryptHash;
        }

        public Channel GetChannelDataById(int channelId) 
        {
            var channel = _dbContext
                .Channels
                .FirstOrDefault(c => c.Id == channelId);

            if (channel is null)
                throw new NotFoundException("Channel not found");

            return channel;
        }

        public int CreateChannel(CreateChannelDto dto)
        {
            var channel = _mapper.Map<Channel>(dto);

            if (channel.hasPassword)
                channel.Password = _bCryptHash.HashPassword(channel.Password);
            else
                channel.Password = "";

            channel.Blocked = false;

            _dbContext.Channels.Add(channel);
            _dbContext.SaveChanges();

            return channel.Id;
        }

        public void UpdateChannel(int channelId, UpdateChannelDto dto)
        {
            var channel = GetChannelDataById(channelId);

            if(dto.Name == "")
                dto.Name = channel.Name;

            channel.Name = dto.Name;
            channel.hasPassword = dto.hasPassword;

            if (dto.hasPassword)
                channel.Password = _bCryptHash.HashPassword(dto.Password);
            else
                channel.Password = "";

            _dbContext.SaveChanges();
        }

        public void DeleteChannel(int channelId)
        {
            var channel = GetChannelDataById(channelId);

            _dbContext.Channels.Remove(channel);
            _dbContext.SaveChanges();
        }

        public void DeleteAllChannel()
        {
            var channels = _dbContext
                .Channels
                .ToList();

            _dbContext.Channels.RemoveRange(channels);
            _dbContext.SaveChanges();
        }

        public IEnumerable<ChannelDto> GetAllChannel()
        {
            var channels = _dbContext
                .Channels
                .ToList();

            var channelsDto = _mapper.Map<List<ChannelDto>>(channels);

            return channelsDto;
        }

        public ChannelDto GetChannelById(int channelId)
        {
            var channel = GetChannelDataById(channelId);
            var channelDto = _mapper.Map<ChannelDto>(channel);

            return channelDto;
        }

    }
}
