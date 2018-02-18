using System;
using System.Collections.Generic;

namespace DutchTreat.Data.Entities
{
  public class Order
  {
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderNumber { get; set; }
    public ICollection<OrderItem> Items { get; set; }
    public StoreUser User { get; set; }
  }
}
