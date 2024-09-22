using AutoMapper;
using ChatAPI.Entities;
using ChatAPI.Models;

namespace ChatAPI
{
    public class ChatMappingProfile : Profile
    {
        public ChatMappingProfile()
        {
            //CreateMap<Conversation, ConversationDto>()
            //    .ForMember(c => c.ChannelName, c => c.MapFrom(c => c.Channel.Name));

            CreateMap<Conversation, ConversationDto>();
            CreateMap<User, UserDto>();
            CreateMap<Message, MessageDto>();
            CreateMap<UserConversation, UserConversationDto>();
            CreateMap<Channel, ChannelDto>();

            CreateMap<AddUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            CreateMap<CreateChannelDto, Channel>();
            CreateMap<UpdateChannelDto, Channel>();

            CreateMap<SendMessageDto, Conversation>();


            //CreateMap<CreateChannel, Channel>();


            /* CreateMap<CreateRestaurant, Restaurant>()
                 .ForMember(r => new Adress()
                 { City = r.City, PostalCode = r.PostalCode, Street = r.Street });*/
        }
    }
}
