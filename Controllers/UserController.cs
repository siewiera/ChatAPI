using AutoMapper;
using ChatAPI.Entities;
using ChatAPI.Interface;
using ChatAPI.Models;
using ChatAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Controllers
{
    [Route("api/chat/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteUser([FromRoute] int id) 
        {
            _userService.DeleteUser(id);

            return NoContent();
        }


        [HttpPost]
        public ActionResult AddUser([FromBody] AddUserDto dto)
        { 
            var id = _userService.AddUser(dto);

            return Created($"/api/chat/user/{id}", null);
        }


        [HttpPut("{id}")]
        public ActionResult UpdateUser([FromBody] UpdateUserDto dto, [FromRoute] int id) 
        { 
            _userService.UpdateUser(id, dto);

            return Ok();
        }


        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAllUsers() 
        {
            var usersDtos = _userService.GetAllUsers();

            return Ok(usersDtos);
        }


        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUserById([FromRoute] int id) 
        {
            var userDto = _userService.GetUserById(id);

            return Ok(userDto);
        }
    }
}
