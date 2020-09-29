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
    [Route("warehouses")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService warehouseService;
        public WarehouseController(IWarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Warehouse>>> GetAllWarehouses()
        {
            var warehouses = await warehouseService.GetAllWarehouses();
            if (warehouses.Count == 0)
                return NoContent();

            return Ok(warehouses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse([FromRoute] int id)
        {
            var warehouse = await warehouseService.GetWarehouseById(id);
            if (warehouse == null)
                return NoContent();

            return Ok(warehouse);
        }

        [HttpPost]
        public async Task<ActionResult> AddWarehouse([FromBody] Warehouse warehouse)
        {
            var addWarehouseResult = await warehouseService.AddWarehouse(warehouse);
            if (!addWarehouseResult.IsSuccessful)
                return BadRequest(addWarehouseResult.Message);

            return Ok(addWarehouseResult.Message);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateWarehouse([FromBody] Warehouse warehouse, [FromRoute] int id)
        {
            var updateWarehouseResult = await warehouseService.UpdateWarehouse(warehouse, id);
            if (!updateWarehouseResult.IsSuccessful)
                return BadRequest(updateWarehouseResult.Message);

            return Ok(updateWarehouseResult.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveWarehouse([FromRoute] int id)
        {
            var removeWarehouseResult = await warehouseService.RemoveWarehouse(id);
            if (!removeWarehouseResult.IsSuccessful)
                return BadRequest(removeWarehouseResult.Message);

            return Ok(removeWarehouseResult.Message);
        }
    }
}
