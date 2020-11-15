using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class TransitForwardingOrderRepository
    {
        private readonly TransThingsDbContext context;

        public TransitForwardingOrderRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<TransitForwardingOrder>> GetAllTransitForwardingOrdersAsync()
        {
            var transitForwardingOrders = await context.TransitForwardingOrders.Include(x => x.ForwardingOrder).Include(x => x.Transit).ToListAsync();
            return transitForwardingOrders;
        }

        public async Task<List<TransitForwardingOrder>> GetTransitForwardingOrdersByTransitAsync(int transitId)
        {
            var transitForwardingOrders = await context.TransitForwardingOrders.Where(x => x.TransitId.Equals(transitId)).
                Include(x => x.ForwardingOrder).Include(x => x.Transit).ToListAsync();
            return transitForwardingOrders;
        }

        public async Task<List<TransitForwardingOrder>> GetTransitForwardingOrdersByForwardingOrderAsync(int forwardingOrderId)
        {
            var transitForwardingOrders = await context.TransitForwardingOrders.Where(x => x.ForwardingOrderId.Equals(forwardingOrderId)).ToListAsync();
            return transitForwardingOrders;
        }

        public async Task<TransitForwardingOrder> GetTransitForwardingOrderByTransitAndForwardingOrderAsync(int transitId, int forwardingId)
        {
            var transitForwardingOrder = await context.TransitForwardingOrders
                .FirstOrDefaultAsync(x => x.TransitId.Equals(transitId) && x.ForwardingOrderId.Equals(forwardingId));
            return transitForwardingOrder;
        }

        public async Task<TransitForwardingOrder> GetTransitForwardingOrderByIdAsync(int id)
        {
            var transitForwardingOrder = await context.TransitForwardingOrders.Include(x => x.ForwardingOrder).Include(x => x.Transit).SingleOrDefaultAsync(x => x.Id.Equals(id));
            return transitForwardingOrder;
        }

        public async Task AddTransitForwardingOrderAsync(TransitForwardingOrder transitForwardingOrder)
        {
            await context.TransitForwardingOrders.AddAsync(transitForwardingOrder);
            await context.SaveChangesAsync();
        }

        public async Task UpdateTransitForwardingOrders(List<TransitForwardingOrder> transitForwardingOrders)
        {
            context.TransitForwardingOrders.UpdateRange(transitForwardingOrders);
            await context.SaveChangesAsync();
        }

        public async Task RemoveTransitForwardingOrder(TransitForwardingOrder transitForwardingOrder)
        {
            context.TransitForwardingOrders.Remove(transitForwardingOrder);
            await context.SaveChangesAsync();
        }
    }
}
