using ChatAPI.Interface;
using ChatAPI.Models;
using ChatAPI.Models.MessagesDto;
using ChatAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    [Route("api/chat/conversation/channel{channelId}/")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("user{userId}")]
        public ActionResult AddMessage([FromRoute] int channelId, [FromRoute] int userId, [FromBody] AddMessageDto dto) 
        {
            var message = _chatService.AddMessage(channelId, userId, dto);

            return Created($"api/chat/conversation/channel{channelId}/user{userId}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<MessageDto>> GetAllMessageByChannelId([FromRoute] int channelId) 
        {
            var messages = _chatService.GetAllMessageByChannelId(channelId);

            return Ok(messages);
        }

        [HttpGet("user{userId}")]
        public ActionResult<IEnumerable<MessageDto>> GetAllMessageByChannelIdAndUserId([FromRoute] int channelId, [FromRoute] int userId)
        {
            var messages = _chatService.GetAllMessageByChannelIdAndUserId(channelId, userId);

            return Ok(messages);
        }


        //[HttpPost]
        //public ActionResult SendMessage([FromBody] CreateUserConversationDto ucdto, [FromBody] CreateMessageDto mdto, [FromRoute] int channelId)
        //{
        //    var id = _chatService.SendMessage(ucdto, mdto, channelId);

        //    return Created($"/api/chat/{id}/conversation", null);
        //}

        //[HttpGet]
        //public ActionResult<IEnumerable<SendMessageDto>> GetAllMessagel()
        //{
        //    var message = _chatService.GetAllMessage();

        //    return Ok(message);
        //}
    }
}
