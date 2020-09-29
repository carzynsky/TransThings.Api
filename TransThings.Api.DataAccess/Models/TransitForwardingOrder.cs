using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransThings.Api.DataAccess.Models
{
    [Table("Transits_ForwardingOrders")]
    public class TransitForwardingOrder
    {
        [Key]
        public int Id { get; set; }
        public int TransitId { get; set; }
        public int ForwardingOrderId{ get; set; }

        [ForeignKey("TransitId")]
        public virtual Transit Transit { get; set; }

        [ForeignKey("ForwardingOrderId")]
        public virtual ForwardingOrder ForwardingOrder { get; set; }
    }
}
