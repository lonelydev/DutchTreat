using DutchTreat.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
  public class DutchRepository : IDutchRepository
  {
    private readonly DutchContext _ctx;
    private readonly ILogger<DutchRepository> _logger;

    public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
    {
      _ctx = ctx;
      _logger = logger;
    }

    public IEnumerable<Product> GetAllProducts()
    {
      _logger.LogInformation("GetAllProducts was called");
      return _ctx.Products.OrderBy(p => p.Title).ToList();
    }

    public IEnumerable<Product> GetProductsByCategory(string category)
    {
      return _ctx.Products.Where(p => p.Category == category).ToList();
    }

    public int SaveAll()
    {
      return _ctx.SaveChanges();
    }
  }
}
