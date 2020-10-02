using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await orderService.GetAllOrders();
            if (orders.Count == 0)
                return NoContent();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder([FromRoute] int id)
        {
            var order = await orderService.GetOrderById(id);
            if (order == null)
                return NoContent();

            return Ok(order);
        }

        [HttpGet("clients/{id}")]
        public async Task<ActionResult<List<Order>>> GetOrdersByClient([FromRoute] int id)
        {
            var order = await orderService.GetOrdersByClientId(id);
            if (order.Count == 0)
                return NoContent();

            return Ok(order);
        }

        [HttpGet("status/{id}")]
        public async Task<ActionResult<List<Order>>> GetOrdersByStatus([FromRoute] int id)
        {
            var order = await orderService.GetOrdersByStatus(id);
            if (order.Count == 0)
                return NoContent();

            return Ok(order);
        }

        [HttpGet("orderers/{id}")]
        public async Task<ActionResult<List<Order>>> GetOrdersByOrderer([FromRoute] int id)
        {
            var order = await orderService.GetOrdersByOrderer(id);
            if (order.Count == 0)
                return NoContent();

            return Ok(order);
        }

        [HttpGet("consultants/{id}")]
        public async Task<ActionResult<List<Order>>> GetOrdersByConsultant([FromRoute] int id)
        {
            var order = await orderService.GetOrdersByConsultant(id);
            if (order.Count == 0)
                return NoContent();

            return Ok(order);
        }

        [HttpGet("status/{id}")]
        public async Task<ActionResult<List<Order>>> GetOrdersByForwardingOrder([FromRoute] int id)
        {
            var order = await orderService.GetOrdersByForwardingOrder(id);
            if (order.Count == 0)
                return NoContent();

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder([FromBody] Order order)
        {
            var addOrderResult = await orderService.AddOrder(order);
            if (!addOrderResult.IsSuccessful)
                return BadRequest(addOrderResult);

            return Ok(addOrderResult);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder([FromBody] Order order, [FromRoute] int id)
        {
            var updateOrderResult = await orderService.UpdateOrder(order, id);
            if (!updateOrderResult.IsSuccessful)
                return BadRequest(updateOrderResult);

            return Ok(updateOrderResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveOrder([FromRoute] int id)
        {
            var removeOrderResult = await orderService.RemoveOrder(id);
            if (!removeOrderResult.IsSuccessful)
                return BadRequest(removeOrderResult);

            return Ok(removeOrderResult);
        }
    }
}
