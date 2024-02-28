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
    [Route("/api/v1/events/{eventId}/subscribers")]
    public class EventSubscribersController : ControllerBase
    {
        private readonly IEventSubscriberService _eventSubscriberService;

        public EventSubscribersController(IEventSubscriberService eventSubscriberService)
        {
            _eventSubscriberService = eventSubscriberService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetEventSubscribers(int eventId)
        {
            return await _eventSubscriberService.GetEventSubscribersAsync(eventId);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> AddEventSubscriber(int eventId, EventSubscribersDTO eventSubscriber)
        {
            return await _eventSubscriberService.AddEventSubscriberAsync(eventId, eventSubscriber);
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<ResponseDTO>> RemoveEventSubscriber(int eventId, int userId)
        {
            return await _eventSubscriberService.RemoveEventSubscriberAsync(eventId, userId);
        }
    }
}