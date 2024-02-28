using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backed.Models;
using DBconnection_namespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backed.Services
{
    public class EventService : IEventService
    {
        private readonly DBconn _context;

        public EventService(DBconn context)
        {
            _context = context;
        }

        public async Task<ActionResult<ResponseDTO>> GetEventsAsync()
        {
            var events = await _context.Events.ToListAsync();
            return new ResponseDTO { message = "Success", responseData = events };
        }

        public async Task<ActionResult<ResponseDTO>> GetEventByIdAsync(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return new ResponseDTO { message = "Event not found", responseData = null };
            }
            return new ResponseDTO { message = "Success", responseData = @event };
        }

        public async Task<ActionResult<ResponseDTO>> CreateEventAsync(EventDTO eventDTO)
        {
            Event @event= new Event()
            {
                EventDate=eventDTO.EventDate,
                EventDescription=eventDTO.EventDescription,
                EventImageUrl=eventDTO.EventImageUrl,
                EventName=eventDTO.EventName,
            };
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();
            return new ResponseDTO { message = "Event created successfully", responseData = @event };
        }

        public async Task UpdateEventAsync(int id, Event @event)
        {
            if (id != @event.Id)
            {
                throw new ArgumentException("Invalid event ID");
            }

            _context.Entry(@event).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                throw new ArgumentException("Event not found");
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
        }
    }
}
