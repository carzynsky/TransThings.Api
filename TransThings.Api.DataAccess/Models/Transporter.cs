using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransThings.Api.DataAccess.Models
{
    [Table("Transporters")]
    public class Transporter
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string FullName { get; set; }

        [StringLength(255)]
        public string ShortName { get; set; }

        [StringLength(40)]
        public string NIP { get; set; }

        [StringLength(255)]
        public string StreetAddress { get; set; }

        [StringLength(30)]
        public string ZipCode { get; set; }

        [StringLength(255)]
        public string City { get; set; }

        [StringLength(255)]
        public string Country { get; set; }

        [StringLength(255)]
        public string Mail { get; set; }

        [StringLength(255)]
        public string SupportedPathsDescriptions { get; set; }
    }
}
