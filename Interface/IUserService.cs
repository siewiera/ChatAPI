using ChatAPI.Models;
using ChatAPI.Services;

namespace ChatAPI.Interface
{
    public interface IUserService
    {
        int AddUser(AddUserDto dto);
        void DeleteUser(int id);
        IEnumerable<UserDto> GetAllUsers();
        UserDto GetUserById(int id);
        void UpdateUser(int id, UpdateUserDto dto);
    }
}