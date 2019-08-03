//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.EntityFrameworkCore;

//namespace Store_Rating_System_Dev.Models
//{
//    public static class SeedData
//    {
//        private static int CountAccountID = 1;  // Inital available Account ID
//        private static int GetAccountID() => CountAccountID++;  // Get free Account ID
//        private static int? SomeAdminID;
//        private static int? SomeModeratorID;
//        private static int? SomeUserID;
//        private static int? SomeStoreID;


//        public static void EnsurePopulated(IApplicationBuilder app)
//        {
//            ApplicationDbContext context = app.ApplicationServices
//            .GetRequiredService<ApplicationDbContext>();

//            if (!context.Stores.Any())
//            {

//                // Array of characters
//                StringBuilder all_characters = new StringBuilder();
//                for (char symbol = 'A'; symbol <= 'Z'; symbol++)
//                {
//                    all_characters.Append(symbol);
//                }

//                for (char symbol = 'a'; symbol <= 'z'; symbol++)
//                {
//                    all_characters.Append(symbol);
//                }

//                // Array of digits
//                StringBuilder all_digits = new StringBuilder();
//                for (char digit = '0'; digit <= '9'; digit++)
//                {
//                    all_digits.Append(digit);
//                }

//                // Array of first names
//                string[] possible_first_names = { "Frank", "Milton", "Lewis", "Mark", "Sophia", "Linda" };

//                // Array of last names
//                string[] possible_last_names = { "Lloyd", "Logan", "Hensley", "Hudson", "Ramsey", "Barrett" };

//                // Array of email domains
//                string[] possible_email_domains = { "gmail.com", "mail.ru", "yandex.ru", "rambler.ru" };

//                // Array of categories
//                string[] possible_categories = { "Electronics", "Appliances", "Food", "Computer Technology",
//                    "Sports & Recreation", "Cosmetics", "Clothing"};

//                // Array of names and urls Stores
//                Dictionary<string, string> names_urls = new Dictionary<string, string>();
//                names_urls.Add("Mvideo", "www.mvideo.ru");
//                names_urls.Add("Sportmaster", "www.sportmaster.ru");
//                names_urls.Add("Gucci", "www.gucci.com");
//                names_urls.Add("Podrygka", "www.podrygka.ru");
//                names_urls.Add("Logitech", "www.logitech.com");
//                names_urls.Add("Bosch", "www.bosch.ru");
//                names_urls.Add("Perekrestok", "www.perekrestok.ru");


//                // Array of names and descriprion
//                Dictionary<string, string> names_description = new Dictionary<string, string>();
//                names_description.Add("Mvideo", "Good electronics store and other related products.");
//                names_description.Add("Sportmaster", "Sporting goods, fitness, leisure activities all at " +
//                    "affordable prices.");
//                names_description.Add("Gucci", "Stylish clothes of the best quality, known all over the world.");
//                names_description.Add("Podrygka", "This is a bright retail chain of stores and" +
//                    " an online store of cosmetics, perfumes and useful things.");
//                names_description.Add("Logitech", "Swiss company, manufacturer of computer peripherals, " +
//                    "devices for gamers, etc.");
//                names_description.Add("Bosch", "German group of companies, a leading global supplier of technologies and " +
//                    "services in the field of automotive and industrial technologies, consumer goods.");
//                names_description.Add("Perekrestok", "Sale and delivery of food to the home and office " +
//                    "in Moscow and the region.");

//                // Array of names and category
//                Dictionary<string, string> names_category = new Dictionary<string, string>();
//                names_category.Add("Mvideo", "Electronics");
//                names_category.Add("Sportmaster", "Sports & Recreation");
//                names_category.Add("Gucci", "Clothing");
//                names_category.Add("Podrygka", "Cosmetics");
//                names_category.Add("Logitech", "Computer Technology");
//                names_category.Add("Bosch", "Appliances");
//                names_category.Add("Asus", "Computer Technology");
//                names_category.Add("Perekrestok", "Food");

//                // Dict of countries with cities
//                Dictionary<string, List<string>> country_cities = new Dictionary<string, List<string>>();
//                country_cities.Add("Russia", new List<string> { "Moscow", "Saint Petersburg", "Saratov", "Krasnodar" });
//                country_cities.Add("Belarus", new List<string> { "Minsk", "Grodno", "Mogilyov" });
//                country_cities.Add("America", new List<string> { "New York", "San Francisco", "Chicago" });
//                country_cities.Add("Germany", new List<string> { "Berlin", "Munich", "Hamburg" });


//                // Array of digits with characters
//                StringBuilder all_digits_characters = new StringBuilder(
//                    all_digits.ToString() + all_characters.ToString());

//                // Create Random instence
//                Random rand = new Random();

//                // Fill Accounts table
//                { 
//                    const int min_length = 6;  // min lenght of login or password
//                    const int max_length = 10;  // max lenght of login or password
//                    const int account_count = 10; // count accounts to create

//                    for (int i = 0; i < account_count; i++)
//                    {
//                        StringBuilder login = new StringBuilder();
//                        StringBuilder password = new StringBuilder();
//                        // get randomize login
//                        for (int j = 0; j < rand.Next(min_length, max_length); j++)
//                        {
//                            login.Append(all_characters[rand.Next(all_characters.Length)]);
//                        }

//                        // get randomize password
//                        for (int j = 0; j < rand.Next(min_length, max_length); j++)
//                        {
//                            password.Append(all_digits_characters[rand.Next(all_digits_characters.Length)]);
//                        }

//                        // Add new Account to conxetx
//                        context.Accounts.Add(
//                            new Account
//                            {
//                                login = login.ToString(),
//                                password = password.ToString()
//                            });
//                    }
//                }

//                // Saving intermidiate changes
//                context.SaveChanges();

//                // Fill Admin table
//                {
//                    const int admin_count = 1;  // count admins to create

//                    int ID = GetAccountID();
//                    string first_name = possible_first_names[rand.Next(possible_first_names.Length)];
//                    string last_name = possible_last_names[rand.Next(possible_last_names.Length)];
//                    string email = first_name + last_name +
//                        "@" + possible_email_domains[rand.Next(possible_email_domains.Length)];

//                    context.Admins.Add(
//                        new Admin
//                        {
//                            ID = ID,
//                            first_name = first_name,
//                            last_name = last_name,
//                            email = email
//                        });

//                    SomeAdminID = ID;
//                }

//                // Saving intermidiate changes
//                context.SaveChanges();

//                // Fill Moderator
//                {
//                    const int moderator_count = 3;  // count moderators to create

//                    for (int i = 0; i < moderator_count; i++)
//                    {
//                        int ID = GetAccountID();
//                        string first_name = possible_first_names[rand.Next(possible_first_names.Length)];
//                        string last_name = possible_last_names[rand.Next(possible_last_names.Length)];
//                        string email = first_name + last_name +
//                            "@" + possible_email_domains[rand.Next(possible_email_domains.Length)];
//                        int Admin_ID = SomeAdminID ?? throw new MissingMemberException("No one Admin in DB!");

//                        context.Moderators.Add(
//                            new Moderator
//                            {
//                                ID = ID,
//                                Admin_ID = Admin_ID,
//                                first_name = first_name,
//                                last_name = last_name,
//                                email = email
//                            }
//                            );

//                        SomeModeratorID = ID;
//                    }
//                }

//                // Saving intermidiate changes
//                context.SaveChanges();

//                // Fill User
//                {
//                    const int user_count = 6;  // count users to create


//                    for (int i = 0; i < user_count; i++)
//                    {
//                        int ID = GetAccountID();
//                        string first_name = possible_first_names[rand.Next(possible_first_names.Length)];
//                        string last_name = possible_last_names[rand.Next(possible_last_names.Length)];
//                        string email = first_name + last_name +
//                            "@" + possible_email_domains[rand.Next(possible_email_domains.Length)];
//                        int Moderator_ID = SomeModeratorID ?? throw new MissingMemberException("No one Moderator in DB!");
//                        StringBuilder phone_number_str = new StringBuilder("8");
//                        const int number_digits = 11 - 1;  // -1 because of start digit is always '8'
//                        // get randomize phone
//                        for (int j = 0; j < number_digits; j++)
//                        {
//                            phone_number_str.Append(all_digits[rand.Next(all_digits.Length)]);
//                        }
//                        var phone_number = phone_number_str.ToString();

//                        //get randomize status
//                        bool? status = null;
//                        switch (rand.Next(0, 3))
//                        {
//                            case 0:
//                                status = true;
//                                break;
//                            case 1:
//                                status = false;
//                                break;
//                            case 2:
//                                break;
//                        }

//                        bool gender = rand.Next(0, 2) == 0 ? true : false;

//                        // Get randomize list of country cities
//                        var country = country_cities.Keys.ToArray()[rand.Next(country_cities.Keys.Count())];
//                        var list_cities = country_cities[country];
//                        var city = list_cities[rand.Next(list_cities.Count())];

//                        // Date of start 1980 year
//                        DateTime start_birth = new DateTime(1980, 1, 1);
//                        //DateTime.Now.Year
//                        // Max plus days to 1980 year
//                        int max_days = 20 * 365;
//                        //range between today and start of current year
//                        //int range = (DateTime.Today - start).Days;
//                        // 
//                        DateTime date_of_birth = start_birth.AddDays(rand.Next(max_days)).AddHours(rand.Next(24)).AddMinutes(rand.Next(60));
//                        DateTime date_of_registration = new DateTime(DateTime.Now.Year-1, 1, 1).AddDays(rand.Next(DateTime.Now.Day+365));

//                        context.Users.Add(
//                            new User
//                            {
//                                ID = ID,
//                                first_name = first_name,
//                                last_name = last_name,
//                                email = email,
//                                Moderator_ID = Moderator_ID,
//                                phone_number = phone_number,
//                                status = status,
//                                gender = gender,
//                                country = country,
//                                city = city,
//                                date_of_birth = date_of_birth,
//                                date_of_registration = date_of_registration
//                            }
//                        );

//                        SomeUserID = ID;
//                    }
//                }

//                // Saving intermidiate changes
//                context.SaveChanges();

//                // Fill Category
//                {
//                    foreach (var i in possible_categories)
//                    {
//                        context.Categories.Add(
//                            new Category
//                            {
//                                name = i
//                            });
//                    }
//                }

//                // Saving intermidiate changes
//                context.SaveChanges();

//                // Fill Store
//                {
//                    const int store_count = 7;

//                    foreach (var i in names_urls)
//                    {
//                        // Get randomize list of country cities
//                        string name = i.Key;
//                        string country = country_cities.Keys.ToArray()[rand.Next(country_cities.Keys.Count())];
//                        var list_cities = country_cities[country];
//                        var city = list_cities[rand.Next(list_cities.Count())];
//                        string category = names_category[name];
//                        string url = names_urls[name];
//                        string description = names_description[name];
//                        const int max_ratings = 100;
//                        int number_of_ratings = rand.Next(max_ratings);
//                        int number_of_pos_ratings = rand.Next(number_of_ratings);
//                        //int number_of_neg_ratings = number_of_ratings - number_of_pos_ratings + rand.Next(max_ratings/10);

//                        context.Stores.Add(
//                            new Store
//                            {
//                                name = name,
//                                url = url,
//                                name_Category = category,
//                                number_of_ratings = number_of_ratings,
//                                number_of_pos_ratings = number_of_pos_ratings,
//                                avarange_rating = number_of_pos_ratings / number_of_ratings, /// FORMULA!
//                                description = description,
//                                country = country,
//                                city = city,
//                            });

//                        SomeStoreID = 1;
//                    }
//                }


//                // Saving intermidiate changes
//                context.SaveChanges();

//                // Fill Rating
//                {
//                    string comment = "Very nice";
//                    bool value = true;
//                    DateTime date_of_publication = new DateTime(DateTime.Now.Year, 1, 1).AddDays(
//                        rand.Next(DateTime.Now.Day)).AddHours(rand.Next(24)).AddMinutes(rand.Next(60));

//                    bool? status = null;
//                    switch (rand.Next(0, 3))
//                    {
//                        case 0:
//                            status = true;
//                            break;
//                        case 1:
//                            status = false;
//                            break;
//                        case 2:
//                            break;
//                    }

//                    int User_ID = SomeUserID ?? throw new MissingMemberException("No one User in DB!");
//                    int Store_ID = SomeStoreID ?? throw new MissingMemberException("No one Store in DB!");


//                    context.Ratings.Add(
//                        new Rating
//                        {
//                            User_ID = User_ID,
//                            Moderator_ID = SomeModeratorID,
//                            Store_ID = Store_ID,
//                            comment = comment,
//                            status = status,
//                            value = value,
//                            date_of_publication = date_of_publication
//                        }
//                    );

//                    comment = "Very bad";
//                    switch (rand.Next(0, 3))
//                    {
//                        case 0:
//                            status = true;
//                            break;
//                        case 1:
//                            status = false;
//                            break;
//                        case 2:
//                            break;
//                    }

//                    date_of_publication = new DateTime(DateTime.Now.Year, 1, 1).AddDays(
//                        rand.Next(DateTime.Now.Day)).AddHours(rand.Next(24)).AddMinutes(rand.Next(60));

//                    value = false;

//                    User_ID = (int)SomeUserID - 1;
//                    Store_ID = (int)SomeStoreID;

//                    context.Ratings.Add(
//                        new Rating
//                        {
//                            User_ID = User_ID,
//                            Moderator_ID = SomeModeratorID,
//                            Store_ID = Store_ID,
//                            comment = comment,
//                            status = status,
//                            value = value,
//                            date_of_publication = date_of_publication
//                        }
//                    );
//                }


//                // Saving intermidiate changes
//                context.SaveChanges();

//            }
//        }
//    }
//}
