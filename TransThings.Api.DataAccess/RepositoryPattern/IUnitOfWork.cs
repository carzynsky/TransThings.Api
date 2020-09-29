using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.Repositories;

namespace TransThings.Api.DataAccess.RepositoryPattern
{
    public interface IUnitOfWork
    {
        ClientRepository ClientRepository { get; }
        ConfigurationRepository ConfigurationRepository { get; }
        DriverRepository DriverRepository { get; }
        UserRepository UserRepository { get; }
        EventRepository EventRepository { get; }
        ForwardingOrderRepository ForwardingOrderRepository { get; }
        LoadRepository LoadRepository { get; }
        LoginHistoryRepository LoginHistoryRepository { get; }
        OrderRepository OrderRepository { get; }
        OrderStatusRepository OrderStatusRepository { get; }
        PaymentFormRepository PaymentFormRepository { get; }
        TransitForwardingOrderRepository TransitForwardingOrderRepository { get; }
        TransitRepository TransitRepository { get; }
        TransporterRepository TransporterRepository { get; }
        UserRoleRepository UserRoleRepository { get; }
        VehicleRepository VehicleRepository { get; }
        VehicleTypeRepository VehicleTypeRepository { get; }
        WarehouseRepository WarehouseRepository { get; }
    }
}
