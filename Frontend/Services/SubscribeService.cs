using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Frontend.Models;
using Frontend.Utils;

namespace Frontend.Services
{
    public class SubscribeService
    {
        private string _baseUrl;
        private HttpClient _client;
        private readonly TokenServices _tokenServices;
        private readonly PopUpMessages _popUpMessages;



        public SubscribeService(TokenServices tokenServices,PopUpMessages popUpMessages)
        {
            _baseUrl="http://localhost:5215/api/v1/events/eventId/subscribers";
           
            _client=new HttpClient();
            _tokenServices=tokenServices;
            _popUpMessages=popUpMessages;
        }


        public async Task subscibeToEvent(int gameId)
        {
            var data=new {userId= 0,eventId= gameId};
            await _tokenServices.AddTokenToRequest(_client);
             var jsonContent = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
           var response= await _client.PostAsync(_baseUrl.Replace("eventId",$"{gameId}"),jsonContent);
           if(response.IsSuccessStatusCode)
           {
            await _popUpMessages.sweetAlert("Event added to playlist","Subscription","success");
            
           }else{
             await _popUpMessages.sweetAlert("Failed to add to playlist","Subscription","error");
           }


        }

        public async Task CancelEvent(int eventId)
        {
            var response= await _client.GetAsync(_baseUrl.Replace("eventId",$"{eventId}")+"/cancel");
            if(response.IsSuccessStatusCode)
            {
                await _popUpMessages.sweetAlert("Event Removed Successfull","Subscription","success");
            }else
            {
                await _popUpMessages.sweetAlert("Error canceling the Event","Subscription","error"); 
            }

        }


        public async Task<List<Subscription>?> GetAllMyEvents()
        {
           var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };
         await _tokenServices.AddTokenToRequest(_client);
         var response= await _client.GetAsync(_baseUrl.Replace("eventId",$"{1}")+"/myEvents");
         if(response.IsSuccessStatusCode){
            string jsonString=await response.Content.ReadAsStringAsync();
            ResponseDTO responseDTO = JsonSerializer.Deserialize<ResponseDTO>(jsonString,options);
          List<Subscription> subscriptions=  JsonSerializer.Deserialize<List<Subscription>>(responseDTO.responseData.ToString(),options);
          return subscriptions;
         
         }else{
         return null;
         }

        }
    }
}