using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface ITransporterService
    {
        Task<List<Transporter>> GetAllTransporters();
        Task<Transporter> GetTransporterById(int id);
        Task<GenericResponse> AddTransporter(Transporter transporter);
        Task<GenericResponse> UpdateTransporter(Transporter transporter, int id);
        Task<GenericResponse> RemoveTransporter(int id);
    }
}
