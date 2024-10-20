using ChatAPI.Entities;
using ChatAPI.Models.TokensDto;

namespace ChatAPI.Interface
{
    public interface ITokenService
    {
        void DeleteAllUserTokens(int userId);
        void DeleteUserToken(int userId, int tokenId);
        void DeleteAllTokens();
        int GenerateToken(GenerateTokenDto dto, int userId);
        List<TokenDto> GetAllTokensByUserId(int userId);
        TokenDto GetTokenByUserIdAndTokenId(int userId, int tokenId);
        void UpdateToken(UpdateTokenDto dto, int userId, int tokenId);
    }
}