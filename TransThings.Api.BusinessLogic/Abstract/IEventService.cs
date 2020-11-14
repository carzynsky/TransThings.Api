using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Dto;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface IEventService
    {
        Task<List<Event>> GetAllEvents();
        Task<Event> GetEventById(int id);
        Task<List<Event>> GetEventsByForwardingOrder(int forwardingOrderId);
        Task<GenericResponse> AddEvents(EventDto events);
        Task<GenericResponse> UpdateEvents(EventDto events, int forwardingOrderId);
        Task<GenericResponse> RemoveEvent(int id);
    }
}
