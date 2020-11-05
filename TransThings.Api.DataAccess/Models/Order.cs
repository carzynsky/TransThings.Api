using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Management.Automation;
using System.Text;

namespace TransThings.Api.DataAccess.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string? OrderNumber { get; set; }
        public DateTime? OrderCreationDate { get; set; }
        public DateTime? OrderExpectedDate { get; set; }
        public DateTime? OrderRealizationDate { get; set; }
        public decimal? NetPrice { get; set; }
        public decimal? GrossPrice { get; set; }
        public decimal? TotalNetWeight { get; set; }
        public decimal? TotalGrossWeight { get; set; }
        public decimal? TotalVolume { get; set; }
        public decimal? TransportDistance { get; set; }
        public bool? IsClientVerified { get; set; }
        public bool? IsAvailableAtWarehouse { get; set; }

        [StringLength(255)]
        public string DestinationStreetAddress { get; set; }

        [StringLength(255)]
        public string DestinationCity { get; set; }

        [StringLength(30)]
        public string DestinationZipCode { get; set; }

        [StringLength(255)]
        public string DestinationCountry { get; set; }

        [StringLength(512)]
        public string CustomerAddtionalInstructions { get; set; }

        public int ClientId { get; set; }
        public int? VehicleTypeId { get; set; }
        public int OrdererId { get; set; }
        public int OrderStatusId { get; set; }
        public int PaymentFormId { get; set; }
        public int WarehouseId { get; set; }
        public int? ConsultantId { get; set; }
        public int? ForwardingOrderId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }

        [ForeignKey("VehicleTypeId")]
        public virtual VehicleType VehicleType { get; set; }

        [ForeignKey("OrdererId")]
        public virtual User Orderer { get; set; }

        [ForeignKey("OrderStatusId")]
        public virtual OrderStatus OrderStatus { get; set; }

        [ForeignKey("PaymentFormId")]
        public virtual PaymentForm PaymentForm { get; set; }

        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }

        [AllowNull]
        [ForeignKey("ConsultantId")]
        public virtual User Consultant { get; set; }

        [AllowNull]
        [ForeignKey("ForwardingOrderId")]
        public virtual ForwardingOrder ForwardingOrder { get; set; }
    }
}
