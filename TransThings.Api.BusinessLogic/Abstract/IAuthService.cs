using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Dto;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface IAuthService
    {
        Task<AuthenticateResponse> IsSignInSuccessful(AuthUserData authUser);
    }
}