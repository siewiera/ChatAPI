using ChatAPI.Entities;
using ChatAPI.Interface;
using ChatAPI.Models;
using ChatAPI.Models.AccountDto;
using ChatAPI.Models.TokensDto;
using ChatAPI.Models.UsersDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChatAPI.Controllers
{
    //[Route("api/chat/account/{tokenNumber}")]
    [Route("api/chat/account/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        //[HttpGet]
        //[HttpPost]
        //public async Task<ActionResult> Verification([FromRoute] string tokenNumber)
        //{
        //    var token = _accountService.GetTokenDataByTokenNumber(tokenNumber);
        //    string message = "";
        //    var body = await new StreamReader(Request.Body).ReadToEndAsync();

        //    switch (token.Type)
        //    {
        //        case Enum.TokenType.Activation:

        //            //message = _accountService.AccountActivation(tokenNumber);
        //            AccountActivation(tokenNumber);

        //            break;
        //        case Enum.TokenType.PasswordReset:

        //            ResetPasswordDto resetPasswordDto = null;

        //            if (!string.IsNullOrWhiteSpace(body))
        //                resetPasswordDto = JsonConvert.DeserializeObject<ResetPasswordDto>(body);

        //            message = _accountService.ResetPassword(tokenNumber, resetPasswordDto);
        //            //ResetPassword(tokenNumber, resetPasswordDto);

        //            break;
        //        case Enum.TokenType.EmailChange:

        //            ChangeEmailDto changeEmailDto = null;

        //            if (!string.IsNullOrWhiteSpace(body))
        //                changeEmailDto = JsonConvert.DeserializeObject<ChangeEmailDto>(body);

        //            message = _accountService.ChangeEmail(tokenNumber, changeEmailDto);

        //            break;
        //        default:
        //            NotFound();
        //            break;
        //    }

        //    return Ok(message);
        //}

        [HttpGet("account-activation/{tokenNumber}")]
        public ActionResult AccountActivation([FromRoute] string tokenNumber)
        {
            string message = _accountService.AccountActivation(tokenNumber);

            return Ok(message);
        }

        [HttpPost("reset-password/{tokenNumber}")]
        public ActionResult ResetPassword([FromRoute] string tokenNumber, [FromBody] ResetPasswordDto dto)
        {
            string message = _accountService.ResetPassword(tokenNumber, dto);

            return Ok(message);
        }

        [HttpPost("email-verification/{tokenNumber}")]
        public ActionResult ChangeEmail([FromRoute] string tokenNumber, [FromBody] ChangeEmailDto dto)
        {
            string message = _accountService.ChangeEmail(tokenNumber, dto);

            return Ok(message);
        }

        //[HttpPost]
        //public async Task<ActionResult> AccountActivation([FromRoute] string tokenNumber)
        ////public ActionResult AccountActivation([FromRoute] string tokenNumber)
        //{
        //    var body = await new StreamReader(Request.Body).ReadToEndAsync();
        //    UpdateAccountDto dto = null;

        //    if (!string.IsNullOrWhiteSpace(body))
        //        dto = JsonConvert.DeserializeObject<UpdateAccountDto>(body);

        //    string message = _accountService.TokenVerification(tokenNumber, dto);

        //    return Ok(message);
        //}


    }
}
