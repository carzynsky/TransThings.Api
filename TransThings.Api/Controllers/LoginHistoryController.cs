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
    [Route("login-histories")]
    public class LoginHistoryController : ControllerBase
    {
        private readonly ILoginHistoryService loginHistoryService;
        public LoginHistoryController(ILoginHistoryService loginHistoryService)
        {
            this.loginHistoryService = loginHistoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LoginHistory>>> GetAllLoginHistories()
        {
            var loginHistories = await loginHistoryService.GetAllLoginHistories();
            if (loginHistories.Count == 0)
                return NoContent();

            return Ok(loginHistories);
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult<List<LoginHistory>>> GetAllLoginHistories([FromRoute] int id)
        {
            var loginHistories = await loginHistoryService.GetLoginHistoryByUser(id);
            if (loginHistories.Count == 0)
                return NoContent();

            return Ok(loginHistories);
        }
    }
}
