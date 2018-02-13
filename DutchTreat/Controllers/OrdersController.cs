using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
  [Route("api/[Controller]")]
  public class OrdersController : Controller
  {
    private readonly IDutchRepository _dutchRepository;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IDutchRepository dutchRepository, ILogger<OrdersController> logger)
    {
      _dutchRepository = dutchRepository;
      _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
      try
      {
        return Ok(_dutchRepository.GetAllOrders());
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
        if (order != null) return Ok(order);
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
    public IActionResult Post([FromBody]Order model)
    {
      //add the order to the database
      try
      {
        _dutchRepository.AddEntity(model);
        _dutchRepository.SaveAll();

        // in http when you create an entity, you return Created not just Ok.
        // you also need to return the entity with its url. 
        // done as part of HATEOAS
        return Created($"/api/orders/{model.Id}", model);
      }
      catch (System.Exception ex)
      {

        _logger.LogError($"Failed to add a new order: {ex}");
      }
      return BadRequest("Failed to save the order");
    }
  }
}
