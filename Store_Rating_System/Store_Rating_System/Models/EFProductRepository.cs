using System.Collections.Generic;
using System.Linq;

namespace Store_Rating_System.Models
{

    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;

        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Store> Stores => context.Stores;
    }
}
