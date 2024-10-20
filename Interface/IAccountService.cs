using ChatAPI.Entities;
using ChatAPI.Enum;
using ChatAPI.Models;
using ChatAPI.Models.AccountDto;
using ChatAPI.Services;

namespace ChatAPI.Interface
{
    public interface IAccountService
    {
        Token GetTokenDataByTokenNumber(string tokenNumber);
        //string TokenVerification(string tokenNumber, UpdateAccountDto dto);
        string AccountActivation(string tokenNumber);
        string ResetPassword(string tokenNumber, ResetPasswordDto dto);
        string ChangeEmail(string tokenNumber, ChangeEmailDto dto);

    }
}