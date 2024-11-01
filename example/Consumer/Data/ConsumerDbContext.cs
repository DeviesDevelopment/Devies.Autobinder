using Microsoft.EntityFrameworkCore;

namespace Consumer.Data;

public class ConsumerDbContext : DbContext {
    public DbSet<Product> Products { get; set; }

    public ConsumerDbContext(DbContextOptions<ConsumerDbContext> options) : base(options)
    {
        
    }
}