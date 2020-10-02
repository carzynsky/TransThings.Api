﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("loads")]
    public class LoadController : ControllerBase
    {
        private readonly ILoadService loadService;
        public LoadController(ILoadService loadService)
        {
            this.loadService = loadService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Load>>> GetAllLoads()
        {
            var loads = await loadService.GetAllLoads();
            if (loads.Count == 0)
                return NoContent();

            return Ok(loads);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Load>> GetLoad([FromRoute] int id)
        {
            var load = await loadService.GetLoadById(id);
            if (load == null)
                return NoContent();

            return Ok(load);
        }

        [HttpGet("orders/{id}")]
        public async Task<ActionResult<List<Load>>> GetLoadsByOrder([FromRoute] int id)
        {
            var load = await loadService.GetLoadsByOrder(id);
            if (load.Count == 0)
                return NoContent();

            return Ok(load);
        }

        [HttpPost]
        public async Task<ActionResult> AddLoad([FromBody] Load load)
        {
            var addLoadResult = await loadService.AddLoad(load);
            if (!addLoadResult.IsSuccessful)
                return BadRequest(addLoadResult);

            return Ok(addLoadResult);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLoad([FromBody] Load load, [FromRoute] int id)
        {
            var updateLoadResult = await loadService.UpdateLoad(load, id);
            if (!updateLoadResult.IsSuccessful)
                return BadRequest(updateLoadResult);

            return Ok(updateLoadResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveLoad([FromRoute] int id)
        {
            var removeLoadResult = await loadService.RemoveLoad(id);
            if (!removeLoadResult.IsSuccessful)
                return BadRequest(removeLoadResult);

            return Ok(removeLoadResult);
        }
    }
}
