using API_MakeupCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace API_MakeupCRUD.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<MakeupProduct> MakeupProducts { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}
