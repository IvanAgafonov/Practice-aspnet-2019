using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Rating_System_Dev.Models
{
    public interface IRepository
    {
        IQueryable<Store> Stores { get; }
        void SaveStore(Store store);
        void DeleteStore(Store store);

        IQueryable<Rating> Ratings { get; }
        void SaveRating(Rating rating);
        void DeleteRating(Rating rating);

        IQueryable<User> Users { get; }

    }
}
