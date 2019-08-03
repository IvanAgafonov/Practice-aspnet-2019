using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Store_Rating_System_Dev.Models
{
    public class UserCreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public Cities City { get; set; }

        public Countries Country { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }

    public class UserLoginModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }

    public class Store_with_Ratings
    {
        public Store store { get; set; }
        public IQueryable<Rating> ratings { get; set; }
    }
}
