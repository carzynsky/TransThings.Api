using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Constants;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWork unitOfWork;

        public DriverService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Driver>> GetAllDrivers()
        {
            var drivers = await unitOfWork.DriverRepository.GetAllDriversAsync();
            return drivers;
        }

        public async Task<List<Driver>> GetDriversByTransporter(int transporterId)
        {
            var drivers = await unitOfWork.DriverRepository.GetDriversByTransporter(transporterId);
            return drivers;
        }

        public async Task<Driver> GetDriverById(int id)
        {
            var driver = await unitOfWork.DriverRepository.GetDriverByIdAsync(id);
            return driver;
        }

        public async Task<GenericResponse> AddDriver(Driver driver)
        {
            if (driver == null)
                return new GenericResponse(false, DriverResponseMessage.DriverDataNotProvided);

            if (string.IsNullOrEmpty(driver.FirstName) || string.IsNullOrEmpty(driver.LastName))
                return new GenericResponse(false, DriverResponseMessage.FirstNameOrLastNameNotProvided);

            if (string.IsNullOrEmpty(driver.PeselNumber))
                return new GenericResponse(false, DriverResponseMessage.PeselNumberNotProvided);

            if (driver.BirthDate == null)
                return new GenericResponse(false, DriverResponseMessage.DriverBirthdateNotProvided);

            if (driver.Gender != 'm' && driver.Gender != 'M' && driver.Gender != 'k' && driver.Gender != 'K')
                return new GenericResponse(false, DriverResponseMessage.IncorrectGender);

            if (driver.Gender == 'm')
                driver.Gender = 'M';

            else if (driver.Gender == 'k')
                driver.Gender = 'K';

           /* bool isPeselValid = PeselValidator.Validate(driver.PeselNumber, driver.Gender, driver.BirthDate);
            if (!isPeselValid)
                return new GenericResponse(false, "Incorrect pesel number.");*/

            var driverAlreadyInDb = await unitOfWork.DriverRepository.GetDriverByPeselNumberAsync(driver.PeselNumber);
            if (driverAlreadyInDb != null)
                return new GenericResponse(false, DriverResponseMessage.DriverWithGivenPeselAlreadyExists);

            try
            {
                await unitOfWork.DriverRepository.AddDriverAsync(driver);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }

            return new GenericResponse(true, DriverResponseMessage.DriverCreated);
        }

        public async Task<GenericResponse> RemoveDriver(int id)
        {
            var driverToRemove = await unitOfWork.DriverRepository.GetDriverByIdAsync(id);
            if (driverToRemove == null)
                return new GenericResponse(false, DriverResponseMessage.DriverWithGivenIdNotExists);
            try
            {
                await unitOfWork.DriverRepository.RemoveDriver(driverToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, DriverResponseMessage.DriverRemoved);
        }

        public async Task<GenericResponse> UpdateDriver(Driver driver, int id)
        {
            if (driver == null)
                return new GenericResponse(false, DriverResponseMessage.DriverDataNotProvided);

            if (string.IsNullOrEmpty(driver.FirstName) || string.IsNullOrEmpty(driver.LastName))
                return new GenericResponse(false, DriverResponseMessage.FirstNameOrLastNameNotProvided);

            if (driver.BirthDate == null)
                return new GenericResponse(false, DriverResponseMessage.DriverBirthdateNotProvided);

            if (driver.Gender != 'm' && driver.Gender != 'M' && driver.Gender != 'k' && driver.Gender != 'K')
                return new GenericResponse(false, DriverResponseMessage.IncorrectGender);

            if (driver.Gender == 'm')
                driver.Gender = 'M';

            else if (driver.Gender == 'k')
                driver.Gender = 'K';


            if (string.IsNullOrEmpty(driver.PeselNumber))
                return new GenericResponse(false, DriverResponseMessage.PeselNumberNotProvided);

           /* bool isPeselValid = PeselValidator.Validate(driver.PeselNumber, driver.Gender, driver.BirthDate);
            if (!isPeselValid)
                return new GenericResponse(false, "Incorrect pesel number.");*/

            var driverToUpdate = await unitOfWork.DriverRepository.GetDriverByIdAsync(id);
            if (driverToUpdate == null)
                return new GenericResponse(false, DriverResponseMessage.DriverWithGivenIdNotExists);

            driverToUpdate.BirthDate = driver.BirthDate;
            driverToUpdate.ContactPhoneNumber = driver.ContactPhoneNumber;
            driverToUpdate.FirstName = driver.FirstName;
            driverToUpdate.LastName = driver.LastName;
            driverToUpdate.Gender = driver.Gender;
            driverToUpdate.Mail = driver.Mail;
            driverToUpdate.PeselNumber = driver.PeselNumber;
            driverToUpdate.TransporterId = driver.TransporterId;

            try
            {
                await unitOfWork.DriverRepository.UpdateDriver(driverToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, DriverResponseMessage.DriverUpdated);
        }
    }
}
