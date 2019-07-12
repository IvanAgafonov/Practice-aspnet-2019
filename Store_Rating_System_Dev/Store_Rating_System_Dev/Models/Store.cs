using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Store_Rating_System_Dev.Models
{
    public class Store
    {
        public int ID                       { get; set; }
        public string name                  { get; set; }
        public string url                   { get; set; }
        public string name_category         { get; set; } // ?????????????????? Are this string or instance of Category Class or enum?
        public int number_of_ratings        { get; set; }
        public int number_of_pos_ratings    { get; set; }
        public double avarange_rating       { get; set; }
        public string description           { get; set; }
        public IFormFile photo              { get; set; }  // ?????????????????????? Are this IFormFile or file or may be something else?
        public string country               { get; set; }  // ?????????????????????? Are this string or instance of Country Class?
        public string city                  { get; set; }
        public string street                { get; set; }
        public int house_number             { get; set; }
        public int building                 { get; set; }
        public int porch                    { get; set; }
        public int postcode                 { get; set; }

    }
}
