using System.Collections.Generic;
using System.Threading.Tasks;
using Backed.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backed.Services
{
    public interface IEventService
    {
        Task<ActionResult<ResponseDTO>> GetEventsAsync();
        Task<ActionResult<ResponseDTO>> GetEventByIdAsync(int id);
        Task<ActionResult<ResponseDTO>> CreateEventAsync(EventDTO @event);
        Task UpdateEventAsync(int id, Event @event);
        Task DeleteEventAsync(int id);
    }
}
