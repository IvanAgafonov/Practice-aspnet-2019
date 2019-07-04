using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Store_Rating_System.Models
{
    public static class Repository
    {
        private static List<Store> responses = new List<Store>();
        public static IEnumerable<Store> Responses
        {
            get
            {
                return responses;
            }
        }
        public static void AddResponse(Store response)
        {
            responses.Add(response);
        }
    }


    public class Store
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter stores name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter some description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter stores url address")]
        [RegularExpression(".+\\..+", ErrorMessage = "Please enter a valid url address")]
        public string URL { get; set; }
    }

    public class User
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        public string First_name { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        public string Last_name { get; set; }

        public string Papatronymic { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public DateTime Registaration_time { get; set; }
    }

    public class Rating
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter comment")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Please enter your value")]
        public int Value { get; set; }

        public bool status { get; set; }

        public DateTime Creating_time { get; set; }
    }
}