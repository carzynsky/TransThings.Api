using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.DataAccess.Dto;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("transits")]
    public class TransitController : ControllerBase
    {
        private readonly ITransitService transitService;
        public TransitController(ITransitService transitService)
        {
            this.transitService = transitService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Transit>>> GetAllTransits()
        {
            var transits = await transitService.GetAllTransits();
            if (transits.Count == 0)
                return NoContent();

            return Ok(transits);
        }

        [HttpGet("forwarding-orders/{id}")]
        public async Task<ActionResult<Transit>> GetTransitByForwardingOrder([FromRoute] int id)
        {
            var transit = await transitService.GetTransitsByForwardingOrder(id);
            if (transit == null)
                return NoContent();

            return Ok(transit);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transit>> GetTransit([FromRoute] int id)
        {
            var transit = await transitService.GetTransitById(id);
            if (transit == null)
                return NoContent();

            return Ok(transit);
        }

        [HttpPost]
        public async Task<ActionResult> AddTransit([FromBody] Transit transit)
        {
            var addTransitResult = await transitService.AddTransit(transit);
            if (!addTransitResult.IsSuccessful)
                return BadRequest(addTransitResult);

            return Ok(addTransitResult);
        }

        [HttpPut("{forwardingOrderId}")]
        public async Task<ActionResult> UpdateTransits([FromBody] TransitDto transits, [FromRoute] int forwardingOrderId)
        {
            var updateTransitResult = await transitService.UpdateTransits(transits, forwardingOrderId);
            if (!updateTransitResult.IsSuccessful)
                return BadRequest(updateTransitResult);

            return Ok(updateTransitResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveTransit([FromRoute] int id)
        {
            var removeTransitResult = await transitService.RemoveTransit(id);
            if (!removeTransitResult.IsSuccessful)
                return BadRequest(removeTransitResult);

            return Ok(removeTransitResult);
        }
    }
}
