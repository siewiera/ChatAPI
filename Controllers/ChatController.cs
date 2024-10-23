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
            var messages = _chatService.GetAllChannelMessages(channelId);

            return Ok(messages);
        }

        [HttpGet("user{userId}")]
        public ActionResult<IEnumerable<MessageDto>> GetAllMessageByChannelIdAndUserId([FromRoute] int channelId, [FromRoute] int userId)
        {
            var messages = _chatService.GetAllChannelMessageByUserId(channelId, userId);

            return Ok(messages);
        }

        [HttpDelete("message{messageId}")]
        public ActionResult DeleteChannelMessageById([FromRoute] int channelId, [FromRoute] int messageId)
        {
            _chatService.DeleteChannelMessageById(channelId, messageId);

            return NoContent();
        }

        [HttpDelete()]
        public ActionResult DeleteChannelMessageById([FromRoute] int channelId)
        {
            _chatService.DeleteAllChannelMessages(channelId);

            return NoContent();
        }
    }
}
