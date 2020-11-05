using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransThings.Api.BusinessLogic.Constants;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.DataAccess.Dto;

namespace TransThings.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService authenticationService;
        private readonly ILoginHistoryService loginHistoryService;
        private readonly IUserService userService;

        public AuthenticationController(IAuthService authenticationService, ILoginHistoryService loginHistoryService, IUserService userService)
        {
            this.authenticationService = authenticationService;
            this.loginHistoryService = loginHistoryService;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetMessageTest()
        {
            return Ok("TransThings Api");
        }

        [AllowAnonymous]
        [HttpPost("hash")]
        public IActionResult GetMessageTest(AuthUserData authUserData)
        {
            HashPassword hashed = new HashPassword(authUserData.Password);
            return Ok(hashed.HashedPassword);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AuthenticateResponse>> Login([FromBody] AuthUserData authUser)
        {
            var authResponse = await authenticationService.IsSignInSuccessful(authUser);

            switch (authResponse.Message)
            {
                case AuthResponseMessage.LoginAndPasswordNotProvided:
                case AuthResponseMessage.LoginNotProvided:
                case AuthResponseMessage.PasswordNotProvided: // note that not providing password does not count as login attempt
                default:
                    return BadRequest(authResponse);

                case AuthResponseMessage.UserWithThisLoginNotExists:
                    return Conflict(authResponse.Message);

                case AuthResponseMessage.WrongPassword:
                    {
                        int userId = await userService.GetUserIdByLogin(authUser.Login);
                        bool shouldBeBlocked = await loginHistoryService.CheckIfLogInShouldBeBlocked(userId);
                        if (shouldBeBlocked)
                            return Conflict("Too many bad attempts. Try later.");

                        await loginHistoryService.AddLoginHistory(userId, false);
                        return Conflict(authResponse.Message);
                    }

                case AuthResponseMessage.LoggedSucessfuly:
                    {
                        int userId = await userService.GetUserIdByLogin(authUser.Login);
                        bool shouldBeBlocked = await loginHistoryService.CheckIfLogInShouldBeBlocked(userId);
                        if (shouldBeBlocked)
                            return Conflict("Too many bad attempts. Try later.");

                        await loginHistoryService.AddLoginHistory(userId, true);
                        return Ok(authResponse);
                    }
            }
        }
    }
}
