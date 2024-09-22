using ChatAPI.Interface;
using ChatAPI.Models;
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
            var id = _channelService.CreateChannel(dto);

            return Created($"api/chat/channel{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateChannel([FromRoute] int id, [FromBody] UpdateChannelDto dto)
        {
            _channelService.UpdateChannel(id, dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteChannel([FromRoute] int id)
        {
            _channelService.DeleteChannel(id);

            return NoContent();
        }

        [HttpGet]
        public ActionResult<IEnumerable<ChannelDto>> GetAllChannel()
        {
            var channels = _channelService.GetAllChannel();

            return Ok(channels);
        }

        [HttpGet("{id}")]
        public ActionResult<ChannelDto> GetChannelById([FromRoute] int id) 
        { 
            var channel = _channelService.GetChannelById(id);

            return Ok(channel);
        }
    }
}
