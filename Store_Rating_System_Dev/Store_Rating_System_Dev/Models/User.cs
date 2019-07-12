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
        public int phone_number { get; set; } // DataTime?
        public int year_of_birth { get; set; }
        public int month_of_birth { get; set; }
        public int day_of_birth { get; set; }
        public string email { get; set; }
        public int status { get; set; }  // enum or instance or... ????
        public int year_of_registration { get; set; }  // DataTime?
        public int month_of_registration { get; set; }
        public int day_of_registartion { get; set; }
        public bool gender { get; set; }
        public int country { get; set; } // enum or ...??
        public int city { get; set; }
        public int district { get; set; }
        public int photo { get; set; }
    }
}
