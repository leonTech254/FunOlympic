using System.Threading.Tasks;
using Backed.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backed.Services
{
    public interface IEventSubscriberService
    {
        Task<ActionResult<ResponseDTO>> GetEventSubscribersAsync(int eventId);
        Task<ActionResult<ResponseDTO>> AddEventSubscriberAsync(int eventId, EventSubscribersDTO eventSubscriber);
        Task<ActionResult<ResponseDTO>> RemoveEventSubscriberAsync(int eventId, int userId);
    }

   
}
