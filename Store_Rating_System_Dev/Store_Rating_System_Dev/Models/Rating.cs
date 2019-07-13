using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Rating_System_Dev.Models
{
    public class Rating
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public int Moderator_ID { get; set; }
        public int Store_ID { get; set; }
        public string comment { get; set; } 
        public string status { get; set; }
        public DateTime date_of_publication { get; set; }

    }
}
