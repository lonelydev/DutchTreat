using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
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
            ViewBag.Title = "Contact Us";
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About us";
            return View();
        }
    }
}
