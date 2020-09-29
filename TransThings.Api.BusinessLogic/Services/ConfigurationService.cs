using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IUnitOfWork unitOfWork;

        public ConfigurationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Configuration>> GetAllConfigurations()
        {
            var configurations = await unitOfWork.ConfigurationRepository.GetAllConfigurationsAsync();
            return configurations;
        }

        public async Task<Configuration> GetConfigurationById(int id)
        {
            var configuration = await unitOfWork.ConfigurationRepository.GetConfigurationByIdAsync(id);
            return configuration;
        }

        public async Task<GenericResponse> AddConfiguration(Configuration configuration)
        {
            if (configuration == null)
                return new GenericResponse(false, "No configuration has been provided.");

            if (string.IsNullOrEmpty(configuration.Name) || string.IsNullOrEmpty(configuration.Value))
                return new GenericResponse(false, "Couldn't add configuration. Name or value has not been provided.");

            var configurationAlreadyInDb = await unitOfWork.ConfigurationRepository.GetConfigurationByNameAsync(configuration.Name);
            if(configurationAlreadyInDb != null)
                return new GenericResponse(false, $"Configuration {configuration.Name} already exists.");

            try
            {
                await unitOfWork.ConfigurationRepository.AddConfigurationAsync(configuration);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Configuration has been added");
        }

        public async Task<GenericResponse> RemoveConfiguration(int id)
        {
            var configurationToRemove = await unitOfWork.ConfigurationRepository.GetConfigurationByIdAsync(id);
            if (configurationToRemove == null)
                return new GenericResponse(false, $"Configuration with id={id} does not exist.");

            try
            {
                await unitOfWork.ConfigurationRepository.RemoveConfiguration(configurationToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Configuration has been removed from database.");
        }

        public async Task<GenericResponse> UpdateConfiguration(Configuration configuration, int id)
        {
            if (configuration == null)
                return new GenericResponse(false, "No configuration has been provided");

            var configurationToUpdate = await unitOfWork.ConfigurationRepository.GetConfigurationByIdAsync(id);
            if (configurationToUpdate == null)
                return new GenericResponse(false, $"Configuration with id={id} does not exist.");

            var configurationAlreadyExistsInDb = await unitOfWork.ConfigurationRepository.GetConfigurationByNameAsync(configuration.Name);
            if (configurationAlreadyExistsInDb != null)
                return new GenericResponse(false, $"Configuration {configuration.Name} already exists.");

            configurationToUpdate.Name = configuration.Name;
            configurationToUpdate.Value = configuration.Value;

            try
            {
                await unitOfWork.ConfigurationRepository.UpdateConfiguration(configurationToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Configuration has been updated.");
        }
    }
}
