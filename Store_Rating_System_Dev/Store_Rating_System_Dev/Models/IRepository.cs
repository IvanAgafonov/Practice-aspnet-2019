using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Rating_System_Dev.Models
{
    // интерфейс для работы с бд
    public class IRepository
    {
        IQueryable<Store> Stores { get; }
        IQueryable<Rating> Ratings { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<User> Users { get; }
        IQueryable<Moderator> Moderators { get; }
        IQueryable<Admin> Admins { get; }
        IQueryable<Account> Accounts { get; }



    }
}
