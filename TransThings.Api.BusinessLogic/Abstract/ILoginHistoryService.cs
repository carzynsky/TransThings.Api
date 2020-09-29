using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface ILoginHistoryService
    {
        Task<List<LoginHistory>> GetAllLoginHistories();
        Task<List<LoginHistory>> GetLoginHistoryByUser(int userId);
        Task<GenericResponse> AddLoginHistory(int userId, bool wasSuccessful);
        Task<bool> CheckIfLogInShouldBeBlocked(int userId);
    }
}
