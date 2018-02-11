using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
  public class AppController : Controller
  {
    private readonly IMailService _mailService;
    private readonly IDutchRepository _dutchRepository;

    public AppController(IMailService mailService, IDutchRepository dutchRepository)
    {
      _mailService = mailService;
      _dutchRepository = dutchRepository;
    }
    /// <summary>
    /// The name of this method is the name of the View file. 
    /// By convention App controller will have a folder inside Views
    /// and Index method would mean there would be a cshtml of the same name
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
      return View();
    }

    [HttpGet("contact")]
    public IActionResult Contact()
    {
      //throw new InvalidOperationException("Fuck it Error");            
      return View();
    }

    /// <summary>
    /// Contact form will post information from the form.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("contact")]
    public IActionResult Contact(ContactViewModel model)
    {
      if (ModelState.IsValid)
      {
        //send the email
        _mailService.SendMessage("eakan@gmail.com", model.Subject, $"From:{model.Name} - {model.Email}, Message: {model.Message}");
        ViewBag.UserMessage = "Mail Sent";
        ModelState.Clear();
      }
      return View();
    }

    public IActionResult About()
    {
      ViewBag.Title = "About us";
      return View();
    }

    public IActionResult Shop()
    {
      var results = _dutchRepository.GetAllProducts();
      return View(results);
    }
  }
}
