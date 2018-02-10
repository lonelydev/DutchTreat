using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DutchTreat.Data
{
  public class DutchContext : DbContext
  {
    public DutchContext(DbContextOptions<DutchContext> dbContextOptions) : base(dbContextOptions)
    {

    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
  }
}
