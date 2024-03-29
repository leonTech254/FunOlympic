using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Backed.Models;
using Backed.Utills;
using DBconnection_namespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backed.Services
{
    public class EventSubscriberService : IEventSubscriberService
    {
        private readonly DBconn _context;
        private Jwt _jwt;

        public EventSubscriberService(DBconn context,Jwt jwt)
        {
            _context = context;
            _jwt=jwt;
        }

        public async Task<ActionResult<ResponseDTO>> GetEventSubscribersAsync(int eventId)
        {
            var eventSubscribers = await _context.EventSubscribers
                .Where(es => es.EventId == eventId)
                .ToListAsync();

            return new ResponseDTO { message = "Success", responseData = eventSubscribers };
        }

        public async Task<ActionResult<ResponseDTO>> GetMyEvents(string jwtToken ){
       string userId= _jwt.GetUserIdFromToken(jwtToken);
        var eventSubscribers = await _context.EventSubscribers.Where(e=>e.UserId==int.Parse(userId)).ToListAsync();
         return new ResponseDTO { message = "Success", responseData = eventSubscribers };


        }


        public async Task<ActionResult<ResponseDTO>> AddEventSubscriberAsync(int eventId, EventSubscribersDTO eventSubscribersDTO,string jwtToken)
        {
            string userId=_jwt.GetUserIdFromToken(jwtToken);
            EventSubscribers eventSubscriber= new EventSubscribers()
            {
                EventId=eventId,
                UserId=int.Parse(userId),
                IsCancelled=false
                
            };
            eventSubscriber.EventId = eventId;
            _context.EventSubscribers.Add(eventSubscriber);
            await _context.SaveChangesAsync();

            return new ResponseDTO { message = "Event subscriber added successfully", responseData = eventSubscriber };
        }

        public async Task<ActionResult<ResponseDTO>> RemoveEventSubscriberAsync(int eventId, int userId)
        {
            var eventSubscriber = await _context.EventSubscribers
                .Where(es => es.EventId == eventId && es.UserId == userId)
                .FirstOrDefaultAsync();

            if (eventSubscriber == null)
            {
                return new ResponseDTO { message = "Event subscriber not found", responseData = null };
            }

            _context.EventSubscribers.Remove(eventSubscriber);
            await _context.SaveChangesAsync();

            return new ResponseDTO { message = "Event subscriber removed successfully", responseData = null };
        }

        public async Task<ActionResult<ResponseDTO>> CancelGet(int eventId, string? jwtToken)
        {
            string userId=_jwt.GetUserIdFromToken(jwtToken);
            List<EventSubscribers> eventSubscribers=await _context.EventSubscribers.ToListAsync();
          EventSubscribers eventSubscriber=eventSubscribers.FirstOrDefault(e=>e.EventId==eventId && e.UserId==int.Parse(userId));
            if(eventSubscriber!=null)
            { 
               eventSubscriber.IsCancelled=!eventSubscriber.IsCancelled;
               _context.SaveChanges();
               return new ResponseDTO() { message="Deleted Successfull",responseData=null};
            }else
            {
                return new ResponseDTO();
            }

            
            
           
        }
    }
}
