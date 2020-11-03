using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransThings.Api.Migrations
{
    public partial class ttMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyFullName = table.Column<string>(maxLength: 255, nullable: true),
                    CompanyShortName = table.Column<string>(maxLength: 255, nullable: true),
                    ClientFirstName = table.Column<string>(maxLength: 255, nullable: false),
                    ClientLastName = table.Column<string>(maxLength: 255, nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    ClientPeselNumber = table.Column<string>(maxLength: 11, nullable: false),
                    StreetName = table.Column<string>(maxLength: 255, nullable: true),
                    NIP = table.Column<string>(maxLength: 40, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 30, nullable: true),
                    City = table.Column<string>(maxLength: 255, nullable: true),
                    Country = table.Column<string>(maxLength: 255, nullable: true),
                    ContactPhoneNumber1 = table.Column<string>(maxLength: 40, nullable: true),
                    ContactPhoneNumber2 = table.Column<string>(maxLength: 40, nullable: true),
                    BuildingNumber = table.Column<string>(maxLength: 40, nullable: true),
                    ApartmentNumber = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Value = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentForms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transporters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 255, nullable: true),
                    ShortName = table.Column<string>(maxLength: 255, nullable: true),
                    NIP = table.Column<string>(maxLength: 40, nullable: true),
                    StreetAddress = table.Column<string>(maxLength: 255, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 30, nullable: true),
                    City = table.Column<string>(maxLength: 255, nullable: true),
                    Country = table.Column<string>(maxLength: 255, nullable: true),
                    Mail = table.Column<string>(maxLength: 255, nullable: true),
                    SupportedPathsDescriptions = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transporters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    StreetAddress = table.Column<string>(maxLength: 255, nullable: false),
                    ZipCode = table.Column<string>(maxLength: 30, nullable: false),
                    City = table.Column<string>(maxLength: 255, nullable: false),
                    ContactPhoneNumber = table.Column<string>(maxLength: 40, nullable: true),
                    ContactPersonFirstName = table.Column<string>(maxLength: 255, nullable: true),
                    ContactPersonLastName = table.Column<string>(maxLength: 255, nullable: true),
                    Mail = table.Column<string>(maxLength: 255, nullable: true),
                    Fax = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteShortPath = table.Column<string>(maxLength: 100, nullable: true),
                    NetPrice = table.Column<decimal>(nullable: false),
                    GrossPrice = table.Column<decimal>(nullable: false),
                    TransitSourceStreetAddress = table.Column<string>(maxLength: 255, nullable: true),
                    TransitSourceZipCode = table.Column<string>(maxLength: 30, nullable: true),
                    TransitSourceCity = table.Column<string>(maxLength: 255, nullable: true),
                    TransitSourceCountry = table.Column<string>(maxLength: 255, nullable: true),
                    TransitDestinationStreetAddress = table.Column<string>(maxLength: 255, nullable: true),
                    TransitDestinationZipCode = table.Column<string>(maxLength: 255, nullable: true),
                    TransitDestinationCity = table.Column<string>(maxLength: 255, nullable: true),
                    TransitDestinationCountry = table.Column<string>(maxLength: 255, nullable: true),
                    PaymentFormId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transits_PaymentForms_PaymentFormId",
                        column: x => x.PaymentFormId,
                        principalTable: "PaymentForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    PeselNumber = table.Column<string>(maxLength: 11, nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    ContactPhoneNumber = table.Column<string>(maxLength: 40, nullable: true),
                    Mail = table.Column<string>(maxLength: 255, nullable: true),
                    TransporterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Transporters_TransporterId",
                        column: x => x.TransporterId,
                        principalTable: "Transporters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    PeselNumber = table.Column<string>(maxLength: 11, nullable: false),
                    DateOfEmployment = table.Column<DateTime>(nullable: true),
                    Login = table.Column<string>(maxLength: 255, nullable: false),
                    Password = table.Column<string>(maxLength: 255, nullable: false),
                    Mail = table.Column<string>(maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 40, nullable: true),
                    UserRoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(maxLength: 40, nullable: true),
                    Model = table.Column<string>(maxLength: 80, nullable: true),
                    LoadingCapacity = table.Column<decimal>(nullable: false),
                    ProductionYear = table.Column<string>(maxLength: 4, nullable: true),
                    Trailer = table.Column<string>(maxLength: 40, nullable: true),
                    TransporterId = table.Column<int>(nullable: false),
                    VehicleTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Transporters_TransporterId",
                        column: x => x.TransporterId,
                        principalTable: "Transporters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForwardingOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ForwardingOrderNumber = table.Column<string>(maxLength: 255, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    AdditionalDescription = table.Column<string>(maxLength: 512, nullable: true),
                    ForwarderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForwardingOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForwardingOrders_Users_ForwarderId",
                        column: x => x.ForwarderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoginHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttemptDate = table.Column<DateTime>(nullable: false),
                    IsSuccessful = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(maxLength: 80, nullable: false),
                    EventStartTime = table.Column<DateTime>(nullable: false),
                    EventEndTime = table.Column<DateTime>(nullable: false),
                    ContactPersonFirstName = table.Column<string>(maxLength: 255, nullable: false),
                    ContactPersonLastName = table.Column<string>(maxLength: 255, nullable: false),
                    ContactPersonPhoneNumber = table.Column<string>(maxLength: 40, nullable: false),
                    EventPlace = table.Column<string>(maxLength: 80, nullable: true),
                    EventStreetAddress = table.Column<string>(maxLength: 100, nullable: true),
                    OtherInformation = table.Column<string>(maxLength: 512, nullable: true),
                    ForwardingOrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_ForwardingOrders_ForwardingOrderId",
                        column: x => x.ForwardingOrderId,
                        principalTable: "ForwardingOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(maxLength: 255, nullable: false),
                    OrderCreationDate = table.Column<DateTime>(nullable: false),
                    OrderExpectedDate = table.Column<DateTime>(nullable: true),
                    OrderRealizationDate = table.Column<DateTime>(nullable: true),
                    NetPrice = table.Column<decimal>(nullable: true),
                    GrossPrice = table.Column<decimal>(nullable: true),
                    TotalNetWeight = table.Column<decimal>(nullable: true),
                    TotalGrossWeight = table.Column<decimal>(nullable: true),
                    TotalVolume = table.Column<decimal>(nullable: true),
                    TransportDistance = table.Column<decimal>(nullable: true),
                    IsClientVerified = table.Column<bool>(nullable: true),
                    IsAvailableAtWarehouse = table.Column<bool>(nullable: true),
                    DestinationStreetAddress = table.Column<string>(maxLength: 255, nullable: true),
                    DestinationCity = table.Column<string>(maxLength: 255, nullable: true),
                    DestinationZipCode = table.Column<string>(maxLength: 30, nullable: true),
                    DestinationCountry = table.Column<string>(maxLength: 255, nullable: true),
                    CustomerAddtionalInstructions = table.Column<string>(maxLength: 512, nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    VehicleTypeId = table.Column<int>(nullable: true),
                    OrdererId = table.Column<int>(nullable: false),
                    OrderStatusId = table.Column<int>(nullable: false),
                    PaymentFormId = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    ConsultantId = table.Column<int>(nullable: true),
                    ForwardingOrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_ForwardingOrders_ForwardingOrderId",
                        column: x => x.ForwardingOrderId,
                        principalTable: "ForwardingOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_OrdererId",
                        column: x => x.OrdererId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentForms_PaymentFormId",
                        column: x => x.PaymentFormId,
                        principalTable: "PaymentForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transits_ForwardingOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransitId = table.Column<int>(nullable: false),
                    ForwardingOrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transits_ForwardingOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transits_ForwardingOrders_ForwardingOrders_ForwardingOrderId",
                        column: x => x.ForwardingOrderId,
                        principalTable: "ForwardingOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transits_ForwardingOrders_Transits_TransitId",
                        column: x => x.TransitId,
                        principalTable: "Transits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    PackageType = table.Column<string>(maxLength: 255, nullable: true),
                    NetWeight = table.Column<decimal>(nullable: false),
                    GrossWeight = table.Column<decimal>(nullable: false),
                    Volume = table.Column<decimal>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loads_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_TransporterId",
                table: "Drivers",
                column: "TransporterId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ForwardingOrderId",
                table: "Events",
                column: "ForwardingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ForwardingOrders_ForwarderId",
                table: "ForwardingOrders",
                column: "ForwarderId");

            migrationBuilder.CreateIndex(
                name: "IX_Loads_OrderId",
                table: "Loads",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginHistories_UserId",
                table: "LoginHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ConsultantId",
                table: "Orders",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ForwardingOrderId",
                table: "Orders",
                column: "ForwardingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrdererId",
                table: "Orders",
                column: "OrdererId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentFormId",
                table: "Orders",
                column: "PaymentFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VehicleTypeId",
                table: "Orders",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WarehouseId",
                table: "Orders",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Transits_PaymentFormId",
                table: "Transits",
                column: "PaymentFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Transits_ForwardingOrders_ForwardingOrderId",
                table: "Transits_ForwardingOrders",
                column: "ForwardingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Transits_ForwardingOrders_TransitId",
                table: "Transits_ForwardingOrders",
                column: "TransitId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_TransporterId",
                table: "Vehicles",
                column: "TransporterId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleTypeId",
                table: "Vehicles",
                column: "VehicleTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Loads");

            migrationBuilder.DropTable(
                name: "LoginHistories");

            migrationBuilder.DropTable(
                name: "Transits_ForwardingOrders");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Transits");

            migrationBuilder.DropTable(
                name: "Transporters");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ForwardingOrders");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "VehicleTypes");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "PaymentForms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
