using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TransThings.Api.DataAccess.Models
{
    [Table("Events")]
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [StringLength(80)]
        [Required]
        public string EventName { get; set; }

        public DateTime EventStartTime { get; set; }

        public DateTime EventEndTime { get; set; }

        [StringLength(255)]
        [Required]
        public string ContactPersonFirstName { get; set; }

        [StringLength(255)]
        [Required]
        public string ContactPersonLastName { get; set; }

        [StringLength(40)]
        [Required]
        public string ContactPersonPhoneNumber { get; set; }

        [StringLength(80)]
        public string EventPlace { get; set; }

        [StringLength(100)]
        public string EventStreetAddress { get; set; }

        [StringLength(512)]
        public string OtherInformation { get; set; }

    }
}
