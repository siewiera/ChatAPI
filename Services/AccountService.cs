using AutoMapper;
using ChatAPI.Entities;
using ChatAPI.Enum;
using ChatAPI.Exceptions;
using ChatAPI.Interface;
using ChatAPI.Models.AccountDto;
using ChatAPI.Models.TokensDto;
using ChatAPI.Services.SendMail;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly ChatDbContext _dbContext;
        //private readonly IMapper _mapper;
        //private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly BCryptHash _bCryptHash;
        private readonly ISendMail _sendMail;

        //public AccountService(ChatDbContext dbContext, IMapper mapper, IUserService userService, ITokenService tokenService, BCryptHash bCryptHash, ISendMail sendMail)
        public AccountService(ChatDbContext dbContext, ITokenService tokenService, BCryptHash bCryptHash, ISendMail sendMail)
        {
            _dbContext = dbContext;
            //_mapper = mapper;
            //_userService = userService;
            _tokenService = tokenService;
            _bCryptHash = bCryptHash;
            _sendMail = sendMail;
        }

        public Token GetTokenDataByTokenNumber(string tokenNumber) 
        {
            var token = _dbContext
                    .Tokens
                    .Include(t => t.User)
                    .FirstOrDefault(t => t.TokenNumber.ToString() == tokenNumber);
 
            if (token is null)
                throw new NotFoundException("Token not found");
            //else if (token.User.Active == true && token.Type == Enum.TokenType.Activation)
                //throw new ConflictException("User account has already been activated");
            else if (token.Used == true)
                throw new ConflictException("Token has already used");
            else if (token.ExpiryDate < DateTime.Now)
                throw new ConflictException("Token expired");

            return token;
        }

        //public string TokenVerification(string tokenNumber, UpdateAccountDto dto)
        //{
        //    var token = GetTokenDataByTokenNumber(tokenNumber);
        //    string message = "";
        //    //var user = _userService.GetUserDataById(token.UserId);

        //    //dto.Email = user.Email;
        //    //dto.Password = user.Password;
        //    //dto.ModifiedAt = DateTime.Now;
        //    //dto.Active = user.Active;
        //    //dto.Used = token.Used;

        //    if (dto != null)
        //    {
        //        dto.Email = token.User.Email;
        //        dto.Password = token.User.Password;
        //        dto.ModifiedAt = DateTime.Now;
        //        dto.Active = token.User.Active;
        //        dto.Used = token.Used;
        //    }


        //    switch (token.Type)
        //    {
        //        case Enum.TokenType.Activation:
        //            message = AccountActivation(token);
        //            break;
        //        case Enum.TokenType.PasswordReset:
        //            message = ResetPassword(token.User, dto);
        //            break;
        //        case Enum.TokenType.EmailVerification:
        //            message = ChangeEmail(token.User, dto);
        //            break;
        //        default:
        //            throw new NotFoundException("Incorrect token type");
        //            break;
        //    }

        //    return message;
        //}

        //public void AccountActivation(string tokenNumber, UpdateAccountDto dto)
        public string AccountActivation(string tokenNumber)
        {
            var token = GetTokenDataByTokenNumber(tokenNumber);

            string message = "The account has been activated";
            token.Used = true;
            token.User.Active = true;

            _dbContext.SaveChanges();

            var subject = "Account activated";
            var body = "Your account has been successfully activated";
            _sendMail.Send(token.User.Email, subject, body);

            return message;
        }


        public string ResetPassword(string tokenNumber, ResetPasswordDto dto)
        {
            var token = GetTokenDataByTokenNumber(tokenNumber);

            string message = "The password has been reset";

            token.User.Password = _bCryptHash.HashPassword(dto.Password);
            token.User.ModifiedAt = DateTime.Now;
            token.Used = true;

            _dbContext.SaveChanges();

            var subject = "Password changed successfully";
            var body = "The password has been changed successfully";
            _sendMail.Send(token.User.Email, subject, body);

            return message;
        }

        public string ChangeEmail(string tokenNumber, ChangeEmailDto dto) 
        {
            var token = GetTokenDataByTokenNumber(tokenNumber);

            string message = "The email address has been changed correctly";

            var email = _dbContext
                .Users
                .Where(u => u.Email == dto.Email)
                .ToList();

            if (email.Count() >= 1)
                throw new ConflictException("The provided Email address already exists");

            token.User.Email = dto.Email;
            token.User.ModifiedAt = DateTime.Now;
            token.Used = true;

            _dbContext.SaveChanges();

            var subject = "Email has been activated";
            var body = "Your email address has been successfully changed";
            _sendMail.Send(dto.Email, subject, body);

            return message;
        }
    }
}
