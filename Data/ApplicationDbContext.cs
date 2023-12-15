using Microsoft.EntityFrameworkCore;
using Product_Management_System.models;

namespace Product_Management_System.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product>Products { get; set; }
        public DbSet<Order>Orders { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
