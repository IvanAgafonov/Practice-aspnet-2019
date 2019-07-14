using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store_Rating_System_Dev.Models
{
    public class Store
    {
        [Key]
        public int ID                       { get; set; }

        [MaxLength(50)]
        public string name_Category         { get; set; }

        [ForeignKey("name_Category")]
        public Category Category            { get; set; }

        [MaxLength(50)]
        public string name                  { get; set; }

        [MaxLength(50)]
        public string url                   { get; set; }

        public int number_of_ratings        { get; set; }

        public int number_of_pos_ratings    { get; set; }

        public double avarange_rating       { get; set; }

        [MaxLength(50)]
        public string description           { get; set; }

        [MaxLength(50)]
        public string country               { get; set; }

        [MaxLength(50)]
        public string city                  { get; set; }

        [MaxLength(50)]
        public string street                { get; set; }

        // public IFormFile photo           { get; set; } 

    }

    public class Category
    {
        [Key]
        [MaxLength(50)]
        string name;
    }
}


