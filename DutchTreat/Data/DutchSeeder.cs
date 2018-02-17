using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
  /// <summary>
  /// Seeders are useful to build lookup information or static data that is
  /// always going to be the same forever. 
  /// </summary>
  public class DutchSeeder
  {
    private readonly DutchContext _ctx;
    private readonly IHostingEnvironment _hosting;
    private readonly UserManager<StoreUser> _userManager;

    public DutchSeeder(DutchContext ctx, IHostingEnvironment hosting, UserManager<StoreUser> userManager)
    {
      _ctx = ctx;
      _hosting = hosting;
      _userManager = userManager;
    }

    public async Task Seed()
    {
      // make sure the datbase actually created!
      _ctx.Database.EnsureCreated();

      //because the following is async, change the signature of the invoker to be 
      // async task too 
      var user = await _userManager.FindByEmailAsync("eakan@dutchtreat.com");
      if (user == null)
      {
        //there are several properties you could set, however we only set the following 
        user = new StoreUser()
        {
          FirstName = "Eakan",
          LastName = "Gopalakrishnan",
          UserName = "eakan@dutchtreat.com",
          Email = "eakan@dutchtreat.com"
        };
        //while creating a user you can also set the password
        var result = await _userManager.CreateAsync(user, "P@$$w0RD!");
        if (result != IdentityResult.Success)
        {
          throw new InvalidOperationException("Failed to create default user!");
        }
      }

      if (!_ctx.Products.Any())
      {
        // need to load a lot of data and not want to manually add new product objects
        // one by one. 
        // we need a path to the file first. we could pass a hardcoded string
        // this will work in visual studio but not runtime. so we can inject ihostingenvironment
        var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
        var json = File.ReadAllText(filepath);

        var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

        _ctx.Products.AddRange(products);

        // add previously created user to the order
        var order = new Order()
        {
          OrderDate = DateTime.Now,
          OrderNumber = "12345",
          User = user,
          Items = new List<OrderItem>()
          {
            new OrderItem()
            {
              Product = products.First(),
              Quantity = 5,
              UnitPrice = products.First().Price
            }
          }
        };
        _ctx.Orders.Add(order);
        _ctx.SaveChanges();
      }
    }
  }
}
