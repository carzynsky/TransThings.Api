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
    [Route("order-statuses")]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusService orderStatusService;
        public OrderStatusController(IOrderStatusService orderStatusService)
        {
            this.orderStatusService = orderStatusService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderStatus>>> GetAllOrderStatuses()
        {
            var orderStatuses = await orderStatusService.GetAllOrderStatuses();
            if (orderStatuses.Count == 0)
                return NoContent();

            return Ok(orderStatuses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ForwardingOrder>> GetOrderStatus([FromRoute] int id)
        {
            var orderStatus = await orderStatusService.GetOrderStatusById(id);
            if (orderStatus == null)
                return NoContent();

            return Ok(orderStatus);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrderStatus([FromBody] OrderStatus orderStatus)
        {
            var addOrderStatusResult = await orderStatusService.AddOrderStatus(orderStatus);
            if (!addOrderStatusResult.IsSuccessful)
                return BadRequest(addOrderStatusResult.Message);

            return Ok(addOrderStatusResult.Message);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrderStatus([FromBody] OrderStatus orderStatus, [FromRoute] int id)
        {
            var updateOrderStatusResult = await orderStatusService.UpdateOrderStatus(orderStatus, id);
            if (!updateOrderStatusResult.IsSuccessful)
                return BadRequest(updateOrderStatusResult.Message);

            return Ok(updateOrderStatusResult.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveOrderStatus([FromRoute] int id)
        {
            var removeOrderStatusResult = await orderStatusService.RemoveOrderStatus(id);
            if (!removeOrderStatusResult.IsSuccessful)
                return BadRequest(removeOrderStatusResult.Message);

            return Ok(removeOrderStatusResult.Message);
        }
    }
}
