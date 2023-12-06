using Microsoft.EntityFrameworkCore;

namespace Order.API.DBContext
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }
        public DbSet<Models.Order> Orders { get; set; }
    }
}
