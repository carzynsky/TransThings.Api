using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.DataAccess.Constants;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("transit-forwarding-order")]
    public class TransitForwardingOrderController : ControllerBase
    {
        private readonly ITransitForwardingOrderService transitForwardingOrderService;
        public TransitForwardingOrderController(ITransitForwardingOrderService transitForwardingOrderService)
        {
            this.transitForwardingOrderService = transitForwardingOrderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransitForwardingOrder>>> GetAllTransitForwardingOrders()
        {
            var userRoles = await transitForwardingOrderService.GetTransitsForwardingOrders();
            if (userRoles.Count == 0)
                return NoContent();

            return Ok(userRoles);
        }

        [Authorize(Roles = Role.Forwarder)]
        [HttpPost]
        public async Task<ActionResult> AddTransitForwardingOrder([FromBody] TransitForwardingOrder transitForwardingOrder)
        {
            var addTransitForwardingOrderResult = await transitForwardingOrderService.AddTransitForwardingOrder(transitForwardingOrder);
            if (!addTransitForwardingOrderResult.IsSuccessful)
                return BadRequest(addTransitForwardingOrderResult);

            return Ok(addTransitForwardingOrderResult);
        }

        [Authorize(Roles = Role.Forwarder)]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTransitForwardingOrder([FromBody] TransitForwardingOrder transitForwardingOrder, [FromRoute] int id)
        {
            var updateTransitForwardingOrderResult = await transitForwardingOrderService.UpdateTransitForwardingOrder(transitForwardingOrder, id);
            if (!updateTransitForwardingOrderResult.IsSuccessful)
                return BadRequest(updateTransitForwardingOrderResult);

            return Ok(updateTransitForwardingOrderResult);
        }

        [Authorize(Roles = Role.Forwarder)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveTransitForwardingOrder([FromRoute] int id)
        {
            var removeTransitForwardingOrderResult = await transitForwardingOrderService.RemoveTransitForwardingOrder(id);
            if (!removeTransitForwardingOrderResult.IsSuccessful)
                return BadRequest(removeTransitForwardingOrderResult);

            return Ok(removeTransitForwardingOrderResult);
        }
    }
}
