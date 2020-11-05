using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Management.Automation;

namespace TransThings.Api.DataAccess.Models
{
    [Table("Loads")]
    public class Load
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        public decimal Weight { get; set; }

        public int Amount { get; set; }

        [StringLength(255)]
        public string PackageType { get; set; }

        public decimal NetWeight { get; set; }

        public decimal GrossWeight { get; set; }

        [AllowNull]
        public decimal Volume { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
