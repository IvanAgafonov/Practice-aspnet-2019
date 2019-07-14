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

        IQueryable<Store> Stores => context.Stores;
        IQueryable<Rating> Ratings => context.Ratings;
        IQueryable<Category> Categories => context.Categories;
        IQueryable<User> Users => context.Users;
        IQueryable<Moderator> Moderators => context.Moderators;
        IQueryable<Admin> Admins => context.Admins;
        IQueryable<Account> Accounts => context.Accounts;
    }
}
