using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.DataAccess.Dto;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("events")]
    public class EventController : ControllerBase
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAllEvents()
        {
            var events = await eventService.GetAllEvents();
            if (events.Count == 0)
                return NoContent();

            return Ok(events);
        }

        [AllowAnonymous]
        [HttpGet("stats")]
        public async Task<ActionResult<List<Event>>> GetEventsStats()
        {
            var eventsStats = await eventService.GetEventStats();
            if (eventsStats == null)
                return NoContent();

            return Ok(eventsStats);
        }

        [HttpGet("forwarding-orders/{id}")]
        public async Task<ActionResult<List<Event>>> GetEventsByForwardingOrder([FromRoute] int id)
        {
            var events = await eventService.GetEventsByForwardingOrder(id);
            if (events.Count == 0)
                return NoContent();

            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent([FromRoute] int id)
        {
            var _event = await eventService.GetEventById(id);
            if (_event == null)
                return NoContent();

            return Ok(_event);
        }

        [HttpPost]
        public async Task<ActionResult> AddEvents([FromBody] EventDto events)
        {
            var addEventResult = await eventService.AddEvents(events);
            if (!addEventResult.IsSuccessful)
                return BadRequest(addEventResult);

            return Ok(addEventResult);
        }

        [HttpPut("{forwardingOrderId}")]
        public async Task<ActionResult> UpdateEvents([FromBody] EventDto events, [FromRoute] int forwardingOrderId)
        {
            var updateEventResult = await eventService.UpdateEvents(events, forwardingOrderId);
            if (!updateEventResult.IsSuccessful)
                return BadRequest(updateEventResult);

            return Ok(updateEventResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveEvent([FromRoute] int id)
        {
            var removeEventResult = await eventService.RemoveEvent(id);
            if (!removeEventResult.IsSuccessful)
                return BadRequest(removeEventResult);

            return Ok(removeEventResult);
        }
    }
}
