using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransThings.Api.DataAccess.Models
{
    [Table("PaymentForms")]
    public class PaymentForm
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string PaymentName { get; set; }
    }
}
