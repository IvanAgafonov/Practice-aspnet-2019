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
        public string comment { get; set; }  // MB another essence?
        public string status { get; set; }  // MB enum or Class instence?
        public int publication_year { get; set; } // MB all in one DataTime???
        public int publication_month { get; set; } // Or String or enum or instance?
        public int publication_day { get; set; } // same as top line
        public int publication_hour { get; set; } // same
        public int publication_minute { get; set; }  //s

    }
}
