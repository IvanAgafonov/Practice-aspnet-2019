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

    public class Store_with_Ratings_Users
    {
        public Store store { get; set; }
        public Dictionary<User ,Rating> dict_user_rating { get; set; }
    }

    public class Rate_data
    {
        public string store_id { get; set; }
        public string comment { get; set; }
        public bool rate_value { get; set; }
    }

    // def category -1 is for selected Any Category in View
    public class Store_search
    {
        public string name { get; set; }
        public int category { get; set; } = -1;
        public string error_mes { get; set; }
    }
}
