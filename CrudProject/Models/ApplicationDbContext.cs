using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace CrudProject.Models
{
    public class ApplicationDbContext : DbContext
    
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
