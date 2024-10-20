﻿using AutoMapper;
using ChatAPI.Entities;
using ChatAPI.Models.AccountDto;
using ChatAPI.Models.ChannelsDto;
using ChatAPI.Models.ConversationsDto;
using ChatAPI.Models.MessagesDto;
using ChatAPI.Models.TokensDto;
using ChatAPI.Models.UsersDto;

namespace ChatAPI
{
    public class ChatMappingProfile : Profile
    {
        public ChatMappingProfile()
        {
            //CreateMap<Conversation, ConversationDto>()
            //    .ForMember(c => c.ChannelName, c => c.MapFrom(c => c.Channel.Name));


            CreateMap<Conversation, ConversationDto>()
                .ForMember(cd => cd.ChannelName, c => c.MapFrom(c => c.Channel.Name));

            CreateMap<User, UserDto>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<RegistrationUserDto, User>()
                .ForMember(t => t.Tokens, r => r.MapFrom(dto => new List<Token>()
                {
                    new Token()
                    {
                        TokenNumber = dto.TokenNumber,
                        Type = dto.Type,
                        ExpiryDate = dto.ExpiryDate,
                        Used = dto.Used,
                        UserId = dto.UserId
                    },
                }));

            CreateMap<Token, TokenDto>()
                .ForMember(td => td.Nickname, t => t.MapFrom(t => t.User.Nickname));
            CreateMap<GenerateTokenDto, Token>();
            CreateMap<UpdateTokenDto, Token>()
                .ForMember(t => t.User, u => u.MapFrom(u => u.Active));

            CreateMap<Channel, ChannelDto>();
            CreateMap<CreateChannelDto, Channel>();
            CreateMap<UpdateChannelDto, Channel>();

            CreateMap<Message, MessageDto>()
                .ForMember(md => md.Nickname, m => m.MapFrom(m => m.User.Nickname))
                .ForMember(md => md.CreatAt, m => m.MapFrom(m => m.Conversation.CreatAt))
                .ForMember(md => md.Name, m => m.MapFrom(m => m.Conversation.Channel.Name));
            CreateMap<AddMessageDto, Message>()
                .ForMember(m => m.Conversation, a => a.MapFrom(a => new Conversation()
                {
                    CreatAt = a.CreatAt, 
                    ChannelId = a.ChannelId 
                }));

            CreateMap<ResetPasswordDto, Token>()
               .ForMember(t => t.User, u => u.MapFrom(u => u.Password))
               .ForMember(t => t.User, u => u.MapFrom(u => u.ModifiedAt));
            CreateMap<ChangeEmailDto, Token>()
                .ForMember(t => t.User, u => u.MapFrom(u => u.Email))
                .ForMember(t => t.User, u => u.MapFrom(u => u.ModifiedAt));


            /* CreateMap<CreateRestaurant, Restaurant>()
                 .ForMember(r => r.Adress, r => r.MapFrom(dto => new Adress()
                 { City = r.City, PostalCode = r.PostalCode, Street = r.Street }));*/
        }
    }
}
