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
    [Route("clients")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;
        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetAllClients()
        {
            var clients = await clientService.GetAllClients();

            if (clients.Count == 0)
                return NoContent();

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient([FromRoute] int id)
        {
            var client = await clientService.GetClientById(id);
            if (client == null)
                return NoContent();

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult> AddClient([FromBody] Client client)
        {
            var addClientResult = await clientService.AddClient(client);

            if (!addClientResult.IsSuccessful)
                return BadRequest(addClientResult);

            return Ok(addClientResult);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditClient([FromBody] Client client, [FromRoute] int id)
        {
            var updateClientResult = await clientService.UpdateClient(client, id);

            if (!updateClientResult.IsSuccessful)
                return BadRequest(updateClientResult);

            return Ok(updateClientResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient([FromRoute] int id)
        {
            var removeClientResult = await clientService.RemoveClient(id);

            if (!removeClientResult.IsSuccessful)
                return BadRequest(removeClientResult);

            return Ok(removeClientResult);
        }
    }
}
