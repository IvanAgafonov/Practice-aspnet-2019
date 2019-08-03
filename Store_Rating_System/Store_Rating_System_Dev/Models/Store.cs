using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store_Rating_System_Dev.Models
{
    public enum Categories
    {
        Electronics, Appliances, Food, Computer_Technology, Sports_and_Recreation, Cosmetics, Clothing
    }

    public class Store
    {
        [Key]
        public int ID                       { get; set; }

        public byte[] Image                 { get; set; }

        public Categories Category          { get; set; }

        [MaxLength(50)]
        public string Name                  { get; set; }

        [MaxLength(50)]
        public string Url                   { get; set; }

        public int Number_of_ratings        { get; set; }

        public int Number_of_pos_ratings    { get; set; }

        public double Avarange_rating       { get; set; }

        [MaxLength(500)]
        public string Description           { get; set; }

        public Countries Country            { get; set; }

        public Cities City                  { get; set; }

    }

}


