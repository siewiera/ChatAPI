using AutoMapper;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Interface;
using ChatAPI.Models.TokensDto;
using ChatAPI.Models.UsersDto;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Mail;
using System.Xml.Linq;

namespace ChatAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ChatDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly BCryptHash _bCryptHash;
        private readonly ISendMail _sendMail;
        //private readonly ITokenService _tokenService;

        //public UserService(ChatDbContext dbContext, IMapper mapper, ILogger<UserService> logger, BCryptHash bCryptHash, ISendMail sendMail, ITokenService tokenService)
        public UserService(ChatDbContext dbContext, IMapper mapper, ILogger<UserService> logger, BCryptHash bCryptHash, ISendMail sendMail)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _bCryptHash = bCryptHash;
            _sendMail = sendMail;
            //_tokenService = tokenService;
        }


        public User GetUserDataById(int userId)
        {
            var user = _dbContext
                .Users
                .Include(u => u.Tokens)
                .Include(u => u.Messages)
                .FirstOrDefault(u => u.Id == userId);

            if (user is null)
                throw new NotFoundException("User not found");

            return user;
        }

        public UserDto GetUserById(int userId)
        {
            var user = GetUserDataById(userId);
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _dbContext
                .Users
                .Include(u => u.Messages)
                .Include(u => u.Tokens)
                .ToList();

            var usersDtos = _mapper.Map<List<UserDto>>(users);

            return usersDtos;
        }


        public int RegistrationUser(RegistrationUserDto dto) 
        {
            string errorMessage = "";

            var email = _dbContext
                .Users
                .Where(u => u.Email == dto.Email)
                .ToList();

            var nickname = _dbContext
                .Users
                .Where(u => u.Nickname == dto.Nickname)
                .ToList();

            //if (email.Count() >= 1)
            //    errorMessage = "The provided Email address already exists";
            //else if (nickname.Count() >= 1)
            //    errorMessage = "The provided Nickname already exists";

            if (errorMessage != "")
                throw new ConflictException(errorMessage);

            var tokenNumber = Guid.NewGuid();

            var user = _mapper.Map<User>(dto);
            user.Password = _bCryptHash.HashPassword(dto.Password);
            user.CreatedAt = DateTime.Now;
            user.Blocked = false;
            user.Active = false;
            user.Tokens = new List<Token>()
            {
                new Token()
                {
                    TokenNumber = tokenNumber,
                    Type = Enum.TokenType.Activation,
                    ExpiryDate = DateTime.Now.AddDays(1),
                    Used = false,
                    UserId = user.Id,
                }
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            string subject = "Account activation";
            string tokenLink = $"<a href='https://localhost:7202/api/chat/account/account-activation/{tokenNumber.ToString()}'>https://localhost:7202/api/chat/account/account-activation/{tokenNumber.ToString()}</a>";
            string body = $"To confirm the account activation, click on the generated token <br><br> {tokenLink}";

            _sendMail.Send(user.Email, subject, body);

            return user.Id;
        }


        public void DeleteUser(int userId) 
        {
            var user = GetUserDataById(userId);
            user.Blocked = false;
            //_dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public void DeleteAllUser() 
        {
            var users = _dbContext
                .Users
                .ToList();

            //users.ForEach(user => user.Blocked = true);
            _dbContext.Users.RemoveRange(users);
            _dbContext.SaveChanges();
        }


        public void UpdateUser(int userId, UpdateUserDto dto)
        {
            var user = GetUserDataById(userId);

            bool verifyCheck = _bCryptHash.VerifyPassword(dto.OldPassword, user.Password);

            if (!verifyCheck)
                throw new NotFoundException("Incorrect password");

            user.Password = dto.NewPassword;
            user.Nickname = dto.Nickname;
            user.ModifiedAt = DateTime.Now;

            _dbContext.SaveChanges();
        }
    }
}
