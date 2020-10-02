using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("forwarding-orders")]
    public class ForwardingOrderController : ControllerBase
    {
        private readonly IForwardingOrderService forwardingOrderService;
        public ForwardingOrderController(IForwardingOrderService forwardingOrderService)
        {
            this.forwardingOrderService = forwardingOrderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ForwardingOrder>>> GetAllForwardingOrders()
        {
            var forwardingOrders = await forwardingOrderService.GetAllForwardingOrders();
            if (forwardingOrders.Count == 0)
                return NoContent();

            return Ok(forwardingOrders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ForwardingOrder>> GetForwardingOrder([FromRoute] int id)
        {
            var forwardingOrder = await forwardingOrderService.GetForwardingOrderById(id);
            if (forwardingOrder == null)
                return NoContent();

            return Ok(forwardingOrder);
        }

        [HttpGet("forwarders/{id}")]
        public async Task<ActionResult<List<ForwardingOrder>>> GetForwardingOrdersByForwarder([FromRoute] int id)
        {
            var forwardingOrders = await forwardingOrderService.GetForwardingOrdersByForwarder(id);
            if (forwardingOrders == null)
                return NoContent();

            return Ok(forwardingOrders);
        }

        [HttpGet("transits/{id}")]
        public async Task<ActionResult<List<ForwardingOrder>>> GetForwardingOrdersByTransit([FromRoute] int id)
        {
            var forwardingOrders = await forwardingOrderService.GetForwardingOrdersByTransit(id);
            if (forwardingOrders == null)
                return NoContent();

            return Ok(forwardingOrders);
        }

        [HttpPost]
        public async Task<ActionResult> AddForwardingOrder([FromBody] ForwardingOrder forwardingOrder)
        {
            var addForwardingOrderResult = await forwardingOrderService.AddForwardingOrder(forwardingOrder);
            if (!addForwardingOrderResult.IsSuccessful)
                return BadRequest(addForwardingOrderResult);

            return Ok(addForwardingOrderResult);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateForwardingOrder([FromBody] ForwardingOrder forwardingOrder, [FromRoute] int id)
        {
            var updateForwardingOrderResult = await forwardingOrderService.UpdateForwardingOrder(forwardingOrder, id);
            if (!updateForwardingOrderResult.IsSuccessful)
                return BadRequest(updateForwardingOrderResult);

            return Ok(updateForwardingOrderResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveForwardingOrder([FromRoute] int id)
        {
            var removeForwardingOrderResult = await forwardingOrderService.RemoveForwardingOrder(id);
            if (!removeForwardingOrderResult.IsSuccessful)
                return BadRequest(removeForwardingOrderResult);

            return Ok(removeForwardingOrderResult);
        }
    }
}
