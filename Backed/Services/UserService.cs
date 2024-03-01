using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Backed.Models;
using DBconnection_namespace;
using Backed.Utills;

namespace Backed.Services
{
    public class UserService : IUserService
    {
        private readonly DBconn _context;

        private readonly Jwt _jwt;

        public UserService(DBconn context,Jwt jwt)
        {
            _context = context;
            _jwt=jwt;
        }

        public async Task<ActionResult<ResponseDTO>> GetUsersAsync()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return new ResponseDTO { message = "Success", responseData = users };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { message = $"Error: {ex.Message}", responseData = null };
            }
        }

        public async Task<ActionResult<ResponseDTO>> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return new ResponseDTO { message = "User not found", responseData = null };
                }
                return new ResponseDTO { message = "Success", responseData = user };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { message = $"Error: {ex.Message}", responseData = null };
            }
        }

        public async Task<ActionResult<ResponseDTO>> CreateUserAsync(RegisterDTO registerDTO)
        {
            User user=new User()
            {
                Email=registerDTO.Email,
                FirstName=registerDTO.FirstName,
                Password=registerDTO.Password,
                LastName=registerDTO.LastName,
                Role="User"  
            };
            
            try
            {
                
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return new ResponseDTO { message = "User created successfully", responseData = user };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { message = $"Error: {ex.Message}", responseData = null };
            }
        }

        public async Task<ActionResult<ResponseDTO>> UpdateUserAsync(int id, User user)
        {
            try
            {
                if (id != user.Id)
                {
                    return new ResponseDTO { message = "Invalid user ID", responseData = null };
                }

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ResponseDTO { message = "User updated successfully", responseData = user };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { message = $"Error: {ex.Message}", responseData = null };
            }
        }

        public async Task<ActionResult<ResponseDTO>> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return new ResponseDTO { message = "User not found", responseData = null };
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return new ResponseDTO { message = "User deleted successfully", responseData = null };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { message = $"Error: {ex.Message}", responseData = null };
            }
        }

        public async Task<ActionResult<ResponseDTO>> AuthenticateAsync(string email, string password)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

                if (user == null)
                {
                    return new ResponseDTO { message = "Invalid email or password", responseData = null };
                }
                var custResponseData= new {userInfo=user,token=_jwt.GenerateToken(user)};

                return new ResponseDTO { message = "Login successful", responseData = custResponseData };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { message = $"Error: {ex.Message}", responseData = null };
            }
        }
    }
}
