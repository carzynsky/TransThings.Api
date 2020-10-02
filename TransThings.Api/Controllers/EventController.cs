using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent([FromRoute] int id)
        {
            var _event = await eventService.GetEventById(id);
            if (_event == null)
                return NoContent();

            return Ok(_event);
        }

        [HttpPost]
        public async Task<ActionResult> AddEvent([FromBody] Event _event)
        {
            var addEventResult = await eventService.AddEvent(_event);
            if (!addEventResult.IsSuccessful)
                return BadRequest(addEventResult);

            return Ok(addEventResult);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEvent([FromBody] Event _event, [FromRoute] int id)
        {
            var updateEventResult = await eventService.UpdateEvent(_event, id);
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
