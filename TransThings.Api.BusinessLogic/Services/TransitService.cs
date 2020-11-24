using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Dto;
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

        public async Task<TransitStats> GetTransitStats()
        {
            TransitStats transitStats = new TransitStats();
            var transits = await unitOfWork.TransitRepository.GetOnlyTransitAsync();
            if (transits == null || transits.Count() == 0)
                return transitStats;

            int len = 6;
            var transitsByLastMonths = new List<TransitsByMonth>(len);
            var today = DateTime.Now;
            var firstIndex = today.AddMonths(-6);

            string[] monthsTranslate = new string[12] { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień",
            "Wrzesień", "Pażdziernik", "Listopad", "Grudzień"};

            for (int i = 5; i >= 0; i--)
            {
                var _month = today.AddMonths(-i);
                var counter = transits.Where(x => x.StartDate?.Month == _month.Month
                && x.StartDate?.Year == _month.Year).Count();

                transitsByLastMonths.Add(new TransitsByMonth(monthsTranslate[_month.Month - 1] + " " + _month.Year, counter));
            }

            transitStats.TransitsByLastMonths = transitsByLastMonths;

            return transitStats;
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

        public async Task<GenericResponse> UpdateTransits(TransitDto transits, int forwardingOrderId)
        {
            if (transits == null || transits.Transits == null)
                return new GenericResponse(false, "Dane przejazdów nie zostały podane");

            // get old transits from transitForwardingOrders table
            var oldTransitForwardingOrders = await unitOfWork.TransitForwardingOrderRepository.GetTransitForwardingOrdersByForwardingOrderAsync(forwardingOrderId);
            var oldTransits = new List<Transit>();
            foreach(var oldTransitForwardingOrder in oldTransitForwardingOrders)
            {
                var oldTransit = await unitOfWork.TransitRepository.GetTransitByIdAsync(oldTransitForwardingOrder.TransitId);
                if (oldTransit == null) continue;

                oldTransits.Add(oldTransit);
            }

            // create new list of transits to add or update
            var transitsToAddOrUpdate = new List<Transit>();

            // create new list of transitForwardingOders to add or update
            var transitForwardingOrdersToAddOrUpdate = new List<TransitForwardingOrder>();

            foreach(var _transit in transits.Transits)
            {
                var transitToUpdate = await unitOfWork.TransitRepository.GetTransitByIdAsync(_transit.Id);
                if (transitToUpdate == null)
                {
                    transitsToAddOrUpdate.Add(_transit);
                    continue;
                }

                #region Transit data update
                transitToUpdate.GrossPrice = _transit.GrossPrice;
                transitToUpdate.NetPrice = _transit.NetPrice;
                transitToUpdate.TransportDistance = _transit.TransportDistance;
                transitToUpdate.PaymentFormId = _transit.PaymentFormId;
                transitToUpdate.DriverId = _transit.DriverId;
                transitToUpdate.StartDate = _transit.StartDate;
                transitToUpdate.EndDate = _transit.EndDate;
                transitToUpdate.TransporterId = _transit.TransporterId;
                transitToUpdate.VehicleId = _transit.VehicleId;
                transitToUpdate.RouteShortPath = _transit.RouteShortPath;
                transitToUpdate.TransitDestinationCity = _transit.TransitDestinationCity;
                transitToUpdate.TransitDestinationCountry = _transit.TransitDestinationCountry;
                transitToUpdate.TransitDestinationStreetAddress = _transit.TransitDestinationStreetAddress;
                transitToUpdate.TransitDestinationZipCode = _transit.TransitDestinationZipCode;
                transitToUpdate.TransitSourceCity = _transit.TransitSourceCity;
                transitToUpdate.TransitSourceCountry = _transit.TransitSourceCountry;
                transitToUpdate.TransitSourceStreetAddress = _transit.TransitSourceStreetAddress;
                transitToUpdate.TransitSourceZipCode = _transit.TransitSourceZipCode;
                transitsToAddOrUpdate.Add(transitToUpdate);
                #endregion
            }

            // catch removed transits from given forwarding order
            foreach(var oldTransit in oldTransits)
            {
                var transitToDelete = transitsToAddOrUpdate.FirstOrDefault(x => x.Id.Equals(oldTransit.Id));
                if(transitToDelete == null)
                {
                    // get all transit forwarding order by transit id to check, if there are other forwarding orders that use this transit
                    var transitsForwardingOrderToDeleteByTransitId = await unitOfWork.TransitForwardingOrderRepository.GetTransitForwardingOrdersByTransitAsync(oldTransit.Id);
                    if (transitsForwardingOrderToDeleteByTransitId == null || transitsForwardingOrderToDeleteByTransitId.Count() == 0) continue;

                    try
                    {
                        var _transitForwardinOrderToDelete = transitsForwardingOrderToDeleteByTransitId.FirstOrDefault(x => x.ForwardingOrderId.Equals(forwardingOrderId));
                        await unitOfWork.TransitForwardingOrderRepository.RemoveTransitForwardingOrder(_transitForwardinOrderToDelete);
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        return new GenericResponse(false, ex.InnerException.Message);
                    }
                    catch (DbUpdateException ex)
                    {
                        return new GenericResponse(false, ex.InnerException.Message);
                    }

                    // filter those which are not related to given forwarding order
                    var transitsForwardingOrderToDelete = transitsForwardingOrderToDeleteByTransitId.Where(x => x.ForwardingOrderId != forwardingOrderId);
                    if (transitsForwardingOrderToDelete.Count() == 0)
                    {
                        // delete also transit from transits table
                        try
                        {
                            await unitOfWork.TransitRepository.RemoveTransit(oldTransit);
                        }
                        catch (DbUpdateConcurrencyException ex)
                        {
                            return new GenericResponse(false, ex.InnerException.Message);
                        }
                        catch (DbUpdateException ex)
                        {
                            return new GenericResponse(false, ex.InnerException.Message);
                        }
                        continue;
                    }
                }
            }

            try
            {
                await unitOfWork.TransitRepository.UpdateTransits(transitsToAddOrUpdate);
                foreach(var _transit in transits.Transits)
                {
                    var transitForwardingOrderToUpdate = await unitOfWork.TransitForwardingOrderRepository.GetTransitForwardingOrderByTransitAndForwardingOrderAsync(_transit.Id, forwardingOrderId);

                    if (transitForwardingOrderToUpdate == null)
                    {
                        var newTransitForwardingOrder = new TransitForwardingOrder()
                        {
                            TransitId = _transit.Id,
                            ForwardingOrderId = forwardingOrderId
                        };
                        transitForwardingOrdersToAddOrUpdate.Add(newTransitForwardingOrder);
                    }
                }

                await unitOfWork.TransitForwardingOrderRepository.UpdateTransitForwardingOrders(transitForwardingOrdersToAddOrUpdate);

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
