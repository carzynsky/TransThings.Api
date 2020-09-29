using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class LoginHistoryService : ILoginHistoryService
    {
        private readonly IUnitOfWork unitOfWork;

        public LoginHistoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<LoginHistory>> GetAllLoginHistories()
        {
            var loginHistories = await unitOfWork.LoginHistoryRepository.GetAllLoginHistoryAsync();
            return loginHistories;
        }

        public async Task<List<LoginHistory>> GetLoginHistoryByUser(int userId)
        {
            var loginHistory = await unitOfWork.LoginHistoryRepository.GetLoginHistoryByUserIdAsync(userId);
            return loginHistory;
        }

        public async Task<GenericResponse> AddLoginHistory(int userId, bool wasSuccessful)
        {
            LoginHistory loginHistory = new LoginHistory()
            {
                AttemptDate = DateTime.Now,
                IsSuccessful = wasSuccessful,
                UserId = userId
            };

            try
            {
                await unitOfWork.LoginHistoryRepository.AddLoginHistoryAsync(loginHistory);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Login history has been added.");
        }

        public async Task<bool> CheckIfLogInShouldBeBlocked(int userId)
        {
            var loginHistory = await unitOfWork.LoginHistoryRepository.GetLoginHistoryByUserIdAsync(userId);
            if (loginHistory == null)
                return false;

            int length = loginHistory.Count;
            if (length < 5)
                return false;

            int wrongAttemptsCounter = 0;

            for(int i=length-1; i>length-6; i--)
            {
                if (!loginHistory[i].IsSuccessful)
                    wrongAttemptsCounter++;
            }
            var totalMinutes = (DateTime.Now - loginHistory[length - 1].AttemptDate).TotalMinutes;
            if (wrongAttemptsCounter == 5 && totalMinutes < 10)
                return true;

            return false;
        }
    }
}
