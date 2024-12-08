using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace CrudProject.Models
{
    public class ApplicationDbContext : DbContext
    
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
