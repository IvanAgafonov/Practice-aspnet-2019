using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace Store_Rating_System_Dev.Models
{
    public class Account
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string login { get; set; }

        [MaxLength(50)]
        public string password { get; set; } 

    }
}
