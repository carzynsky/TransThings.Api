using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransThings.Api.DataAccess.Models
{
    [Table("Events")]
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [StringLength(80)]
        public string EventName { get; set; }

        public DateTime? EventStartTime { get; set; }

        public DateTime? EventEndTime { get; set; }

        [StringLength(255)]
        public string ContactPersonFirstName { get; set; }

        [StringLength(255)]
        public string ContactPersonLastName { get; set; }

        [StringLength(40)]
        public string ContactPersonPhoneNumber { get; set; }

        [StringLength(80)]
        public string EventPlace { get; set; }

        [StringLength(100)]
        public string EventStreetAddress { get; set; }

        public int ForwardingOrderId{ get; set; }

        [ForeignKey("ForwardingOrderId")]
        public virtual ForwardingOrder ForwardingOrder { get; set; }
    }
}
