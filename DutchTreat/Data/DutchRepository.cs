using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

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

    public void AddEntity(object model)
    {
      _ctx.Add(model);
    }

    /// <summary>
    /// Order refers to OrderItem refers to Order. 
    /// This would then result in self referencing errors. 
    /// To avoid this handle reference loop handling if you don't
    /// want to throw reference loop exceptions. 
    /// Let us return not just the order but also the related items
    /// and order item products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order> GetAllOrders()
    {
      return _ctx.Orders
        .Include(o => o.Items)
        .ThenInclude(oi => oi.Product)
        .ToList();
    }

    public IEnumerable<Product> GetAllProducts()
    {
      _logger.LogInformation("GetAllProducts was called");
      return _ctx.Products.OrderBy(p => p.Title).ToList();
    }

    public Order GetOrderById(int id)
    {
      return _ctx.Orders
        .Include(o => o.Items)
        .ThenInclude(oi => oi.Product)
        .Where(o => o.Id == id)
        .FirstOrDefault();
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
