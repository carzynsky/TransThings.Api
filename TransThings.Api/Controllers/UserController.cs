using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Constants;
using TransThings.Api.DataAccess.Dto;

namespace TransThings.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var users = await userService.GetAllUsers();
            if (users.Count == 0)
                return NoContent();

            return Ok(users);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("stats")]
        public async Task<ActionResult<UserStatsDto>> GetUsersStats()
        {
            var userStats = await userService.GetUsersStats();
            return Ok(userStats);
        }

        [HttpGet("role/{userRoleId}")]
        public async Task<ActionResult<List<UserDto>>> GetUsersByRole([FromRoute] int userRoleId)
        {
            var users = await userService.GetAllUsersByRole(userRoleId);
            if (users.Count == 0)
                return NoContent();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById([FromRoute] int id)
        {
            var user = await userService.GetUserById(id);
            if (user == null)
                return NoContent();

            return Ok(user);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<ActionResult<AddUserResponse>> AddUser([FromBody] UserDto userDto)
        {
            var addUserResult = await userService.AddUser(userDto);
            if (!addUserResult.IsSuccessful)
                return BadRequest(addUserResult);

            return Ok(addUserResult);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser([FromBody] UserDto userDto, [FromRoute] int id)
        {
            var updateUserResult = await userService.UpdateUser(userDto, id);
            if (!updateUserResult.IsSuccessful)
                return BadRequest(updateUserResult);

            return Ok(updateUserResult);
        }

        [HttpPut("{id}/change-password")]
        public async Task<ActionResult> UpdateUserPassword([FromBody] ChangePasswordData passwordData, [FromRoute] int id)
        {
            var updateUserResult = await userService.ChangePassword(passwordData, id);
            if (!updateUserResult.IsSuccessful)
                return BadRequest(updateUserResult);

            return Ok(updateUserResult);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveUser([FromRoute] int id)
        {
            var removeUserResult = await userService.RemoveUser(id);
            if (!removeUserResult.IsSuccessful)
                return BadRequest(removeUserResult);

            return Ok(removeUserResult);
        }
    }
}
