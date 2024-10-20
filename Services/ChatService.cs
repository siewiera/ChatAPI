using AutoMapper;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Interface;
using ChatAPI.Models.ConversationsDto;
using ChatAPI.Models.MessagesDto;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Services
{
    public class ChatService : IChatService
    {
        private readonly ChatDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ChatService> _logger;
        private readonly IChannelService _channelService;
        private readonly IUserService _userService;

        public ChatService(ChatDbContext dbContext, IMapper mapper, ILogger<ChatService> logger, IChannelService channelService, IUserService userService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _channelService = channelService;
            _userService = userService;
        }

        public int AddMessage(int channelId, int userId, AddMessageDto dto) 
        {
            var channel = _channelService.GetChannelDataById(channelId);
            var user = _userService.GetUserDataById(userId);

            var message = _mapper.Map<Message>(dto);
            message.UserId = userId;
            message.Conversation.ChannelId = channelId;
            message.Conversation.CreatAt = DateTime.Now;
            message.CreationDate = DateTime.Now;

            _dbContext.Add(message);
            _dbContext.SaveChanges();

            return message.Id;
        }

        public List<MessageDto> GetAllMessageByChannelId(int channelId) 
        {
            var channel = _channelService.GetChannelDataById(channelId);

            var messages = _dbContext
                .Messages
                .Include(m => m.User)
                .Include(m => m.Conversation)
                .Where(m => m.Conversation.ChannelId == channelId);

            var messagesDto = _mapper.Map<List<MessageDto>>(messages);

            return messagesDto;
        }

        public List<MessageDto> GetAllMessageByChannelIdAndUserId(int channelId, int userId)
        {
            var channel = _channelService.GetChannelDataById(channelId);
            var user = _userService.GetUserDataById(userId);

            var messages = _dbContext
                .Messages
                .Include(m => m.User)
                .Include(m => m.Conversation)
                .Where(m => m.Conversation.ChannelId == channelId 
                    && m.UserId == userId);
             

            var messagesDto = _mapper.Map<List<MessageDto>>(messages);

            return messagesDto;
        }

    }
}
