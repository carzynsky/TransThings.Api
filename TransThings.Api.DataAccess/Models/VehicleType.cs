using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransThings.Api.DataAccess.Models
{
    [Table("VehicleTypes")]
    public class VehicleType
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string TypeName { get; set; }
    }
}
