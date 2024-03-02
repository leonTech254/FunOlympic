using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Frontend.Models;
using Frontend.Utils;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace Frontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _client;
        private readonly string _baseURL;
        private TokenServices _tokenServices;
        private readonly PopUpMessages _popUpMessages;
        private NavigationManager _navigationManager;

        public AuthService(HttpClient httpClient,TokenServices tokenServices,PopUpMessages popUpMessages,NavigationManager navigationManager)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _baseURL = "http://localhost:5215/api/v1/";
            _tokenServices=tokenServices;
            _popUpMessages=popUpMessages;
            _navigationManager=navigationManager;
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

    if (response.IsSuccessStatusCode)
    {
        var jsonString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(jsonString);
        JObject jsonObject = JObject.Parse(jsonString);
        Console.WriteLine(jsonObject["value"]["responseData"]+"i am here ");
        string msg = jsonObject["value"]["message"].ToString();
        if(jsonObject["value"]["responseData"].ToString()=="")
        {
             await _popUpMessages.sweetAlert("Wrong username/password","Authentication","error");
             return null;
        }

        string token = jsonObject["value"]["responseData"]["token"].ToString();
        
        Console.WriteLine(token);
        await _tokenServices.SetTokenAsync(token);
        await _popUpMessages.sweetAlert(msg,"Authentication","success");
        //  await Task.Delay(3000);
        _navigationManager.NavigateTo("/profile");

        var responseDTO = JsonSerializer.Deserialize<ResponseDTO>(jsonString, options);

        if (responseDTO != null)
        {         
            return new ResponseDTO();
        }
        else
        {
            Console.WriteLine("i am null");
            return null;
        }
    }
    else
    {
        // Handle the case where the response is not successful
        return null;
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
