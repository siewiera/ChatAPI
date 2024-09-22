using ChatAPI.Interface;
using ChatAPI.Models;
using ChatAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    [Route("api/chat/{channelId}/")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }


        [HttpPost]
        public ActionResult SendMessage([FromBody] SendMessageDto dto)
        {
            var id = _chatService.SendMessage(dto, channelId);

            return Created($"/api/chat/user/{id}", null);
        }
    }
}
