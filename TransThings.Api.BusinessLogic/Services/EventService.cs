using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Dto;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork unitOfWork;

        public EventService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<Event>> GetAllEvents()
        {
            var events = await unitOfWork.EventRepository.GetAllEventsAsync();
            return events;
        }

        public async Task<Event> GetEventById(int id)
        {
            var _event = await unitOfWork.EventRepository.GetEventByIdAsync(id);
            return _event;
        }

        public async Task<GenericResponse> AddEvents(EventDto events)
        {
            if (events == null || events?.Events == null)
                return new GenericResponse(false, "Nie podano danych załadunku/rozładunku.");

            try
            {
                await unitOfWork.EventRepository.AddEventsAsync(events.Events);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }

            return new GenericResponse(true, "Załadunki / rozładunki zostały dodane.");
        }

        public async Task<GenericResponse> RemoveEvent(int id)
        {
            var eventToRemove = await unitOfWork.EventRepository.GetEventByIdAsync(id);
            if (eventToRemove == null)
                return new GenericResponse(false, $"Event with id={id} does not exist.");

            try
            {
                await unitOfWork.EventRepository.RemoveEvent(eventToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }

            return new GenericResponse(true, "Event has been removed.");
        }

        public async Task<GenericResponse> UpdateEvents(EventDto events, int forwardingOrderId)
        {
            if (events == null || events?.Events == null)
                return new GenericResponse(false, "Brak danych załadunków/rozładunków.");

            // get old events
            var oldEvents = await unitOfWork.EventRepository.GetEventsByForwardingOrderAsync(forwardingOrderId);

            // create new list of events to add or update
            var eventsToAddOrUpdate = new List<Event>();
            foreach(var _event in events.Events)
            {
                var eventToUpdate = await unitOfWork.EventRepository.GetEventByIdAsync(_event.Id);
                if(eventToUpdate == null)
                {
                    eventsToAddOrUpdate.Add(_event);
                    continue;
                }

                #region Event data update
                eventToUpdate.ContactPersonFirstName = _event.ContactPersonFirstName;
                eventToUpdate.ContactPersonLastName = _event.ContactPersonLastName;
                eventToUpdate.ContactPersonPhoneNumber = _event.ContactPersonPhoneNumber;
                eventToUpdate.EventEndTime = _event.EventEndTime;
                eventToUpdate.EventName = _event.EventName;
                eventToUpdate.EventPlace = _event.EventPlace;
                eventToUpdate.EventStartTime = _event.EventStartTime;
                eventToUpdate.EventStreetAddress = _event.EventStreetAddress;
                eventsToAddOrUpdate.Add(eventToUpdate);
                #endregion
            }

            // catch removed events
            foreach (var oldEvent in oldEvents)
            {
                var eventToDelete = eventsToAddOrUpdate.FirstOrDefault(x => x.Id.Equals(oldEvent.Id));
                if (eventToDelete == null)
                    await unitOfWork.EventRepository.RemoveEvent(oldEvent);
            }

            try
            {
                await unitOfWork.EventRepository.UpdateEvents(eventsToAddOrUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }

            return new GenericResponse(true, "Event has been updated.");
        }

        public async Task<List<Event>> GetEventsByForwardingOrder(int forwardingOrderId)
        {
            var _events = await unitOfWork.EventRepository.GetEventsByForwardingOrderAsync(forwardingOrderId);
            return _events;
        }
    }
}
