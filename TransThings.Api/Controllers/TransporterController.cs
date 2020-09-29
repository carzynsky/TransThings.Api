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
    [Route("transporters")]
    public class TransporterController : ControllerBase
    {
        private readonly ITransporterService transporterService;
        public TransporterController(ITransporterService transporterService)
        {
            this.transporterService = transporterService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Transporter>>> GetAllTransporters()
        {
            var transporters = await transporterService.GetAllTransporters();
            if (transporters.Count == 0)
                return NoContent();

            return Ok(transporters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transporter>> GetTransport([FromRoute] int id)
        {
            var transporter = await transporterService.GetTransporterById(id);
            if (transporter == null)
                return NoContent();

            return Ok(transporter);
        }

        [HttpPost]
        public async Task<ActionResult> AddTransporter([FromBody] Transporter transporter)
        {
            var addTransporterResult = await transporterService.AddTransporter(transporter);
            if (!addTransporterResult.IsSuccessful)
                return BadRequest(addTransporterResult.Message);

            return Ok(addTransporterResult.Message);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTransporter([FromBody] Transporter transporter, [FromRoute] int id)
        {
            var updateTransporterResult = await transporterService.UpdateTransporter(transporter, id);
            if (!updateTransporterResult.IsSuccessful)
                return BadRequest(updateTransporterResult.Message);

            return Ok(updateTransporterResult.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveTransporter([FromRoute] int id)
        {
            var removeTransporterResult = await transporterService.RemoveTransporter(id);
            if (!removeTransporterResult.IsSuccessful)
                return BadRequest(removeTransporterResult.Message);

            return Ok(removeTransporterResult.Message);
        }
    }
}
