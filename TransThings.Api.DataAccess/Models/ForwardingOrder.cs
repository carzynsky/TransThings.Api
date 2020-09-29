using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Management.Automation;

namespace TransThings.Api.DataAccess.Models
{
    [Table("ForwardingOrders")]
    public class ForwardingOrder
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string ForwardingOrderNumber { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [StringLength(512)]
        [AllowNull]
        public string AdditionalDescription { get; set; }

        public int ForwarderId { get; set; }

        [ForeignKey("ForwarderId")]
        public virtual User Forwarder { get; set; }
    }
}
