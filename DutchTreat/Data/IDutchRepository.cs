using DutchTreat.Data.Entities;
using System.Collections.Generic;

namespace DutchTreat.Data
{
  public interface IDutchRepository
  {
    IEnumerable<Product> GetAllProducts();
    IEnumerable<Product> GetProductsByCategory(string category);

    IEnumerable<Order> GetAllOrders(bool includeItem);
    IEnumerable<Order> GetAllOrdersByUser(string userName, bool includeItem);
    Order GetOrderById(string userName, int id);

    bool SaveAll();
    void AddEntity(object model);
    void AddOrder(Order newOrder);
  }
}