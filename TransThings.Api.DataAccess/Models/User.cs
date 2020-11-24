using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransThings.Api.DataAccess.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(255)]
        [Required]
        public string LastName { get; set; }

        [Required]
        public char Gender { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [StringLength(11)]
        [Required]
        public string PeselNumber { get; set; }

        public DateTime? DateOfEmployment { get; set; }

        [StringLength(255)]
        [Required]
        public string Login { get; set; }

        [StringLength(255)]
        [Required]
        public string Password { get; set; }

        [StringLength(255)]
        public string Mail { get; set; }

        [StringLength(40)]
        public string PhoneNumber { get; set; }

        public int UserRoleId { get; set; }

        [ForeignKey("UserRoleId")]
        public virtual UserRole UserRole { get; set; }
    }
}
