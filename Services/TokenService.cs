using AutoMapper;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Interface;
using ChatAPI.Models.TokensDto;
using ChatAPI.Services.SendMail;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly ChatDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly ISendMail _sendMail;
        private readonly IUserService _userService;

        public TokenService(ChatDbContext dbContext, IMapper mapper, ILogger<UserService> logger, ISendMail sendMail, IUserService userService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _sendMail = sendMail;
            _userService = userService;
        }


        public TokenDto GetTokenByUserIdAndTokenId(int userId, int tokenId)
        {
            var user = _userService.GetUserById(userId);
            var token = _dbContext
                .Tokens
                .FirstOrDefault(t => t.Id == tokenId);

            if (token is null || token.UserId != userId)
                throw new NotFoundException("Token not found");

            var tokenDto = _mapper.Map<TokenDto>(token);

            return tokenDto;
        }

        public List<TokenDto> GetAllTokensByUserId(int userId)
        {
            var user = _userService.GetUserDataById(userId);
            var tokensDto = _mapper.Map<List<TokenDto>>(user.Tokens);

            return tokensDto;
        }

        public int GenerateToken(GenerateTokenDto dto, int userId)
        {
            var user = _userService.GetUserDataById(userId);
            var token = _mapper.Map<Token>(dto);

            token.TokenNumber = Guid.NewGuid();
            token.ExpiryDate = DateTime.Now.AddDays(1);
            token.Used = false;
            token.UserId = userId;

            string subject = "Account activation";
            string change = "account activation";
            string type = "account-activation";
            
            switch (dto.Type)
            { 
                case Enum.TokenType.Activation:                 
                    break;
                case Enum.TokenType.PasswordReset:

                    subject = "Reset Password";
                    change = "password reset";
                    type = "reset-password";

                    break;
                case Enum.TokenType.EmailChange:

                    subject = "Change email";
                    change = "email change";
                    type = "email-verification";

                    break;
                //case Enum.TokenType.ActivatingNewEmail:

                //    subject = "Activation new email";
                //    change = "email change";
                //    //subject = "Activation new email";
                //    //body = "Your email address has been successfully changed";
                    break;
                default:
                    throw new NotFoundException("Incorrect token type");
                    break;
            }
            
            _dbContext.Tokens.Add(token);
            _dbContext.SaveChanges();

            string tokenLink = $"<a href='https://localhost:7202/api/chat/account/{type}/{token.TokenNumber.ToString()}'>https://localhost:7202/api/chat/account/{type}/{token.TokenNumber.ToString()}</a>";
            string body = $"To confirm the {change}, click on the generated token <br><br> {tokenLink}";

            _sendMail.Send(user.Email, subject, body);

            return token.Id;
        }

        public void UpdateToken(UpdateTokenDto dto, int userId, int tokenId)
        {
            var user = _userService.GetUserDataById(userId);
            var token = _dbContext
                .Tokens
                .FirstOrDefault(t => t.Id == tokenId);

            if (token is null || token.UserId != userId)
                throw new NotFoundException("Token not found");

            token.Used = dto.Used;
            token.UserId = userId;
            token.User.Active = dto.Active;

            _dbContext.SaveChanges();
        }

        public void DeleteAllUserTokens(int userId)
        {
            var user = _userService.GetUserDataById(userId);

            _dbContext.RemoveRange(user.Tokens);
            _dbContext.SaveChanges();
        }

        public void DeleteUserToken(int userId, int tokenId)
        {
            var user = _userService.GetUserDataById(userId);
            var token = _dbContext
                .Tokens
                .FirstOrDefault(t => t.Id == tokenId);

            if (token is null || token.UserId != userId)
                throw new NotFoundException("Token not found");

            _dbContext.Tokens.Remove(token);
            _dbContext.SaveChanges();
        }

        public void DeleteAllTokens() 
        {
            var tokens = _dbContext
                .Tokens
                .ToList();

            _dbContext.Tokens.RemoveRange(tokens);
            _dbContext.SaveChanges();
        }
    }
}
