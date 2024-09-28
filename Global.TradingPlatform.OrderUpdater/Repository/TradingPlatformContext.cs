using Microsoft.EntityFrameworkCore;

namespace Global.TradingPlatform.OrderUpdater
{
    public class TradingPlatformContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Execution> Executions { get; set; }
        public TradingPlatformContext(DbContextOptions<TradingPlatformContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Executions)
                .WithOne(e => e.Order)
                .HasForeignKey(e => e.OrderID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
