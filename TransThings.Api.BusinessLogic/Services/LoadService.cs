using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Dto;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class LoadService : ILoadService
    {
        private readonly IUnitOfWork unitOfWork;

        public LoadService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<Load>> GetAllLoads()
        {
            var loads = await unitOfWork.LoadRepository.GetAllLoadsAsync();
            return loads;
        }

        public async Task<List<Load>> GetLoadsByOrder(int orderId)
        {
            var loads = await unitOfWork.LoadRepository.GetLoadsByOrderAsync(orderId);
            return loads;
        }

        public async Task<List<Load>> GetLoadsByManyOrders(OrderIdsDto orders)
        {
            var allLoads = new List<Load>();
            foreach(var orderId in orders.OrderIds)
            {
                var loads = await unitOfWork.LoadRepository.GetLoadsByOrderAsync(orderId);
                if (loads == null) continue;

                allLoads.AddRange(loads);
            }
            return allLoads;
        }

        public async Task<Load> GetLoadById(int id)
        {
            var load = await unitOfWork.LoadRepository.GetLoadByIdAsync(id);
            return load;
        }

        public async Task<GenericResponse> AddLoad(LoadDto loads)
        {
            if (loads == null)
                return new GenericResponse(false, "Load data has not been provided.");

            /*foreach(var load in loads.Loads)
                if (string.IsNullOrEmpty(load.))
                    return new GenericResponse(false, "Load name has not been provided.");*/

            try
            {
                await unitOfWork.LoadRepository.AddLoadAsync(loads.Loads);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Towary zostały dodane.");
        }

        public async Task<GenericResponse> RemoveLoad(int id)
        {
            var loadToRemove = await unitOfWork.LoadRepository.GetLoadByIdAsync(id);
            if (loadToRemove == null)
                return new GenericResponse(false, "Load with given id does not exist.");

            try
            {
                await unitOfWork.LoadRepository.RemoveLoad(loadToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Load has been removed.");
        }

        public async Task<GenericResponse> UpdateLoads(LoadDto loads, int orderId)
        {
            if (loads == null)
                return new GenericResponse(false, "No loads have been provided.");

            /*var loadToUpdate = await unitOfWork.LoadRepository.GetLoadByIdAsync(id);
            if (loadToUpdate == null)
                return new GenericResponse(false, $"Load with id={id} does not exist.");*/

            /*if (string.IsNullOrEmpty(load.Name))
                return new GenericResponse(false, "Load name has not been provided.");*/

            // get old loads
            var oldLoads = await GetLoadsByOrder(orderId);

            // create new list of loads to add or update
            var loadsToAddOrUpdate = new List<Load>();

            foreach(var load in loads.Loads)
            {
                var loadToUpdate = await unitOfWork.LoadRepository.GetLoadByIdAsync(load.Id);
                if (loadToUpdate == null)
                {
                    loadsToAddOrUpdate.Add(load);
                    continue;
                }

                loadToUpdate.Amount = load.Amount;
                loadToUpdate.GrossWeight = load.GrossWeight;
                loadToUpdate.Name = load.Name;
                loadToUpdate.NetWeight = load.NetWeight;
                loadToUpdate.PackageType = load.PackageType;
                loadToUpdate.Volume = load.Volume;
                loadToUpdate.Weight = load.Weight;
                loadsToAddOrUpdate.Add(loadToUpdate);
            }

            // Catch removed loads on frontend and remove them from db
            foreach(var oldLoad in oldLoads)
            {
                var _loadToDelete = loadsToAddOrUpdate.FirstOrDefault(x => x.Id.Equals(oldLoad.Id));

                if (_loadToDelete == null)
                    await unitOfWork.LoadRepository.RemoveLoad(oldLoad);
            }

            try
            {
                await unitOfWork.LoadRepository.UpdateLoads(loadsToAddOrUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Towary zostały dodane lub zaktualizowane.");
        }
    }
}
