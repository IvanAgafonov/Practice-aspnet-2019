using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Store_Rating_System_Dev.Models
{
    // Контекст для работы с MS SQL
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Store> Stores { get; set; }
    }
}
