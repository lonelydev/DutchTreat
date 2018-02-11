using DutchTreat.Data.Entities;
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
    public DutchRepository(DutchContext ctx)
    {
      _ctx = ctx;
    }

    public IEnumerable<Product> GetAllProducts()
    {
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
