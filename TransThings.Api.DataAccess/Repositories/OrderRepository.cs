using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class OrderRepository
    {
        private readonly TransThingsDbContext context;

        public OrderRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var orders = await context.Orders.Include(x => x.Client).Include(x => x.Consultant)
                .Include(x => x.ForwardingOrder).Include(x => x.Orderer).Include(x => x.OrderStatus)
                .Include(x => x.PaymentForm).Include(x => x.VehicleType).Include(x => x.Warehouse).ToListAsync();
            return orders;
        }

        public async Task<List<Order>> GetOrdersByClientAsync(int clientId)
        {
            var orders = await context.Orders.Where(x => x.ClientId.Equals(clientId)).Include(x => x.Client).Include(x => x.Consultant)
                .Include(x => x.ForwardingOrder).Include(x => x.Orderer).Include(x => x.OrderStatus)
                .Include(x => x.PaymentForm).Include(x => x.VehicleType).Include(x => x.Warehouse).ToListAsync();
            return orders;
        }

        public async Task<List<Order>> GetOrdersByStatusAsync(int orderStatusId)
        {
            var orders = await context.Orders.Where(x => x.OrderStatusId.Equals(orderStatusId)).Include(x => x.Client).Include(x => x.Consultant)
                .Include(x => x.ForwardingOrder).Include(x => x.Orderer).Include(x => x.OrderStatus)
                .Include(x => x.PaymentForm).Include(x => x.VehicleType).Include(x => x.Warehouse).ToListAsync();
            return orders;
        }

        public async Task<List<Order>> GetOrdersByOrdererAsync(int ordererId)
        {
            var orders = await context.Orders.Where(x => x.OrdererId.Equals(ordererId)).Include(x => x.Client).Include(x => x.Consultant)
                .Include(x => x.ForwardingOrder).Include(x => x.Orderer).Include(x => x.OrderStatus)
                .Include(x => x.PaymentForm).Include(x => x.VehicleType).Include(x => x.Warehouse).ToListAsync();
            return orders;
        }

        public async Task<List<Order>> GetOrdersByConsultantAsync(int consultantId)

        {
            var orders = await context.Orders.Where(x => x.ConsultantId.Equals(consultantId)).Include(x => x.Client).Include(x => x.Consultant)
                .Include(x => x.ForwardingOrder).Include(x => x.Orderer).Include(x => x.OrderStatus)
                .Include(x => x.PaymentForm).Include(x => x.VehicleType).Include(x => x.Warehouse).ToListAsync();
            return orders;
        }

        public async Task<List<Order>> GetOrdersByForwardingOrderAsync(int forwardingOrderId)
        {
            var orders = await context.Orders.Where(x => x.ForwardingOrderId.Equals(forwardingOrderId))
                .Include(x => x.Client).Include(x => x.Consultant)
                .Include(x => x.ForwardingOrder).Include(x => x.Orderer).Include(x => x.OrderStatus)
                .Include(x => x.PaymentForm).Include(x => x.VehicleType).Include(x => x.Warehouse).ToListAsync();
            return orders;
        }
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await context.Orders.Include(x => x.Client).Include(x => x.Consultant)
                .Include(x => x.ForwardingOrder).Include(x => x.Orderer).Include(x => x.OrderStatus)
                .Include(x => x.PaymentForm).Include(x => x.VehicleType).Include(x => x.Warehouse).SingleOrDefaultAsync(x => x.Id.Equals(id));
            return order;
        }

        public async Task AddOrderAsync(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }

        public async Task RemoveOrder(Order order)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }

        public async Task<Order> GetLastOrderAsync()
        {
            var lastOrder = await context.Orders.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            return lastOrder;
        }
    }
}
