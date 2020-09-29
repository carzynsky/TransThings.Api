using Microsoft.EntityFrameworkCore;
using TransThings.Api.DataAccess.Constants;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess
{
    public class TransThingsDbContext : DbContext
    {
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<ForwardingOrder> ForwardingOrders { get; set; }
        public virtual DbSet<Load> Loads { get; set; }
        public virtual DbSet<LoginHistory> LoginHistories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<PaymentForm> PaymentForms { get; set; }
        public virtual DbSet<Transit> Transits { get; set; }
        public virtual DbSet<TransitForwardingOrder> TransitForwardingOrders { get; set; }
        public virtual DbSet<Transporter> Transporters { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server={ConnectionConfiguration.ServerName};Database={ConnectionConfiguration.Database};Trusted_Connection={ConnectionConfiguration.TrustedConnection};", b => b.MigrationsAssembly("TransThings.Api"));
        }
    }
}
