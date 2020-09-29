using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class OrderStatusRepository
    {
        private readonly TransThingsDbContext context;

        public OrderStatusRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<OrderStatus>> GetAllOrderStatusesAsync()
        {
            var orderStatuses = await context.OrderStatuses.ToListAsync();
            return orderStatuses;
        }

        public async Task<OrderStatus> GetOrderStatusByNameAsync(string orderStatusName)
        {
            var orderStatus = await context.OrderStatuses.SingleOrDefaultAsync(x => x.StatusName.ToLower().Equals(orderStatusName.ToLower()));
            return orderStatus;
        }

        public async Task<OrderStatus> GetOrderStatusByIdAsync(int id)
        {
            var orderStatus = await context.OrderStatuses.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return orderStatus;
        }

        public async Task AddOrderStatusAsync(OrderStatus orderStatus)
        {
            await context.OrderStatuses.AddAsync(orderStatus);
            await context.SaveChangesAsync();
        }

        public async Task UpdateOrderStatus(OrderStatus orderStatus)
        {
            context.OrderStatuses.Update(orderStatus);
            await context.SaveChangesAsync();
        }

        public async Task RemoveOrderStatus(OrderStatus orderStatus)
        {
            context.OrderStatuses.Remove(orderStatus);
            await context.SaveChangesAsync();
        }
    }
}
