using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Frontend.Models;

namespace Frontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _client;
        private readonly string _baseURL;

        public AuthService(HttpClient httpClient)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _baseURL = "http://localhost:5215/api/v1/";
        }

        public async Task<ResponseDTO> RegisterUserAsync(RegisterDTO user)
        {
            var response = await _client.PostAsJsonAsync($"{_baseURL}users/register-user", user);
            return await response.Content.ReadFromJsonAsync<ResponseDTO>();
        }

        public async Task<ResponseDTO> LoginAsync(LoginDTO login)
        {
             var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
            var response = await _client.PostAsJsonAsync($"{_baseURL}users/login", login);
            if(response.IsSuccessStatusCode)
            {
            var jsonString=await response.Content.ReadAsStringAsync();
            var responseDTO=JsonSerializer.Deserialize<ResponseDTO>(jsonString,options);
                
                return responseDTO;

            }else
            {
                return new ResponseDTO();
            }
            
        }

        // Additional authentication methods

        public async Task<ResponseDTO> LogoutAsync()
        {
            // Implement logout logic here
            return null;
        }

        public async Task<ResponseDTO> ResetPasswordAsync(string email)
        {
            // Implement password reset logic here
            return null;
        }

        // You can continue adding more authentication-related methods as needed
    }
}
