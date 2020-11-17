using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Dto;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();
        Task<UserStatsDto> GetUsersStats();
        Task<UserDto> GetUserById(int id);
        Task<List<UserDto>> GetAllUsersByRole(int userRoleId);
        Task<int> GetUserIdByLogin(string login);
        Task<AddUserResponse> AddUser(UserDto userDto);
        Task<GenericResponse> UpdateUser(UserDto userDto, int id);
        Task<GenericResponse> RemoveUser(int id);
        Task<GenericResponse> ChangePassword(ChangePasswordData changePasswordData, int id);
    }
}
