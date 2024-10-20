using ChatAPI.Interface;
using ChatAPI.Models.ChannelsDto;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    [Route("api/chat/channel")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelService _channelService;

        public ChannelController(IChannelService channelService)
        {
            _channelService = channelService;
        }

        [HttpPost]
        public ActionResult CreateChannel([FromBody] CreateChannelDto dto)
        {
            var channelId = _channelService.CreateChannel(dto);

            return Created($"api/chat/channel{channelId}", null);
        }

        [HttpPut("{channelId}")]
        public ActionResult UpdateChannel([FromRoute] int channelId, [FromBody] UpdateChannelDto dto)
        {        
            _channelService.UpdateChannel(channelId, dto);

            return Ok();
        }

        [HttpDelete("{channelId}")]
        public ActionResult DeleteChannel([FromRoute] int channelId)
        {
            _channelService.DeleteChannel(channelId);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult DeleteAllChannel()
        {
            _channelService.DeleteAllChannel();

            return NoContent();
        }

        [HttpGet]
        public ActionResult<IEnumerable<ChannelDto>> GetAllChannel()
        {
            var channels = _channelService.GetAllChannel();

            return Ok(channels);
        }

        [HttpGet("{channelId}")]
        public ActionResult<ChannelDto> GetChannelById([FromRoute] int channelId) 
        { 
            var channel = _channelService.GetChannelById(channelId);

            return Ok(channel);
        }
    }
}
