using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backed.Models;
using Backed.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("/api/v1/users/")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetUsers()
        {
            var response = await _userService.GetUsersAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetUser(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            if (response.Value == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("register-user")]
        public async Task<ActionResult<ResponseDTO>> CreateUser(RegisterDTO user)
        {
            var response = await _userService.CreateUserAsync(user);
            return response;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDTO>> UpdateUser(int id, User user)
        {
            var response = await _userService.UpdateUserAsync(id, user);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO>> DeleteUser(int id)
        {
            var response = await _userService.DeleteUserAsync(id);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDTO>> Login(LoginDTO login)
        {
            var response = await _userService.AuthenticateAsync(login.Email, login.Password);
            return Ok(response);
        }
    }
}
