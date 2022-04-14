using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreExercise.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Friends> Friends { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sales> SalesReport { get; set; }
    }
}
