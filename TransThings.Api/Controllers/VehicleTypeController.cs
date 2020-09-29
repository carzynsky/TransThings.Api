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
    [Route("vehicle-types")]
    public class VehicleTypeController : ControllerBase
    {
        private readonly IVehicleTypeService vehicleTypeService;
        public VehicleTypeController(IVehicleTypeService vehicleTypeService)
        {
            this.vehicleTypeService = vehicleTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VehicleType>>> GetAllVehicleTypes()
        {
            var vehicleTypes = await vehicleTypeService.GetAllVehicleTypes();
            if (vehicleTypes.Count == 0)
                return NoContent();

            return Ok(vehicleTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleType>> GetVehicleType([FromRoute] int id)
        {
            var vehicleType = await vehicleTypeService.GetVehicleTypeById(id);
            if (vehicleType == null)
                return NoContent();

            return Ok(vehicleType);
        }

        [HttpPost]
        public async Task<ActionResult> AddVehicleType([FromBody] VehicleType vehicleType)
        {
            var addVehicleTypeResult = await vehicleTypeService.AddVehicleType(vehicleType);
            if (!addVehicleTypeResult.IsSuccessful)
                return BadRequest(addVehicleTypeResult.Message);

            return Ok(addVehicleTypeResult.Message);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateVehicleType([FromBody] VehicleType vehicleType, [FromRoute] int id)
        {
            var updateVehicleTypeResult = await vehicleTypeService.UpdateVehicleType(vehicleType, id);
            if (!updateVehicleTypeResult.IsSuccessful)
                return BadRequest(updateVehicleTypeResult.Message);

            return Ok(updateVehicleTypeResult.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveVehicleType([FromRoute] int id)
        {
            var removeVehicleTypeResult = await vehicleTypeService.RemoveVehicleType(id);
            if (!removeVehicleTypeResult.IsSuccessful)
                return BadRequest(removeVehicleTypeResult.Message);

            return Ok(removeVehicleTypeResult.Message);
        }
    }
}
