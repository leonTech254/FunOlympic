using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Backed.Models;
using DBconnection_namespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backed.Services
{
    public class EventSubscriberService : IEventSubscriberService
    {
        private readonly DBconn _context;

        public EventSubscriberService(DBconn context)
        {
            _context = context;
        }

        public async Task<ActionResult<ResponseDTO>> GetEventSubscribersAsync(int eventId)
        {
            var eventSubscribers = await _context.EventSubscribers
                .Where(es => es.EventId == eventId)
                .ToListAsync();

            return new ResponseDTO { message = "Success", responseData = eventSubscribers };
        }

        public async Task<ActionResult<ResponseDTO>> AddEventSubscriberAsync(int eventId, EventSubscribersDTO eventSubscribersDTO)
        {
            EventSubscribers eventSubscriber= new EventSubscribers()
            {
                EventId=eventId,
                UserId=eventSubscribersDTO.UserId
                
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

        
    }
}
