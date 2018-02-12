using DutchTreat.Data;
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
  }
}
