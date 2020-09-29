using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Dto;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await unitOfWork.UserRepository.GetAllUsersAsync();
            if (users == null)
                return null;

            List<UserDto> usersDto = new List<UserDto>();
            foreach(var user in users)
            {
                usersDto.Add(new UserDto()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    BirthDate = user.BirthDate,
                    DateOfEmployment = user.DateOfEmployment,
                    Login = user.Login,
                    Mail = user.Mail,
                    PhoneNumber = user.PhoneNumber,
                    UserRoleId = user.UserRoleId
                });
            }
            return usersDto;
        }

        public async Task<List<UserDto>> GetAllUsersByRole(int userRoleId)
        {
            var users = await unitOfWork.UserRepository.GetAllUsersByRole(userRoleId);
            if (users == null)
                return null;

            List<UserDto> usersDto = new List<UserDto>();
            foreach (var user in users)
            {
                usersDto.Add(new UserDto()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    BirthDate = user.BirthDate,
                    DateOfEmployment = user.DateOfEmployment,
                    Login = user.Login,
                    Mail = user.Mail,
                    PhoneNumber = user.PhoneNumber,
                    UserRoleId = user.UserRoleId
                });
            }
            return usersDto;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(id);
            if (user == null)
                return null;

            UserDto userDto = new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                BirthDate = user.BirthDate,
                DateOfEmployment = user.DateOfEmployment,
                Login = user.Login,
                Mail = user.Mail,
                PhoneNumber = user.PhoneNumber,
                UserRoleId = user.UserRoleId
            };
            return userDto;
        }

        public async Task<AddUserResponse> AddUser(UserDto userDto)
        {
            if (userDto == null)
                return new AddUserResponse(false, "User data has not been provided.", null);

            if (string.IsNullOrEmpty(userDto.FirstName) || string.IsNullOrEmpty(userDto.LastName) || userDto == null || string.IsNullOrEmpty(userDto.Login))
                return new AddUserResponse(false, "You haven not provided enough data.", null);

            var userWithGivenLogin = await unitOfWork.UserRepository.GetUserByLoginAsync(userDto.Login);
            if (userWithGivenLogin != null)
                return new AddUserResponse(false, "User with given login already exists.", null);

            if (userDto.Gender != 'm' && userDto.Gender != 'M' && userDto.Gender != 'k' && userDto.Gender != 'K')
                return new AddUserResponse(false, "Incorrect gender has been provided.", null);

            if (userDto.Gender == 'm')
                userDto.Gender = 'M';

            else if (userDto.Gender == 'k')
                userDto.Gender = 'K';

            bool isPeselValid = PeselValidator.Validate(userDto.PeselNumber, userDto.Gender, userDto.BirthDate);
            if (!isPeselValid)
                return new AddUserResponse(false, "Pesel number is incorrect.", null);

            User newUser = new User()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                BirthDate = userDto.BirthDate,
                Gender = userDto.Gender,
                DateOfEmployment = userDto.DateOfEmployment,
                Login = userDto.Login,
                Mail = userDto.Mail,
                PeselNumber = userDto.PeselNumber,
                PhoneNumber = userDto.PhoneNumber,
                UserRoleId = userDto.UserRoleId
            };

            var password = GenerateTemporaryPassword();
            HashPassword hash = new HashPassword(password);
            newUser.Password = hash.HashedPassword; // temporary password, no validation needed
            try
            {
                await unitOfWork.UserRepository.AddUserAsync(newUser);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new AddUserResponse(false, ex.InnerException.Message, null);
            }
            catch (DbUpdateException ex)
            {
                return new AddUserResponse(false, ex.InnerException.Message, null);
            }
            return new AddUserResponse(true, "New user has been created.", password);
        }

        public async Task<GenericResponse> ChangePassword(ChangePasswordData changePasswordData, int id)
        {
            if (changePasswordData == null)
                return new GenericResponse(false, "No data has been provided.");

            if (string.IsNullOrEmpty(changePasswordData.NewPassword) || string.IsNullOrEmpty(changePasswordData.NewPasswordAgain))
                return new GenericResponse(false, "New password has not been provided.");

            if (changePasswordData.NewPassword != changePasswordData.NewPasswordAgain)
                return new GenericResponse(false, "Provided passwords are not identical.");

            var userToUpdatePassword = await unitOfWork.UserRepository.GetUserByIdAsync(id);
            if (userToUpdatePassword == null)
                return new GenericResponse(false, "User with given id does not exist.");

            HashPassword hash = new HashPassword(changePasswordData.NewPassword);
            if (userToUpdatePassword.Password == hash.HashedPassword)
                return new GenericResponse(false, "New password has to be different than the old one.");

            bool isPasswordSecure = IsPasswordSecure(changePasswordData.NewPassword, userToUpdatePassword.FirstName, userToUpdatePassword.LastName, userToUpdatePassword.Login);
            if (!isPasswordSecure)
                return new GenericResponse(false, "This password is not safe.");

            hash = new HashPassword(changePasswordData.NewPassword);
            userToUpdatePassword.Password = hash.HashedPassword;
            try
            {
                await unitOfWork.UserRepository.UpdateUser(userToUpdatePassword);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Password has been changed.");
        }

        public async Task<GenericResponse> RemoveUser(int userId)
        {
            var userToRemove = await unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if (userToRemove == null)
                return new GenericResponse(false, "User with given id does not exist.");

            try
            {
                await unitOfWork.UserRepository.RemoveUser(userToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "User has been removed.");
        }

        public async Task<GenericResponse> UpdateUser(UserDto userDto, int id)
        {
            if (userDto == null)
                return new GenericResponse(false, "User has not been provided.");

            var userToUpdate = await unitOfWork.UserRepository.GetUserByIdAsync(id);
            if (userToUpdate == null)
                return new GenericResponse(false, "User with given id does not exist.");

            if (string.IsNullOrEmpty(userDto.FirstName) || string.IsNullOrEmpty(userDto.LastName))
                return new GenericResponse(false, "First name or last name data is incorrect.");

            if (string.IsNullOrEmpty(userDto.Login))
                return new GenericResponse(false, "Login cannot be null or empty.");

            var userWithThisLogin = await unitOfWork.UserRepository.GetUserByLoginAsync(userDto.Login);
            if (userWithThisLogin != null && userToUpdate.Login != userDto.Login)
                return new GenericResponse(false, "Cannot change login name, because provided one is taken by another user.");

            if (userDto.Gender != 'm' && userDto.Gender != 'M' && userDto.Gender != 'k' && userDto.Gender != 'K')
                return new GenericResponse(false, "Incorrect gender has been provided.");

            if (userDto.Gender == 'm')
                userDto.Gender = 'M';

            else if (userDto.Gender == 'k')
                userDto.Gender = 'K';

            bool isPeselValid = PeselValidator.Validate(userDto.PeselNumber, userDto.Gender, userDto.BirthDate);
            if (!isPeselValid)
                return new GenericResponse(false, "Pesel number is incorrect.");

            userToUpdate.FirstName = userDto.FirstName;
            userToUpdate.LastName = userDto.LastName;
            userToUpdate.BirthDate = userDto.BirthDate;
            userToUpdate.DateOfEmployment = userDto.DateOfEmployment;
            userToUpdate.Login = userDto.Login;
            userToUpdate.Mail = userDto.Mail;
            userToUpdate.PeselNumber = userDto.PeselNumber;
            userToUpdate.PhoneNumber = userDto.PhoneNumber;
            userToUpdate.UserRoleId = userDto.UserRoleId;

            try
            {
                await unitOfWork.UserRepository.UpdateUser(userToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "User has been updated.");

        }

        public async Task<int> GetUserIdByLogin(string login)
        {
            var user = await unitOfWork.UserRepository.GetUserByLoginAsync(login);
            return user.Id;
        }

        private string GenerateTemporaryPassword()
        {
            int len = 12;
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();

            for(int i=0; i<len; i++)
            {
                stringBuilder.Append((char)random.Next(35, 126));
            }

            return stringBuilder.ToString();
        }

        private bool IsPasswordSecure(string password, string firstName, string lastName, string login)
        {
            string passwordToLower = password.ToLower();

            Regex hasMinimumLength = new Regex(@"^.{12,}$");
            Regex hasAtLeastOneLowercase = new Regex(@"[a-z]+");
            Regex hasAtLeastOneUppercase = new Regex(@"[A-Z]+");
            Regex hasAtLeastOneDigit = new Regex(@"[0-9]+");
            Regex hasAtLeastOneSpecialSign = new Regex(@"[!@#$%^&*()_\-+={[}\]:;""'<,>.?\/]");

            bool doesContainFirstName = passwordToLower.Contains(firstName.ToLower());
            bool doesContainLastName = passwordToLower.Contains(lastName.ToLower());
            bool doesContainLogin = passwordToLower.Contains(login.ToLower());

            if (hasMinimumLength.IsMatch(password) && hasAtLeastOneLowercase.IsMatch(password)
                && hasAtLeastOneUppercase.IsMatch(password) && hasAtLeastOneDigit.IsMatch(password)
                && hasAtLeastOneSpecialSign.IsMatch(password) && !doesContainFirstName && !doesContainLastName
                && !doesContainLogin)
                return true;

            return false;
        }
    }
}
