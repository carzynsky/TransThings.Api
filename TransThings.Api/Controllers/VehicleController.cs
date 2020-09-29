using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("vehicles")]
    public class VehicleController : ControllerBase
    {
        private IVehicleService vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Vehicle>>> GetAllVehicles()
        {
            var vehicles = await vehicleService.GetAllVehicles();
            if (vehicles.Count == 0)
                return NoContent();

            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle([FromRoute] int id)
        {
            var vehicle = await vehicleService.GetVehicleById(id);
            if (vehicle == null)
                return NoContent();

            return Ok(vehicle);
        }

        [HttpGet("transporters/{id}")]
        public async Task<ActionResult<List<Vehicle>>> GetAllVehicles([FromRoute] int id)
        {
            var vehicles = await vehicleService.GetVehiclesByTransporter(id);
            if (vehicles.Count == 0)
                return NoContent();

            return Ok(vehicles);
        }

        [HttpPost]
        public async Task<ActionResult> AddVehicles([FromBody] Vehicle vehicle)
        {
            var addVehicleResult = await vehicleService.AddVehicle(vehicle);
            if (!addVehicleResult.IsSuccessful)
                return BadRequest(addVehicleResult.Message);

            return Ok(addVehicleResult.Message);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateVehicle([FromBody] Vehicle vehicle, [FromRoute] int id)
        {
            var updateVehicleResult = await vehicleService.UpdateVehicle(vehicle, id);
            if (!updateVehicleResult.IsSuccessful)
                return BadRequest(updateVehicleResult.Message);

            return Ok(updateVehicleResult.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveVehicle([FromRoute] int id)
        {
            // ToDo
            var removeVehicleResult = await vehicleService.RemoveVehicle(id);
            if (!removeVehicleResult.IsSuccessful)
                return BadRequest(removeVehicleResult.Message);

            return Ok(removeVehicleResult.Message);
        }
    }
}
