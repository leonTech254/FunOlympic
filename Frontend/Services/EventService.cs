using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using Frontend.Models;
using Frontend.Utils;
namespace Frontend.Services
{
    public class EventService
    {
        public String _baseURL;
        // public List<EventModel> Events { get; private set; }
        public HttpClient _client;
        public readonly PopUpMessages _popUpMessages;

        public EventService(PopUpMessages popUpMessages)
        {
        //    _httpContent= new HttpClient();
             _client=new HttpClient(); 
             _baseURL="http://localhost:5215/api/v1/";  
             _popUpMessages=popUpMessages;    
           
        }

        public async Task<List<EventModel>> getAllEvents()
        {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
           var response= await _client.GetAsync(_baseURL+"events");
           if(response.IsSuccessStatusCode)
           {
            var jsonString=await response.Content.ReadAsStringAsync();
            var responseDTO=JsonSerializer.Deserialize<ResponseDTO>(jsonString,options);
            List<EventModel> events=JsonSerializer.Deserialize<List<EventModel>>(responseDTO.responseData.ToString(),options);
            Console.WriteLine(responseDTO.responseData.ToString() +"i am here");
            return events;
           }else
           {
            return null;
           }

                
        }


        public async Task<EventModel> getEventDetails(int eventId)
        {
            var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var response= await _client.GetAsync(_baseURL+"events/"+eventId);
                 if(response.IsSuccessStatusCode)
           {
            var jsonString=await response.Content.ReadAsStringAsync();
            var responseDTO=JsonSerializer.Deserialize<ResponseDTO>(jsonString,options);
            EventModel events=JsonSerializer.Deserialize<EventModel>(responseDTO.responseData.ToString(),options);
            Console.WriteLine(responseDTO.responseData.ToString() +"i am here");
            return events;
           }else
           {
            return null;
           }

        }



        

        public async Task AddEvent(EventModel eventModel)
        {
              try
        {
           

            // Submit the form data to the API endpoint
            
            var response = await _client.PostAsJsonAsync("http://localhost:5215/api/v1/events", eventModel);

            if (response.IsSuccessStatusCode)
            {
                eventModel = new EventModel();
                await _popUpMessages.sweetAlert("Event Added Successfully","Events","success");
            }
            else
            {

                await _popUpMessages.sweetAlert("Failed to create event. Please try again later.","Events","error");
                
            }
        }
        catch (Exception ex)
        {
             await _popUpMessages.sweetAlert("An error occurred while processing your request. Please try again later.","Events","error");
        }
        
        }

        // public void UpdateEvent(EventModel updatedEvent)
        // {
        //     var existingEvent = Events.Find(e => e.EventDate == updatedEvent.EventDate);
        //     if (existingEvent != null)
        //     {
        //         existingEvent.EventName = updatedEvent.EventName;
        //         existingEvent.EventImageUrl = updatedEvent.EventImageUrl;
        //         existingEvent.EventDescription = updatedEvent.EventDescription;
        //     }
        // }

        // public void DeleteEvent(DateTime eventDate)
        // {
        //     Events.RemoveAll(e => e.EventDate == eventDate);
        // }
    }

    // public class EventModel
    // {
    //     public DateTime EventDate { get; set; }
    //     public string EventName { get; set; }
    //     public string EventImageUrl { get; set; }
    //     public string EventDescription { get; set; }
    // }
}
