using ChatAPI.Entities;
using ChatAPI.Models.UsersDto;
using ChatAPI.Services;

namespace ChatAPI.Interface
{
    public interface IUserService
    {
        int RegistrationUser(RegistrationUserDto dto);
        void DeleteUser(int userId);
        void DeleteAllUser();
        IEnumerable<UserDto> GetAllUsers();
        UserDto GetUserById(int userId);
        void UpdateUser(int userId, UpdateUserDto dto);
        User GetUserDataById(int userId);
    }
}