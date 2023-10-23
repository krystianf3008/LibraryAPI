using LibraryAPI.Models.Users;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService service)
        {
            _userService = service;
        }
        [HttpPost("/api/user/register")]
        public async Task<ActionResult> Create([FromForm] RegisterUserDTO createUserDto)
        {
            var id = await _userService.CreateUser(createUserDto);
            return Created($"/api/user/{id}", null);
        }
        [HttpGet("/api/user/verify")]
        public async Task<ActionResult> Verify([FromQuery] string token)
        {
            await _userService.VerifyUser(token);
            return Ok();
        }
        [HttpPost("/api/user/forgot-password")]
        public async Task<ActionResult> ForgotPassword([FromForm] string email)
        {
            await _userService.CreateResetPasswordToken(email);
            return Ok();
        }
        [HttpPost("/api/user/reset-password")]
        public async Task<ActionResult> ResetPassword([FromForm] UserResetPasswordDTO userResetPasswordDto)
        {
            await _userService.ResetPassword(userResetPasswordDto);
            return Ok();
        }
        [HttpPost("/api/user/login")]
        public async Task<ActionResult> Login([FromForm] UserLoginDTO userLoginDto)
        {
            string token = await _userService.GenerateJwtToken(userLoginDto);
            return Ok(token);
        }
        [HttpGet("/api/user/")]
        [Authorize(Roles = "Admin,Moderator,User")]
        public async Task<ActionResult<List<UserDTO>>> GetAll()
        {
            return Ok(await _userService.GetAll());
        }
        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpGet("/api/user/{id}")]
        public async Task<ActionResult<UserDTO>> Get([FromRoute] Guid id)
        {
            return Ok(await _userService.GetById(id));
        }
       
        [Authorize(Roles = "Admin")]
        [HttpPost("/api/user/{id}/add-moderator")]
        public async Task AddModeratorRole([FromRoute] Guid id)
        {
           await _userService.AddModeratorRole(id);
        }
    }
}
