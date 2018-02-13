using DutchTreat.Data.Entities;
using System.Collections.Generic;

namespace DutchTreat.Data
{
  public interface IDutchRepository
  {
    IEnumerable<Product> GetAllProducts();
    IEnumerable<Product> GetProductsByCategory(string category);

    IEnumerable<Order> GetAllOrders();
    Order GetOrderById(int id);

    int SaveAll();
    void AddEntity(object model);
  }
}