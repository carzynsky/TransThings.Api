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
    [Route("drivers")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService driverService;
        public DriverController(IDriverService driverService)
        {
            this.driverService = driverService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Driver>>> GetAllDrivers()
        {
            var drivers = await driverService.GetAllDrivers();
            if (drivers.Count == 0)
                return NoContent();

            return Ok(drivers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> GetDriver([FromRoute] int id)
        {
            var driver = await driverService.GetDriverById(id);
            if (driver == null)
                return NoContent();

            return Ok(driver);
        }

        [HttpGet("transporters/{id}")]
        public async Task<ActionResult<List<Driver>>> GetDriversByTransporter([FromRoute] int id)
        {
            var drivers = await driverService.GetDriversByTransporter(id);
            if (drivers == null)
                return NoContent();

            return Ok(drivers);
        }

        [HttpPost]
        public async Task<ActionResult> AddDriver([FromBody] Driver driver)
        {
            var addDriverResult = await driverService.AddDriver(driver);
            if (!addDriverResult.IsSuccessful)
                return BadRequest(addDriverResult.Message);

            return Ok(addDriverResult.Message);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDriver([FromBody] Driver driver, [FromRoute] int id)
        {
            var updateDriverResult = await driverService.UpdateDriver(driver, id);
            if (!updateDriverResult.IsSuccessful)
                return BadRequest(updateDriverResult.Message);

            return Ok(updateDriverResult.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveDriver([FromRoute] int id)
        {
            var removeDriverResult = await driverService.RemoveDriver(id);
            if (!removeDriverResult.IsSuccessful)
                return BadRequest(removeDriverResult.Message);

            return Ok(removeDriverResult.Message);
        }
    }
}
