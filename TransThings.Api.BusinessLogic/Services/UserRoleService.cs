using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;
using Microsoft.EntityFrameworkCore;

namespace TransThings.Api.BusinessLogic.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork unitOfWork;
        public UserRoleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<UserRole>> GetAllUserRoles()
        {
            var userRoles = await unitOfWork.UserRoleRepository.GetAllUserRolesAsync();
            return userRoles;
        }

        public async Task<UserRole> GetUserRoleById(int id)
        {
            var userRole = await unitOfWork.UserRoleRepository.GetUserRoleByIdAsync(id);
            return userRole;
        }

        public async Task<GenericResponse> AddUserRole(UserRole userRole)
        {
            if (userRole == null)
                return new GenericResponse(false, "User role has not been provided.");

            if (string.IsNullOrEmpty(userRole.RoleName))
                return new GenericResponse(false, "User role name has not been provided.");

            var userRoleAlreadyInDb = await unitOfWork.UserRoleRepository.GetUserRoleByNameAsync(userRole.RoleName);
            if (userRoleAlreadyInDb != null)
                return new GenericResponse(false, $"User role {userRole.RoleName} already exists.");

            try
            {
                await unitOfWork.UserRoleRepository.AddUserRoleAsync(userRole);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "New user role has been created.");
        }

        public async Task<GenericResponse> RemoveUserRole(int id)
        {
            var userRoleToRemove = await unitOfWork.UserRoleRepository.GetUserRoleByIdAsync(id);
            if (userRoleToRemove == null)
                return new GenericResponse(false, $"User role with id={id} does not exist.");

            try
            {
                await unitOfWork.UserRoleRepository.RemoveUserRole(userRoleToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "User role has been removed.");
        }

        public async Task<GenericResponse> UpdateUserRole(UserRole userRole, int id)
        {
            if (userRole == null)
                return new GenericResponse(false, "User role has not been provided.");

            var userRoleToUpdate = await unitOfWork.UserRoleRepository.GetUserRoleByIdAsync(id);
            if (userRoleToUpdate == null)
                return new GenericResponse(false, $"User role with id={id} does not exist.");

            userRoleToUpdate.RoleName = userRole.RoleName;

            try
            {
                await unitOfWork.UserRoleRepository.UpdateUserRole(userRoleToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "User role has been updated.");
        }
    }
}
