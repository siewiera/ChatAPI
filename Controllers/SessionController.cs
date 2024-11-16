using ChatAPI.Interface;
using ChatAPI.Models.SessionsDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChatAPI.Controllers
{
    [Route("api/chat/user/{userId}/session")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public string GetClientIPAddress()
        {
            var forwardedFor = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

            if (!string.IsNullOrEmpty(forwardedFor))
                ip = forwardedFor.Split(',').First().Trim();    //pobieranie z nagłówka X-Forwarded-For pierwszy adres ip - gdy istnieje

            return ip;
        }

        [HttpGet]
        public ActionResult<SessionDto> GetUserSession([FromRoute] int userId) 
        {
            var userSession = _sessionService.GetUserSession(userId);

            return Ok(userSession);
        }

        [HttpPost]
        public ActionResult CreateUserSession([FromRoute] int userId)
        {
            string ip = GetClientIPAddress();

            var userSession = _sessionService.CreateUserSession(userId, ip);

            return Created($"api/chat/{userId}/session{userSession}", null);
        }

        [HttpDelete]
        public ActionResult DeleteUserSession([FromRoute] int userId) 
        {
            _sessionService.DeleteUserSession(userId);

            return NoContent();
        }

        [HttpPut]
        public ActionResult IncreasingSessionTime([FromRoute] int userId) 
        {
            string ip = GetClientIPAddress();
            _sessionService.IncreasingSessionTime(userId, ip);

            return Ok();
        }
    }
}
