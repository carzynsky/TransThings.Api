using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransThings.Api.DataAccess.Models
{
    [Table("Drivers")]
    public class Driver
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(255)]
        [Required]
        public string LastName { get; set; }

        [StringLength(11)]
        [Required]
        public string PeselNumber { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public char Gender { get; set; }

        [StringLength(40)]
        public string ContactPhoneNumber { get; set; }

        [StringLength(255)]
        public string Mail { get; set; }

        public int TransporterId { get; set; }

        [ForeignKey("TransporterId")]
        public virtual Transporter Transporter { get; set; }
    }
}
