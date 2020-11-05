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
    [Route("config")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService configurationService;
        public ConfigurationController(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Configuration>>> GetAllConfiguration()
        {
            var configurations = await configurationService.GetAllConfigurations();
            if (configurations.Count == 0)
                return NoContent();

            return Ok(configurations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Configuration>> GetConfiguration([FromRoute] int id)
        {
            // ToDo
            var configuration = await configurationService.GetConfigurationById(id);
            if (configuration == null)
                return NoContent();

            return Ok();
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<ActionResult> AddConfiguration([FromBody] Configuration configuration)
        {
            var configurationAddResult = await configurationService.AddConfiguration(configuration);
            if (!configurationAddResult.IsSuccessful)
                return BadRequest(configurationAddResult);

            return Ok(configurationAddResult);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateConfiguration([FromBody] Configuration configuration, [FromRoute] int id)
        {
            var configurationUpdateResult = await configurationService.UpdateConfiguration(configuration, id);
            if (!configurationUpdateResult.IsSuccessful)
                return BadRequest(configurationUpdateResult);

            return Ok(configurationUpdateResult);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveConfiguration([FromRoute] int id)
        {
            var configurationRemoveResult = await configurationService.RemoveConfiguration(id);
            if (!configurationRemoveResult.IsSuccessful)
                return BadRequest(configurationRemoveResult);

            return Ok(configurationRemoveResult);
        }
    }
}
