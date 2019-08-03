using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Rating_System_Dev.Models
{
    public class EFRepository : IRepository
    {
        private ApplicationDbContext context;

        public EFRepository(ApplicationDbContext ctx) { context = ctx; }

        public IQueryable<Store> Stores => context.Stores;
        public IQueryable<Rating> Ratings => context.Ratings;

        public IQueryable<User> Users => context.Users;

        private void OnRatingCreate(Rating rating)
        {
            var store = Stores.Where(x => x.ID == rating.Store_ID).FirstOrDefault();
            if (store != null)
            {
                store.Number_of_ratings++;
                if (rating.Rate_value == true)
                    store.Number_of_pos_ratings++;
                store.Avarange_rating = System.Math.Round(((double)store.Number_of_pos_ratings / store.Number_of_ratings), 1);
            }
            context.SaveChanges();
        }

        public void SaveRating(Rating rating)
        {
            Rating DB_Rating = context.Ratings.Where(x => x.ID == rating.ID).FirstOrDefault();
            if (DB_Rating == null)
            {
                context.Ratings.Add(rating);
                OnRatingCreate(rating);
            }
            else
            {
                DB_Rating.User_ID = rating.User_ID;
                DB_Rating.Store_ID = rating.Store_ID;
                DB_Rating.Comment = rating.Comment;
                DB_Rating.Rate_value = rating.Rate_value;
                DB_Rating.Rate_Status = rating.Rate_Status;
                DB_Rating.Date_of_publication = rating.Date_of_publication;
            }
            context.SaveChanges();
        }

        public void DeleteRating(Rating rating)
        {
            Rating DB_Rating = context.Ratings.Where(x => x.ID == rating.ID).FirstOrDefault();
            if (DB_Rating != null)
            {
                context.Ratings.Remove(DB_Rating);
                context.SaveChanges();
            }
        }

        public void SaveStore(Store store)
        {
            Store DB_Store = context.Stores.Where(x => x.ID == store.ID).FirstOrDefault();
            if (DB_Store == null)
            {
                context.Stores.Add(store);
            }
            else
            {
                DB_Store.Name = store.Name;
                DB_Store.Url = store.Url;
                DB_Store.Category = store.Category;
                DB_Store.Number_of_ratings = store.Number_of_ratings;
                DB_Store.Number_of_pos_ratings = store.Number_of_pos_ratings;
                DB_Store.Description = store.Description;
                DB_Store.City = store.City;
                DB_Store.Country = store.Country;
                DB_Store.Image = store.Image;
            }
            context.SaveChanges();
        }

        public void DeleteStore(Store store)
        {
            Store DB_Store = context.Stores.Where(x => x.ID == store.ID).FirstOrDefault();
            if (DB_Store != null)
            {
                context.Stores.Remove(DB_Store);
                context.SaveChanges();
            }
        }
    }
}
