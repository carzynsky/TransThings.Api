using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransThings.Api.DataAccess.Models
{
    [Table("Warehouses")]
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        [Required]
        public string StreetAddress { get; set; }

        [StringLength(30)]
        [Required]
        public string ZipCode { get; set; }

        [StringLength(255)]
        [Required]
        public string City { get; set; }

        [StringLength(40)]
        public string ContactPhoneNumber { get; set; }

        [StringLength(255)]
        public string ContactPersonFirstName { get; set; }

        [StringLength(255)]
        public string ContactPersonLastName { get; set; }

        [StringLength(255)]
        public string Mail { get; set; }

        [StringLength(255)]
        public string Fax { get; set; }
    }
}
