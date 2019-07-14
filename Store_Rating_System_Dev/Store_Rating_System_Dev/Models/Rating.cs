using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Rating_System_Dev.Models
{
    public class Rating
    {
        [Key]
        public int ID { get; set; }

        public int User_ID { get; set; }

        [ForeignKey("User_ID")]
        public User User { get; set; }

        public int Moderator_ID { get; set; }

        [ForeignKey("Moderator_ID")]
        public Moderator Moderator { get; set; }

        public int Store_ID { get; set; }

        [ForeignKey("Store_ID")]
        public Store Store { get; set; }

        [MaxLength(50)]
        public string comment { get; set; } 

        public bool? status { get; set; }

        public DateTime date_of_publication { get; set; }
    }
}
