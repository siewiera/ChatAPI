using AutoMapper;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Interface;
using ChatAPI.Models;

namespace ChatAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ChatDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(ChatDbContext dbContext, IMapper mapper, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }


        public UserDto GetUserById(int id)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == id);

            if (user is null)
                throw new NotFoundException("User not found");

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }


        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _dbContext
                .Users
                .ToList();

            var usersDtos = _mapper.Map<List<UserDto>>(users);

            return usersDtos;
        }

        public int AddUser(AddUserDto dto)
        {
            var user = _mapper.Map<User>(dto);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        public void DeleteUser(int id) 
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == id);

            if (user is null)
                throw new NotFoundException("User not found");

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public void UpdateUser(int id, UpdateUserDto dto) 
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == id);

            if (user is null)
                throw new NotFoundException("User not found");

            user.Nickname = dto.Nickname;
            user.Email = dto.Email;
            user.Password = dto.Password;
            user.Blocked = dto.Blocked;

            _dbContext.SaveChanges();
        }
    }
}
