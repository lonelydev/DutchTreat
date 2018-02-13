using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DutchTreat.Controllers
{
  [Route("api/[Controller]")]
  public class OrdersController : Controller
  {
    private readonly IDutchRepository _dutchRepository;
    private readonly ILogger<OrdersController> _logger;
    private readonly IMapper _mapper;

    public OrdersController(IDutchRepository dutchRepository, ILogger<OrdersController> logger, IMapper mapper)
    {
      _dutchRepository = dutchRepository;
      _logger = logger;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Get()
    {
      try
      {
        return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(_dutchRepository.GetAllOrders()));
      }
      catch (System.Exception ex)
      {
        _logger.LogError($"Failed to get orders: {ex}");
        return BadRequest("Failed to get orders");
      }
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
      try
      {
        var order = _dutchRepository.GetOrderById(id);
        //remember to add mappings configuration.
        if (order != null) return Ok(_mapper.Map<Order, OrderViewModel>(order));
        return NotFound();
      }
      catch (System.Exception ex)
      {
        _logger.LogError($"Failed to get order: {ex}");
        return BadRequest("Failed to get order");
      }
    }

    /// <summary>
    /// Frombody param is used to mention specifically that the data is supposed to come from the body 
    /// and not from the url query string. 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Post([FromBody]OrderViewModel model)
    {
      //add the order to the database
      try
      {
        if (ModelState.IsValid)
        {
          var newOrder = _mapper.Map<OrderViewModel, Order>(model);
          if (newOrder.OrderDate == DateTime.MinValue)
          {
            newOrder.OrderDate = DateTime.Now;
          }

          _dutchRepository.AddEntity(newOrder);

          if (_dutchRepository.SaveAll())
          {
            var vm = _mapper.Map<Order, OrderViewModel>(newOrder);

            // in http when you create an entity, you return Created not just Ok.
            // you also need to return the entity with its url. 
            // done as part of HATEOAS
            return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));
          }
        }
        else
        {
          return BadRequest(ModelState);
        }

      }
      catch (System.Exception ex)
      {

        _logger.LogError($"Failed to add a new order: {ex}");
      }
      return BadRequest("Failed to add a new order");
    }
  }
}
