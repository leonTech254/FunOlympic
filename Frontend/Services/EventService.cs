using System;
using System.Collections.Generic;
using System.Text.Json;
using Frontend.Models; 
namespace Frontend.Services
{
    public class EventService
    {
        public String _baseURL;
        // public List<EventModel> Events { get; private set; }
        public HttpClient _client;

        public EventService()
        {
        //    _httpContent= new HttpClient();
             _client=new HttpClient(); 
             _baseURL="http://localhost:5215/api/v1/";      
           
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

        public void AddEvent(EventModel newEvent)
        {
            // Events.Add(newEvent);
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
