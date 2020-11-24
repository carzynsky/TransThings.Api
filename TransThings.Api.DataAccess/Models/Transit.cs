using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransThings.Api.DataAccess.Models
{
    [Table("Transits")]
    public class Transit
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string RouteShortPath { get; set; }
        public decimal NetPrice { get; set; }
        public decimal GrossPrice { get; set; }
        public decimal TransportDistance { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [StringLength(255)]
        public string TransitSourceStreetAddress { get; set; }

        [StringLength(30)]
        public string TransitSourceZipCode { get; set; }

        [StringLength(255)]
        public string TransitSourceCity { get; set; }

        [StringLength(255)]
        public string TransitSourceCountry { get; set; }

        [StringLength(255)]
        public string TransitDestinationStreetAddress { get; set; }

        [StringLength(255)]
        public string TransitDestinationZipCode { get; set; }

        [StringLength(255)]
        public string TransitDestinationCity { get; set; }

        [StringLength(255)]
        public string TransitDestinationCountry { get; set; }

        public int PaymentFormId { get; set; }
        public int TransporterId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }

        [ForeignKey("PaymentFormId")]
        public virtual PaymentForm PaymentForm { get; set; }

        [ForeignKey("TransporterId")]
        public virtual Transporter Transporter { get; set; }

        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; }

        [ForeignKey("DriverId")]
        public virtual Driver Driver { get; set; }
    }
}
