using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Store_Rating_System.Models
{

    public static class SeedData
    {

        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Stores.Any())
            {
                context.Stores.AddRange(
                    new Store
                    {
                        Name = "MacDonalds",
                        Description = "Tasty Fastfood",
                        URL = "mcdonalds.ru"
                    },
                    new Store
                    {
                        Name = "Samsung",
                        Description = "Modern Technology",
                        URL = "samsung.com"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
