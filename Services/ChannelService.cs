using AutoMapper;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Interface;
using ChatAPI.Models;

namespace ChatAPI.Services
{
    public class ChannelService : IChannelService
    {
        private readonly ChatDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ChannelService> _logger;

        public ChannelService(ChatDbContext dbContext, IMapper mapper, ILogger<ChannelService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public int CreateChannel(CreateChannelDto dto)
        {
            var channel = _mapper.Map<Channel>(dto);

            _dbContext.Channels.Add(channel);
            _dbContext.SaveChanges();

            return channel.Id;
        }

        public void UpdateChannel(int id, UpdateChannelDto dto)
        {
            var channel = _dbContext
                .Channels
                .FirstOrDefault(x => x.Id == id);

            if (channel is null)
                throw new NotFoundException("Channel not found");

            channel.Name = dto.Name;
            channel.hasPassword = dto.hasPassword;
            if (dto.hasPassword)
                channel.Password = dto.Password;
            channel.Blocked = dto.Blocked;

            _dbContext.SaveChanges();
        }

        public void DeleteChannel(int id)
        {
            var channel = _dbContext
                 .Channels
                 .FirstOrDefault(x => x.Id == id);

            if (channel is null)
                throw new NotFoundException("Channel not found");

            _dbContext.Channels.Remove(channel);
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

        public ChannelDto GetChannelById(int id)
        {
            var channel = _dbContext
                .Channels
                .FirstOrDefault(c => c.Id == id);

            if (channel is null)
                throw new NotFoundException("Channel not found");

            var channelDto = _mapper.Map<ChannelDto>(channel);

            return channelDto;
        }

    }
}
