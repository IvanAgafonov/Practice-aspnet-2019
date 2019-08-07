using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store_Rating_System_Dev.Models
{
    public enum Statuses
    {
        Undefined=0,
        Rejected=-1,
        Accepted=1
    }

    public class Rating
    {
        [Key]
        public string ID                    { get; set; }

        public string User_ID               { get; set; }

        [ForeignKey("User_ID")]
        public User User                    { get; set; }

        public string Store_ID              { get; set; }

        [ForeignKey("Store_ID")]
        public Store Store                  { get; set; }

        [MaxLength(500)]
        public string Comment               { get; set; } 

        public bool Rate_value              { get; set; }

        public Statuses? Rate_Status        { get; set; }

        public DateTime Date_of_publication { get; set; }
    }
}
