using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DutchTreat.Data
{
  /// <summary>
  /// Derive from Identity entity framework core namespace.
  /// add mention what the type of the identity user is.
  /// After switching to IdentityDbContext, we have to go open a command prompt
  /// and run the following:
  /// dotnet ef migrations add Identity
  /// That should add the migration to create Identity related tables in the database.
  /// 
  /// </summary>
  public class DutchContext : IdentityDbContext<StoreUser>
  {
    public DutchContext(DbContextOptions<DutchContext> dbContextOptions) : base(dbContextOptions)
    {

    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
  }
}
