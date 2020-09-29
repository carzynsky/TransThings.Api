using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

        [ForeignKey("PaymentFormId")]
        [Required]
        public virtual PaymentForm PaymentForm { get; set; }
    }
}
