using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class TransitService : ITransitService
    {
        private readonly IUnitOfWork unitOfWork;

        public TransitService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Transit>> GetAllTransits()
        {
            var transits = await unitOfWork.TransitRepository.GetAllTransitsAsync();
            return transits;
        }

        public async Task<Transit> GetTransitById(int id)
        {
            var transit = await unitOfWork.TransitRepository.GetTransitByIdAsync(id);
            return transit;
        }

        public async Task<GenericResponse> AddTransit(Transit transit)
        {
            if (transit == null)
                return new GenericResponse(false, "No transit has been provided.");

            try
            {
                await unitOfWork.TransitRepository.AddTransitAsync(transit);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "New transit has been created.");
        }

        public async Task<GenericResponse> RemoveTransit(int id)
        {
            var transitToRemove = await unitOfWork.TransitRepository.GetTransitByIdAsync(id);
            if (transitToRemove == null)
                return new GenericResponse(false, $"Transit with id={id} does not exist.");

            try
            {
                await unitOfWork.TransitRepository.RemoveTransit(transitToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Transit has been removed.");
        }

        public async Task<GenericResponse> UpdateTransit(Transit transit, int id)
        {
            if (transit == null)
                return new GenericResponse(false, "No transit has been provided.");

            var transitToUpdate = await unitOfWork.TransitRepository.GetTransitByIdAsync(id);
            if (transitToUpdate == null)
                return new GenericResponse(false, $"Transit with id={id} does not exist.");

            transitToUpdate.GrossPrice = transit.GrossPrice;
            transitToUpdate.NetPrice = transit.NetPrice;
            transitToUpdate.PaymentFormId = transit.PaymentFormId;
            transitToUpdate.RouteShortPath = transit.RouteShortPath;
            transitToUpdate.TransitDestinationCity = transit.TransitDestinationCity;
            transitToUpdate.TransitDestinationCountry = transit.TransitDestinationCountry;
            transitToUpdate.TransitDestinationStreetAddress = transit.TransitDestinationStreetAddress;
            transitToUpdate.TransitDestinationZipCode = transit.TransitDestinationZipCode;
            transitToUpdate.TransitSourceCity = transit.TransitSourceCity;
            transitToUpdate.TransitSourceCountry = transit.TransitSourceCountry;
            transitToUpdate.TransitSourceStreetAddress = transit.TransitSourceStreetAddress;
            transitToUpdate.TransitSourceZipCode = transit.TransitSourceZipCode;
            try
            {
                await unitOfWork.TransitRepository.UpdateTransit(transitToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Transit has been updated.");
        }

        public async Task<List<Transit>> GetTransitsByForwardingOrder(int forwardingOrderId)
        {
            var transitsForwardingOrders = await unitOfWork.TransitForwardingOrderRepository.GetTransitForwardingOrdersByForwardingOrderAsync(forwardingOrderId);
            if(transitsForwardingOrders != null)
            {
                List<Transit> transits = new List<Transit>();
                foreach(var t in transitsForwardingOrders)
                {
                    var transit = await unitOfWork.TransitRepository.GetTransitByIdAsync(t.TransitId);
                    if (transit != null)
                        transits.Add(transit);
                }
                return transits;
            }
            return null;
        }
    }
}
