using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
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

        public async Task<GenericResponse> AddEvent(Event _event)
        {
            if (_event == null)
                return new GenericResponse(false, "Event has not been provided.");

            if (string.IsNullOrEmpty(_event.ContactPersonFirstName) || string.IsNullOrEmpty(_event.ContactPersonLastName))
                return new GenericResponse(false, "Contact person first name and last name are mandatory.");

            if (string.IsNullOrEmpty(_event.ContactPersonPhoneNumber))
                return new GenericResponse(false, "Contact person phone number is mandatory.");

            if (string.IsNullOrEmpty(_event.EventName))
                return new GenericResponse(false, "Event name has not been provided.");

            try
            {
                await unitOfWork.EventRepository.AddEventAsync(_event);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }

            return new GenericResponse(true, "New event has been created.");
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

        public async Task<GenericResponse> UpdateEvent(Event _event, int id)
        {
            if (_event == null)
                return new GenericResponse(false, "Event has not been provided.");

            var eventToUpdate = await unitOfWork.EventRepository.GetEventByIdAsync(id);
            if (eventToUpdate == null)
                return new GenericResponse(false, $"Event with id={id} does not exist.");

            if (string.IsNullOrEmpty(_event.ContactPersonFirstName) || string.IsNullOrEmpty(_event.ContactPersonLastName))
                return new GenericResponse(false, "Contact person first name and last name are mandatory.");

            if (string.IsNullOrEmpty(_event.ContactPersonPhoneNumber))
                return new GenericResponse(false, "Contact person phone number is mandatory.");

            if (string.IsNullOrEmpty(_event.EventName))
                return new GenericResponse(false, "Event name has not been provided.");

            eventToUpdate.ContactPersonFirstName = _event.ContactPersonFirstName;
            eventToUpdate.ContactPersonLastName = _event.ContactPersonLastName;
            eventToUpdate.ContactPersonPhoneNumber = _event.ContactPersonPhoneNumber;
            eventToUpdate.EventEndTime = _event.EventEndTime;
            eventToUpdate.EventName = _event.EventName;
            eventToUpdate.EventPlace = _event.EventPlace;
            eventToUpdate.EventStartTime = _event.EventStartTime;
            eventToUpdate.EventStreetAddress = _event.EventStreetAddress;
            eventToUpdate.OtherInformation = _event.OtherInformation;

            try
            {
                await unitOfWork.EventRepository.UpdateEvent(eventToUpdate);
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
