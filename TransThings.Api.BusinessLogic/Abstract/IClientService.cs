using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface IClientService
    {
        Task<List<Client>> GetAllClients();
        Task<Client> GetClientById(int id);
        Task<GenericResponse> AddClient(Client client);
        Task<GenericResponse> UpdateClient(Client client, int id);
        Task<GenericResponse> RemoveClient(int id);
    }
}
