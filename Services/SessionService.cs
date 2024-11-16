using AutoMapper;
using ChatAPI.Entities;
using ChatAPI.Exceptions;
using ChatAPI.Interface;
using ChatAPI.Models.ChannelsDto;
using ChatAPI.Models.SessionsDto;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.CompilerServices;

namespace ChatAPI.Services
{
    public class SessionService : ISessionService
    {
        private readonly ChatDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private string _configFilePath = Path.Combine("Settings", "sessionSettings.json");
        private SessionSettings _time;
        private readonly TimeSpan _sessionTimeout = TimeSpan.FromMinutes(2);

        public SessionService(ChatDbContext dbContext, IMapper mapper, IUserService userService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userService = userService;
            _time = LoadingSessionSettingsFile();
            _sessionTimeout = TimeSpan.FromMinutes(_time.sesstionTime);
        }

        private SessionSettings LoadingSessionSettingsFile()
        {
            try
            {
                var jsonConfig = File.ReadAllText(_configFilePath);
                return JsonConvert.DeserializeObject<SessionSettings>(jsonConfig);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CheckUserSessionExists(int userId)
        {
            bool check = true;
            var user = _userService.GetUserDataById(userId);
            var session = _dbContext
                .Sessions
                .FirstOrDefault(s => s.UserId == userId);

            if (session is not null)
                throw new ConflictException("User session exists");
        }

        public Session GetUserSessionData(int userId)
        {
            var userSession = _dbContext
                .Sessions
                .Include(s => s.User)
                .FirstOrDefault(s => s.UserId == userId);

            if (userSession is null)
                throw new NotFoundException("User has no session");

            return userSession;
        }

        public int CreateUserSession(int userId, string ip)
        {
            var user = _userService.GetUserDataById(userId);
            CheckUserSessionExists(userId);

            CreateSessionDto dto = new CreateSessionDto();

            var session = _mapper.Map<Session>(dto);
            session.SessionId = Guid.NewGuid();
            session.LoginTime = DateTime.Now;
            session.LastAction = DateTime.Now;
            session.Ip = ip;
            session.UserId = userId;

            _dbContext.Sessions.Add(session);
            _dbContext.SaveChanges();

            return session.Id;
        }

        //public IEnumerable<SessionDto> GetAllUserSessions(int userId) 
        //{
        //    var user = _userService.GetUserDataById(userId);

        //    var userSessions = _dbContext
        //        .Sessions
        //        .Include(s => s.User)
        //        .Where(s => s.UserId == userId)
        //        .ToList();

        //    var sessionsDtos = _mapper.Map<List<SessionDto>>(userSessions);

        //    return sessionsDtos;
        //}

        public SessionDto GetUserSession(int userId)
        {
            var user = _userService.GetUserDataById(userId);
            var userSession = GetUserSessionData(userId);

            var userSessionDto = _mapper.Map<SessionDto>(userSession);

            return userSessionDto;
        }

        public void DeleteUserSession(int userId)
        {
            var user = _userService.GetUserDataById(userId);
            var userSession = GetUserSessionData(userId);

            _dbContext.Sessions.Remove(userSession);
            _dbContext.SaveChanges();
        }

        public void IncreasingSessionTime(int userId, string ip)
        {
            var user = _userService.GetUserDataById(userId);
            var userSession = GetUserSessionData(userId);

            userSession.LastAction = DateTime.Now;
            userSession.Ip = ip;

            _dbContext.SaveChanges();
        }

        //usuwanie wszystkich nieaktywnych sesji
        public void RemoveInactiveSession() 
        {
            var thresholdTime = DateTime.Now - _sessionTimeout;
            var sessions = _dbContext
                .Sessions
                .Where(s => s.LastAction <= thresholdTime)
                .ToList();

            if (sessions.Any())
            {
                _dbContext.Sessions.RemoveRange(sessions);
                _dbContext.SaveChanges();
            }
        }

    }
}
