using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DutchTreat.Controllers
{
  [Route("/api/orders/{orderId}/items")]
  public class OrderItemsController : Controller
  {
    private readonly IDutchRepository _dutchRepository;
    private readonly ILogger<OrderItemsController> _logger;
    private readonly IMapper _mapper;

    public OrderItemsController(IDutchRepository dutchRepository,
      ILogger<OrderItemsController> logger,
      IMapper mapper)
    {
      _dutchRepository = dutchRepository;
      _logger = logger;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Get(int orderId)
    {
      var order = _dutchRepository.GetOrderById(orderId);
      if (order != null) return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
      return NotFound();
    }

    [HttpGet("{id}")]
    public IActionResult Get(int orderId, int id)
    {
      var order = _dutchRepository.GetOrderById(orderId);
      if (order != null)
      {
        var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
        if (item != null)
        {
          return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
        }
      }
      return NotFound();
    }

    //create methods for post, delete in order to add new items, delete, update
    //http://shawnw.me/corewebapi

  }
}
