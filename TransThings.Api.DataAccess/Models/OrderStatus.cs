using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransThings.Api.DataAccess.Models
{
    [Table("OrderStatuses")]
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string StatusName { get; set; }
    }
}
