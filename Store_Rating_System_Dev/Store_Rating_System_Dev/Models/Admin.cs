using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Rating_System_Dev.Models
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("ID")]
        public Account Account { get; set; }

        [MaxLength(50)]
        public string first_name { get; set; }

        [MaxLength(50)]
        public string last_name { get; set; }

        [MaxLength(50)]
        public string patronymic { get; set; }

        [MaxLength(50)]
        public string email { get; set; }
    }
}
