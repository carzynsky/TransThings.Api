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
    [Route("user-roles")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService userRoleService;
        public UserRoleController(IUserRoleService userRoleService)
        {
            this.userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserRole>>> GetAllUserRoles()
        {
            var userRoles = await userRoleService.GetAllUserRoles();
            if (userRoles.Count == 0)
                return NoContent();

            return Ok(userRoles);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<ActionResult> AddUserRole([FromBody] UserRole userRole)
        {
            var addUserRoleResult = await userRoleService.AddUserRole(userRole);
            if (!addUserRoleResult.IsSuccessful)
                return BadRequest(addUserRoleResult);

            return Ok(addUserRoleResult);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserRole([FromBody] UserRole userRole, [FromRoute] int id)
        {
            var updateUserRoleResult = await userRoleService.UpdateUserRole(userRole, id);
            if (!updateUserRoleResult.IsSuccessful)
                return BadRequest(updateUserRoleResult);

            return Ok(updateUserRoleResult);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveUserRole([FromRoute] int id)
        {
            var removeUserRoleResult = await userRoleService.RemoveUserRole(id);
            if (!removeUserRoleResult.IsSuccessful)
                return BadRequest(removeUserRoleResult);

            return Ok(removeUserRoleResult);
        }
    }
}
