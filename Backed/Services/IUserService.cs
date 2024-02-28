using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backed.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backed.Services
{
    
       

        public interface IUserService
    {
        Task<ActionResult<ResponseDTO>> GetUsersAsync();
        Task<ActionResult<ResponseDTO>> GetUserByIdAsync(int id);
        Task<ActionResult<ResponseDTO>> CreateUserAsync(RegisterDTO user);
        Task<ActionResult<ResponseDTO>> UpdateUserAsync(int id, User user);
        Task<ActionResult<ResponseDTO>> DeleteUserAsync(int id);
        Task<ActionResult<ResponseDTO>> AuthenticateAsync(string email, string password);
    }
    }