using ChatAPI.Entities;
using ChatAPI.Models.ChannelsDto;
using ChatAPI.Models.SessionsDto;

namespace ChatAPI.Interface
{
    public interface ISessionService
    {
        void CheckUserSessionExists(int userId);
        int CreateUserSession(int userId, string ip);
        void DeleteUserSession(int userId);
        SessionDto GetUserSession(int userId);
        Session GetUserSessionData(int userId);
        void IncreasingSessionTime(int userId, string ip);
        void RemoveInactiveSession();
    }
}