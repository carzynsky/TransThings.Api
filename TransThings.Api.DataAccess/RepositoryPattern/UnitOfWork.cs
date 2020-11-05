using System;
using System.Collections.Generic;
using System.Text;
using TransThings.Api.DataAccess.Repositories;

namespace TransThings.Api.DataAccess.RepositoryPattern
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransThingsDbContext context;
        private ClientRepository clientRepository;
        private ConfigurationRepository configurationRepository;
        private DriverRepository driverRepository;
        private UserRepository userRepository;
        private EventRepository eventRepository;
        private ForwardingOrderRepository forwardingOrderRepository;
        private LoadRepository loadRepository;
        private LoginHistoryRepository loginHistoryRepository;
        private OrderRepository orderRepository;
        private OrderStatusRepository orderStatusRepository;
        private PaymentFormRepository paymentFormRepository;
        private TransitForwardingOrderRepository transitForwardingOrderRepository;
        private TransitRepository transitRepository;
        private TransporterRepository transporterRepository;
        private UserRoleRepository userRoleRepository;
        private VehicleRepository vehicleRepository;
        private VehicleTypeRepository vehicleTypeRepository;
        private WarehouseRepository warehouseRepository;

        public UnitOfWork(TransThingsDbContext context)
        {
            this.context = context;
        }

        public UserRepository UserRepository
        {
            get { return userRepository ??= new UserRepository(context); }
        }

        public ClientRepository ClientRepository
        {
            get { return clientRepository ??= new ClientRepository(context); }
        }

        public ConfigurationRepository ConfigurationRepository
        {
            get { return configurationRepository ??= new ConfigurationRepository(context); }
        }


        public DriverRepository DriverRepository
        {
            get { return driverRepository ??= new DriverRepository(context); }
        }


        public EventRepository EventRepository
        {
            get { return eventRepository ??= new EventRepository(context); }
        }


        public ForwardingOrderRepository ForwardingOrderRepository
        {
            get { return forwardingOrderRepository ??= new ForwardingOrderRepository(context); }
        }

        public LoadRepository LoadRepository
        {
            get { return loadRepository ??= new LoadRepository(context); }
        }

        public LoginHistoryRepository LoginHistoryRepository
        {
            get { return loginHistoryRepository ??= new LoginHistoryRepository(context); }
        }

        public OrderRepository OrderRepository
        {
            get { return orderRepository ??= new OrderRepository(context); }
        }

        public OrderStatusRepository OrderStatusRepository
        {
            get { return orderStatusRepository ??= new OrderStatusRepository(context); }
        }


        public PaymentFormRepository PaymentFormRepository
        {
            get { return paymentFormRepository ??= new PaymentFormRepository(context); }
        }

        public TransitForwardingOrderRepository TransitForwardingOrderRepository
        {
            get { return transitForwardingOrderRepository ??= new TransitForwardingOrderRepository(context); }
        }

        public TransitRepository TransitRepository
        {
            get { return transitRepository ??= new TransitRepository(context); }
        }

        public TransporterRepository TransporterRepository
        {
            get { return transporterRepository ??= new TransporterRepository(context); }
        }

        public UserRoleRepository UserRoleRepository
        {
            get
            { return userRoleRepository ??= new UserRoleRepository(context); }
        }


        public VehicleRepository VehicleRepository
        {
            get { return vehicleRepository ??= new VehicleRepository(context); }
        }


        public VehicleTypeRepository VehicleTypeRepository
        {
            get { return vehicleTypeRepository ??= new VehicleTypeRepository(context); }
        }

        public WarehouseRepository WarehouseRepository
        {
            get { return warehouseRepository ??= new WarehouseRepository(context); }
        }
    }
}
