using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Store_Rating_System_Dev.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("ID")]
        public Account Account { get; set; }

        public int? Moderator_ID { get; set; }

        [ForeignKey("Moderator_ID")]
        public Moderator Moderator { get; set; }

        [MaxLength(50)]
        public string first_name { get; set; }

        [MaxLength(50)]
        public string last_name { get; set; }

        public string phone_number { get; set; }

        public DateTime date_of_birth { get; set; }

        [MaxLength(50)]
        [EmailAddressAttribute]
        public string email { get; set; }

        public bool? status { get; set; }  

        public DateTime date_of_registration { get; set; }

        public bool gender { get; set; }

        [MaxLength(50)]
        public string country { get; set; }

        [MaxLength(50)]
        public string city { get; set; }

        // public int photo { get; set; }
    }
}
