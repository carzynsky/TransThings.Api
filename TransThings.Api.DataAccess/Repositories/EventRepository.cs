using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class EventRepository
    {
        private readonly TransThingsDbContext context;

        public EventRepository(TransThingsDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Event>> GetAllEventsAsync()
        {
            var _events = await context.Events.ToListAsync();
            return _events;
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            var _event = await context.Events.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return _event;
        }
        
        public async Task<List<Event>> GetEventsByForwardingOrderAsync(int forwardingOrderId)
        {
            var _event = await context.Events.Where(x => x.ForwardingOrderId.Equals(forwardingOrderId)).ToListAsync();
            return _event;
        }

        public async Task AddEventAsync(Event _event)
        {
            await context.Events.AddAsync(_event);
            await context.SaveChangesAsync();
        }

        public async Task UpdateEvent(Event _event)
        {
            context.Events.Update(_event);
            await context.SaveChangesAsync();
        }

        public async Task RemoveEvent(Event _event)
        {
            context.Events.Remove(_event);
            await context.SaveChangesAsync();
        }
    }
}
