using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Frontend.Models;
using Frontend.Utils;
using Microsoft.JSInterop;

namespace Frontend.Services
{
    public class TokenServices
    {
   
    private readonly IJSRuntime _jsRuntime;
    private readonly PopUpMessages _popUpMessages;
   

    public TokenServices(IJSRuntime jsRuntime,PopUpMessages popUpMessages)
    {
        _jsRuntime = jsRuntime;
        _popUpMessages=popUpMessages;
    }

    public async Task<string> GetTokenAsync()
    {
         string token= await _jsRuntime.InvokeAsync<string>("localStorageFunctions.getItem", "token");
         if(token==null)
         {
            await _popUpMessages.sweetAlert("Access Denied You need To Login First","Authentication","error");
            return "null";
         }
         return token;
    }

 public async Task LogOut()
    {
        await SetTokenAsync("null");
        await _jsRuntime.InvokeAsync<string>("localStorageFunctions.removeItem","token");
    }


    public async Task SetTokenAsync(String token)
    {
        await _jsRuntime.InvokeAsync<string>("localStorageFunctions.setItem","token", token);
    }


    public async Task<UserModel> getUserDetails()
    {
        UserModel user=new UserModel();
        var tokenHandler = new JwtSecurityTokenHandler();
        string token = await GetTokenAsync();
        if (token == "null")
        {
            return user;
        }
        try
        {
            Console.WriteLine("!_12");
            var tokenS = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var userId = tokenS?.Claims.First(claim => claim.Type == "user_id").Value;
            var userName = tokenS?.Claims.First(claim => claim.Type == "username").Value;
            var email = tokenS?.Claims.First(claim => claim.Type == "user_email").Value;
            var user_role = tokenS?.Claims.First(claim => claim.Type == "user_role").Value;
            var fullname = tokenS?.Claims.First(claim => claim.Type == "fullname").Value;
            // var ProfileUrl = tokenS?.Claims.First(claim => claim.Type == "profile_url").Value;
            
            
            // Console.WriteLine(userId+" hello leon token");
            user=new UserModel()
            {
                userRole=user_role,
                username=userName,
                Email=email,
                user_id=userId,
                Firstname=fullname,

            };

            return user;



            // Do something with the decoded information
        }
        catch (Exception ex)
        {
            Console.WriteLine("My error "+ex);
             await _popUpMessages.sweetAlert("Access Denied You need To Login First","Authentication","error");
            return null;;

            // Token validation failed
            // Handle the exception accordingly
        }


    }
    
    public async Task<bool> CheckTokenValidity()
    {
        String token =await GetTokenAsync();
         var tokenHandler = new JwtSecurityTokenHandler();
         if(token!="null")
         {
            var tokenS = tokenHandler.ReadToken(token) as JwtSecurityToken;
        DateTime? expirationDate = tokenS?.ValidTo;
        Console.WriteLine("Token Expiry time "+expirationDate);
        if(expirationDate<DateTime.Now)
        {
        await _popUpMessages.sweetAlert("Access Denied You need To Login First","Authentication","error");
        return false;
        }else
        {
        return true;
        }

         }else
         {
            await _popUpMessages.sweetAlert("Access Denied You need To Login First","Authentication","error");
           return false; 
         }
        
    }


    public async Task AddTokenToRequest(HttpClient client)
    {
       
        var token = await GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }


    }
}
