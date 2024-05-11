using Microsoft.EntityFrameworkCore;
using WebApiWeb.Models;
namespace WebApiWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Webdb;Username=postgres;Password=1234");
        }
    }
}
