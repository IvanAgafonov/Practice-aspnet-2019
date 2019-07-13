using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Rating_System_Dev.Models
{
    public class User
    {
        public int ID { get; set; }
        public int Moderator_ID { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string patronymic { get; set; }
        public int? phone_number { get; set; }
        public DateTime date_of_birth { get; set; }
        public string email { get; set; }
        public bool? status { get; set; }  
        public DateTime date_of_registarion { get; set; }
        public bool gender { get; set; }
        public string country { get; set; } 
        public string city { get; set; }
        // public int photo { get; set; }
    }
}
