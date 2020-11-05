using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface IEventService
    {
        Task<List<Event>> GetAllEvents();
        Task<Event> GetEventById(int id);
        Task<List<Event>> GetEventsByForwardingOrder(int forwardingOrderId);
        Task<GenericResponse> AddEvent(Event _event);
        Task<GenericResponse> UpdateEvent(Event _event, int id);
        Task<GenericResponse> RemoveEvent(int id);
    }
}
