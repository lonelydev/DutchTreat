using DutchTreat.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DutchTreat.Controllers
{
  /// <summary>
  /// webapi used to be a separate library to expose api calls. 
  /// in .net core it is all in one library. 
  /// </summary>
  [Route("api/[Controller]")]
  public class ProductsController : Controller
  {
    private readonly IDutchRepository _dutchRepository;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IDutchRepository dutchRepository, ILogger<ProductsController> logger)
    {
      _dutchRepository = dutchRepository;
      _logger = logger;
    }

    /// <summary>
    /// How do we handle exceptions ?
    /// What do I return to the users?
    /// Setting return type to IActionResult
    /// simplifies HttpResponse codes. And Error Handling. 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
      try
      {
        return Ok(_dutchRepository.GetAllProducts());
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get products:{ex}");
        return BadRequest("Failed to get products");
      }
    }
  }
}
