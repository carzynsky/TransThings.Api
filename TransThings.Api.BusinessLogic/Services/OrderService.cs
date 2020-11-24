using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await unitOfWork.OrderRepository.GetAllOrdersAsync();
            return orders;
        }

        public async Task<List<Order>> GetOrdersByClientId(int clientId)
        {
            var orders = await unitOfWork.OrderRepository.GetOrdersByClientAsync(clientId);
            return orders;
        }

        public async Task<List<Order>> GetOrdersByStatus(int orderStatusId)
        {
            var orders = await unitOfWork.OrderRepository.GetOrdersByStatusAsync(orderStatusId);
            return orders;
        }

        public async Task<List<Order>> GetOrdersByOrderer(int ordererId)
        {
            var orders = await unitOfWork.OrderRepository.GetOrdersByOrdererAsync(ordererId);
            return orders;
        }

        public async Task<List<Order>> GetOrdersByConsultant(int consultantId)
        {
            var orders = await unitOfWork.OrderRepository.GetOrdersByConsultantAsync(consultantId);
            return orders;
        }

        public async Task<List<Order>> GetOrdersByForwardingOrder(int forwardingOrderId)
        {
            var orders = await unitOfWork.OrderRepository.GetOrdersByForwardingOrderAsync(forwardingOrderId);
            return orders;
        }

        public async Task<OrderStats> GetOrdersStats()
        {
            OrderStats orderStats = new OrderStats();
            var orders = await unitOfWork.OrderRepository.GetOnlyOrders();
            if (orders == null || orders.Count() == 0)
                return orderStats;

            int len = 6;
            var ordersByLastMonths = new List<OrdersByMonth>(len);
            var today = DateTime.Now;
            var firstIndex = today.AddMonths(-6);

            string[] monthsTranslate = new string[12] { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień",
            "Wrzesień", "Pażdziernik", "Listopad", "Grudzień"};

            for (int i = 5; i >= 0; i--)
            {
                var _month = today.AddMonths(-i);
                var counter = orders.Where(x => x.OrderCreationDate?.Month == _month.Month
                && x.OrderCreationDate?.Year == _month.Year).Count();

                ordersByLastMonths.Add(new OrdersByMonth(monthsTranslate[_month.Month - 1] + " " + _month.Year, counter));
            }
            orderStats.OrdersByLastMonths = ordersByLastMonths;

            orderStats.OrdersThisMonth = ordersByLastMonths[len - 1].AmountOfOrders;
            orderStats.OrdersLastMonth = ordersByLastMonths[len - 2].AmountOfOrders;

            int diff = orderStats.OrdersThisMonth - orderStats.OrdersLastMonth;

            orderStats.LastMonthComparerMessage = 
                diff >= 0 ? $"+{diff}" : diff.ToString();

            int? finishedOrderStatusId = null;
            var orderStatuses = await unitOfWork.OrderStatusRepository.GetOrderStatusByNameAsync("Zakończone");
            if (orderStatuses != null)
                finishedOrderStatusId = orderStatuses.Id;

            var finishedOrders = orders.Where(x => x.OrderStatusId.Equals(finishedOrderStatusId));

            if (finishedOrders == null || finishedOrders.Count() == 0)
                orderStats.TimelyDeliveriesRatio = 100;

            else
            {
                int all = finishedOrders.Count();
                int finishedOnTimeCounter = finishedOrders
                    .Where(x => x.OrderRealizationDate?.Date <= x.OrderExpectedDate?.Date).Count();
                decimal ratio = (finishedOnTimeCounter / all) * 100;
                orderStats.TimelyDeliveriesRatio = (int) ratio;
            }

            return orderStats;
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await unitOfWork.OrderRepository.GetOrderByIdAsync(id);
            return order;
        }

        public async Task<OrderResponse> AddOrder(Order order)
        {
            if (order == null)
                return new OrderResponse(false, "No order has been provided.", null);

            if (order.OrderNumber != null)
                return new OrderResponse(false, "Order number can't be provided manaully.", null);

            order.OrderNumber = await CreateOrderNumber();

            var orderStatusName = await unitOfWork.OrderStatusRepository.GetOrderStatusByNameAsync("Utworzone");
            if (orderStatusName == null)
                return new OrderResponse(false, "Brak poprawnego statusu zamówienia.", null);

            order.OrderStatusId = orderStatusName.Id;
            order.OrderCreationDate = DateTime.Now;

            try
            {
                await unitOfWork.OrderRepository.AddOrderAsync(order);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new OrderResponse(false, ex.InnerException.Message, null);
            }
            catch (DbUpdateException ex)
            {
                return new OrderResponse(false, ex.InnerException.Message, null);
            }

            int? orderId = order?.Id;
            return new OrderResponse(true, $"Utworzono zamówienie {order.OrderNumber}", orderId);
        }

        public async Task<GenericResponse> RemoveOrder(int id)
        {
            var orderToRemove = await unitOfWork.OrderRepository.GetOrderByIdAsync(id);
            if (orderToRemove == null)
                return new GenericResponse(false, $"Zamówienie o id={id} nie istnieje.");

            try
            {
                await unitOfWork.OrderRepository.RemoveOrder(orderToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Order has been removed.");
        }

        public async Task<GenericResponse> UpdateOrder(Order order, int id)
        {
            if (order == null)
                return new GenericResponse(false, "Nie podano danych zamówienia");

            var orderToUpdate = await unitOfWork.OrderRepository.GetOrderByIdAsync(id);
            if (orderToUpdate == null)
                return new GenericResponse(false, $"Zamówienie o id={id} nie istnieje.");

            // Another logic that might not be handled
            /*if (orderToUpdate.OrderNumber != order.OrderNumber)
                return new GenericResponse(false, "Order number can't be modified.");*/

            /*if (orderToUpdate.OrderCreationDate != order.OrderCreationDate)
                return new GenericResponse(false, "Order create date can't be modified.");*/

            int? finishedOrderStatusId = null;
            var orderStatuses = await unitOfWork.OrderStatusRepository.GetOrderStatusByNameAsync("Zakończone");
            if (orderStatuses != null)
                finishedOrderStatusId = orderStatuses.Id;

            #region Update order data
            orderToUpdate.ClientId = order.ClientId;
            orderToUpdate.ConsultantId = order.ConsultantId;
            orderToUpdate.CustomerAdditionalInstructions = order.CustomerAdditionalInstructions;
            orderToUpdate.DestinationCity = order.DestinationCity;
            orderToUpdate.DestinationCountry = order.DestinationCountry;
            orderToUpdate.DestinationStreetAddress = order.DestinationStreetAddress;
            orderToUpdate.DestinationZipCode = order.DestinationZipCode;
            orderToUpdate.ForwardingOrderId = order.ForwardingOrderId;
            orderToUpdate.GrossPrice = order.GrossPrice;
            orderToUpdate.IsAvailableAtWarehouse = order.IsAvailableAtWarehouse;
            orderToUpdate.IsClientVerified = order.IsClientVerified;
            orderToUpdate.NetPrice = order.NetPrice;
            orderToUpdate.OrdererId = order.OrdererId;
            orderToUpdate.OrderExpectedDate = order.OrderExpectedDate;
            orderToUpdate.OrderStatusId = order.OrderStatusId;
            orderToUpdate.PaymentFormId = order.PaymentFormId;
            orderToUpdate.TotalGrossWeight = order.TotalGrossWeight;
            orderToUpdate.TotalNetWeight = order.TotalNetWeight;
            orderToUpdate.TotalVolume = order.TotalVolume;
            orderToUpdate.TransportDistance = order.TransportDistance;
            orderToUpdate.VehicleTypeId = order.VehicleTypeId;
            orderToUpdate.WarehouseId = order.WarehouseId;

            if (order.OrderStatusId == finishedOrderStatusId)
                orderToUpdate.OrderRealizationDate = DateTime.Now;
            #endregion

            try
            {
                await unitOfWork.OrderRepository.UpdateOrder(orderToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Zamówienie zostało zaktualizowane");
        }

        /// <summary>
        /// Metoda generująca numer zamówienia transportu
        /// </summary>
        /// <returns></returns>
        private async Task<string> CreateOrderNumber()
        {
            string prefix = "ZT";
            string todayDate = DateTime.Now.ToString("ddMMyyyy");
            string timeNow = DateTime.Now.ToString("HHmmss");

            int lastOrderId;
            var lastOrder = await unitOfWork.OrderRepository.GetLastOrderAsync();
            lastOrderId = lastOrder is null ? 1 : lastOrder.Id + 1;

            return string.Concat(prefix, "-", todayDate, "-", timeNow, "-", lastOrderId);
        }
    }
}
