using ChatAPI.Interface;
using ChatAPI.Models.TokensDto;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    [Route("api/chat/user/{userId}/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<TokenDto>> GetAllTokensByUserId([FromRoute] int userId)
        { 
            var tokens = _tokenService.GetAllTokensByUserId(userId);

            return Ok(tokens);
        }

        [HttpGet("{tokenId}")]
        public ActionResult GetTokenByUserIdAndTokenId([FromRoute] int userId, [FromRoute] int tokenId)
        {
            var tokens = _tokenService.GetTokenByUserIdAndTokenId(userId, tokenId);

            return Ok(tokens);
        }


        [HttpPost]
        public ActionResult GenerateToken([FromBody] GenerateTokenDto dto, [FromRoute] int userId) 
        {
            var id = _tokenService.GenerateToken(dto, userId);

            return Created($"\"api/chat/{userId}/token{id}", null);
        }

        [HttpPut("{tokenId}")]
        public ActionResult UpdateToken([FromBody] UpdateTokenDto dto, [FromRoute] int userId, [FromRoute] int tokenId) 
        {
            _tokenService.UpdateToken(dto, userId, tokenId);

            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteAllTokens([FromRoute] int userId) 
        {
            if(userId == -1)
                _tokenService.DeleteAllTokens();
            else
                _tokenService.DeleteAllUserTokens(userId);

            return NoContent();
        }

        [HttpDelete("{tokenId}")]
        public ActionResult DeleteUserToken([FromRoute] int userId, [FromRoute] int tokenId) 
        {
            _tokenService.DeleteUserToken(userId, tokenId);

            return NoContent();
        }
    }
}
