using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface IUserRoleService
    {
        Task<List<UserRole>> GetAllUserRoles();
        Task<UserRole> GetUserRoleById(int id);
        Task<GenericResponse> AddUserRole(UserRole userRole);
        Task<GenericResponse> UpdateUserRole(UserRole userRole, int id);
        Task<GenericResponse> RemoveUserRole(int id);
    }
}
