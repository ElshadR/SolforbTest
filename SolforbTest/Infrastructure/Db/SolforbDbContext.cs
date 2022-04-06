using Microsoft.EntityFrameworkCore;
using SolforbTest.Infrastructure.Db.Models;

namespace SolforbTest.Infrastructure.Db
{
    public class SolforbDbContext : DbContext
    {
        public SolforbDbContext(DbContextOptions<SolforbDbContext> dbContext) : base(dbContext)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
    }
}
