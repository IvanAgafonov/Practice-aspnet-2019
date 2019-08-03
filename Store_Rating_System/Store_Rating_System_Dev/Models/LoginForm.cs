using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Rating_System_Dev.Models
{
    public class LoginForm
    {
        [Required]
        [UIHint("email")]
        public string name { get; set; }

        [Required]
        [UIHint("password")]
        public string password { get; set; }
    }
}
