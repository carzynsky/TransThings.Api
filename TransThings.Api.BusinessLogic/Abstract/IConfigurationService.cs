using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface IConfigurationService
    {
        Task<List<Configuration>> GetAllConfigurations();
        Task<Configuration> GetConfigurationById(int id);
        Task<GenericResponse> AddConfiguration(Configuration configuration);
        Task<GenericResponse> UpdateConfiguration(Configuration configuration, int id);
        Task<GenericResponse> RemoveConfiguration(int id);
    }
}
