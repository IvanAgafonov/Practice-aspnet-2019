using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Store_Rating_System_Dev.Models
{
    public enum Countries
    {
        Russia, America, Germany, Belarus
    }

    public enum Cities
    {
        Moscow, Saint_Petersburg, Saratov,
        Minsk, Grodno, Mogilyov,
        New_York, San_Francisco, Chicago,
        Berlin, Munich, Hamburg
    }

    public class User : IdentityUser
    {
        public Cities City { get; set; }
        public Countries Country { get; set; }
        public byte[] Image { get; set; }
    }
}
