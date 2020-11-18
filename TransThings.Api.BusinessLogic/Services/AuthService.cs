using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Constants;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;
using TransThings.Api.DataAccess.Dto;

namespace TransThings.Api.BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Method indicates whether sign in process was successful
        /// </summary>
        /// <param name="authUser"></param>
        /// <returns></returns>
        public async Task<AuthenticateResponse> IsSignInSuccessful(AuthUserData authUser)
        {
            bool isLoginNullOrEmpty = string.IsNullOrEmpty(authUser.Login);
            bool isPasswordNullOrEmpty = string.IsNullOrEmpty(authUser.Password);

            if (isLoginNullOrEmpty && isPasswordNullOrEmpty)
                return new AuthenticateResponse(AuthResponseMessage.LoginAndPasswordNotProvided, null, null, null, null, null);

            else if (isLoginNullOrEmpty)
                return new AuthenticateResponse(AuthResponseMessage.LoginNotProvided, null, null, null, null, null);

            else if (isPasswordNullOrEmpty)
                return new AuthenticateResponse(AuthResponseMessage.PasswordNotProvided, null, null, null, null, null);

            var userWithAuthLogin = await unitOfWork.UserRepository.GetUserByLoginAsync(authUser.Login);

            if (userWithAuthLogin == null)
                return new AuthenticateResponse(AuthResponseMessage.UserWithThisLoginNotExists, null,null, null, null, null);

            HashPassword hash = new HashPassword(authUser.Password);
            if (userWithAuthLogin.Password.Equals(hash.HashedPassword))
            {
                var userRole = await unitOfWork.UserRoleRepository.GetUserRole(userWithAuthLogin);
                var authResponseData = Authenticate(userWithAuthLogin, userRole);

                if (authResponseData == null)
                    return new AuthenticateResponse(AuthResponseMessage.ErrorWhileGeneratingToken, null, null, null, null, null);

                return authResponseData;
            }
            return new AuthenticateResponse(AuthResponseMessage.WrongPassword, null, null, null, null, null);
        }

        /// <summary>
        /// Creating jwt token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        private AuthenticateResponse Authenticate(User user, UserRole userRole)
        {
            // generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            try
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, userRole.RoleName)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                string initials = null;
                if (user.FirstName != null && user.LastName != null)
                    initials = user.FirstName[0].ToString().ToUpper() + user.LastName[0].ToString().ToUpper()[0];

                return new AuthenticateResponse(AuthResponseMessage.LoggedSucessfuly, user.Id, user.UserRole.RoleName, user.Login, initials, tokenHandler.WriteToken(token));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
