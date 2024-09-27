using Microsoft.EntityFrameworkCore;

namespace Global.TradingPlatform.OrderService
{
    public class TradingPlatformContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        
        public TradingPlatformContext(DbContextOptions<TradingPlatformContext> options)
            : base(options)
        {
        }
    }
}
