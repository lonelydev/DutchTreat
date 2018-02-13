using DutchTreat.Data.Entities;
using System.Collections.Generic;

namespace DutchTreat.Data
{
  public interface IDutchRepository
  {
    IEnumerable<Product> GetAllProducts();
    IEnumerable<Product> GetProductsByCategory(string category);

    IEnumerable<Order> GetAllOrders(bool includeItem);
    Order GetOrderById(int id);

    bool SaveAll();
    void AddEntity(object model);
  }
}