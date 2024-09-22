using AutoMapper;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Interface;
using ChatAPI.Models;

namespace ChatAPI.Services
{
    public class ChatService : IChatService
    {
        private readonly ChatDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ChatService> _logger;

        public ChatService(ChatDbContext dbContext, IMapper mapper, ILogger<ChatService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public int SendMessage(SendMessageDto dto, int channelId)
        {
            var channel = _dbContext.Channels.FirstOrDefault(c => c.Id == channelId);

            if (channel is null)
                throw new NotFoundException("Channel not found");

            var sendMessage = _mapper.Map<Conversation>(dto);

            _dbContext.Conversations.Add(sendMessage);
            _dbContext.SaveChanges();

            return sendMessage.Id;
        }


        public IEnumerable<ConversationDto> Get { get; set; }
    }
}
