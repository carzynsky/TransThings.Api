using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransThings.Api.DataAccess.Models
{
    [Table("Clients")]
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string CompanyFullName { get; set; }

        [StringLength(255)]
        public string CompanyShortName { get; set; }

        [StringLength(255)]
        [Required]
        public string ClientFirstName { get; set; }

        [StringLength(255)]
        [Required]
        public string ClientLastName { get; set; }

        [Required]
        public char Gender { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [StringLength(11)]
        [Required]
        public string ClientPeselNumber{ get; set; }


        [StringLength(255)]
        public string StreetName { get; set; }

        [StringLength(40)]
        public string NIP { get; set; }

        [StringLength(30)]
        public string ZipCode { get; set; }

        [StringLength(255)]
        public string City { get; set; }

        [StringLength(255)]
        public string Country { get; set; }

        [StringLength(40)]
        public string ContactPhoneNumber1 { get; set; }

        [StringLength(40)]
        public string ContactPhoneNumber2 { get; set; }

        [StringLength(40)]
        public string BuildingNumber { get; set; }

        [StringLength(40)]
        public string ApartmentNumber { get; set; }
    }
}
