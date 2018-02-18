using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
  [Route("api/[Controller]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class OrdersController : Controller
  {
    private readonly IDutchRepository _dutchRepository;
    private readonly ILogger<OrdersController> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<StoreUser> _userManager;

    public OrdersController(IDutchRepository dutchRepository,
      ILogger<OrdersController> logger,
      IMapper mapper,
      UserManager<StoreUser> userManager)
    {
      _dutchRepository = dutchRepository;
      _logger = logger;
      _mapper = mapper;
      this._userManager = userManager;
    }

    /// <summary>
    /// Optional bool for query string. 
    /// </summary>
    /// <param name="includeItem"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get(bool includeItem = true)
    {
      // now that we have added jwt authentication we can also try and retrieve 
      // orders that are related to a particular user. 
      try
      {
        // to retrieve user information, we use the User.Identity.Name as the account controller 
        // has already authenticated and authorized the user. 
        // then we have also added the [Authorize] attribute to the class
        var userName = User.Identity.Name;
        var results = _dutchRepository.GetAllOrdersByUser(userName, includeItem);
        return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(results));
      }
      catch (Exception ex)
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
        var order = _dutchRepository.GetOrderById(User.Identity.Name, id);
        //remember to add mappings configuration.
        if (order != null) return Ok(_mapper.Map<Order, OrderViewModel>(order));
        return NotFound();
      }
      catch (Exception ex)
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
    public async Task<IActionResult> Post([FromBody]OrderViewModel model)
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

          var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
          newOrder.User = currentUser;

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
