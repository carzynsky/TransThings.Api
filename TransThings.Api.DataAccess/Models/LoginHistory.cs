using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TransThings.Api.DataAccess.Models
{
    [Table("LoginHistories")]
    public class LoginHistory
    {
        [Key]
        public int Id { get; set; }
        public DateTime AttemptDate { get; set; }
        public bool IsSuccessful { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
