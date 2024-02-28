using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backed.Models;
using Backed.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backed.Controllers
{
   [ApiController]
    [Route("/api/v1/events")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetEvents()
        {
            return await _eventService.GetEventsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetEvent(int id)
        {
            return await _eventService.GetEventByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateEvent(EventDTO @event)
        {
            return await _eventService.CreateEventAsync(@event);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event @event)
        {
            try
            {
                await _eventService.UpdateEventAsync(id, @event);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                await _eventService.DeleteEventAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}