using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransThings.Api.DataAccess.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [StringLength(40)]
        [Required]
        public string Brand { get; set; }

        [StringLength(80)]
        [Required]
        public string Model { get; set; }

        public decimal LoadingCapacity { get; set; }

        [StringLength(4)]
        public string ProductionYear { get; set; }

        [StringLength(40)]
        public string Trailer { get; set; }

        public int TransporterId { get; set; }

        public int VehicleTypeId { get; set; }

        [ForeignKey("TransporterId")]
        public virtual Transporter Transporter { get; set; }

        [ForeignKey("VehicleTypeId")]
        public virtual VehicleType VehicleType { get; set; }
    }
}
