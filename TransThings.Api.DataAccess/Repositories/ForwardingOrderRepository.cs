using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class ForwardingOrderRepository
    {
        private readonly TransThingsDbContext context;

        public ForwardingOrderRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ForwardingOrder>> GetAllForwardingOrdersAsync()
        {
            var forwardingOrders = await context.ForwardingOrders.ToListAsync();
            return forwardingOrders;
        }

        public async Task<List<ForwardingOrder>> GetForwardingOrdersByForwarderAsync(int forwarderId)
        {
            var forwardingOrders = await context.ForwardingOrders.Where(x => x.ForwarderId.Equals(forwarderId)).ToListAsync();
            return forwardingOrders;
        }

        public async Task<ForwardingOrder> GetForwardingOrderByIdAsync(int id)
        {
            var forwardingOrder = await context.ForwardingOrders.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return forwardingOrder;
        }

        public async Task AddForwardingOrderAsync(ForwardingOrder forwardingOrder)
        {
            await context.ForwardingOrders.AddAsync(forwardingOrder);
            await context.SaveChangesAsync();
        }

        public async Task UpdateForwardingOrder(ForwardingOrder forwardingOrder)
        {
            context.ForwardingOrders.Update(forwardingOrder);
            await context.SaveChangesAsync();
        }

        public async Task RemoveForwardingOrder(ForwardingOrder forwardingOrder)
        {
            context.ForwardingOrders.Remove(forwardingOrder);
            await context.SaveChangesAsync();
        }

        public async Task<ForwardingOrder> GetLastForwardingOrderAsync()
        {
            var lastForwardingOrder = await context.ForwardingOrders.OrderByDescending(x => x.Id).SingleOrDefaultAsync();
            return lastForwardingOrder;
        }
    }
}
