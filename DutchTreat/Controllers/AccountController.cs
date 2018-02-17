using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
  public class AccountController : Controller
  {
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<StoreUser> _signInManager;

    /// <summary>
    /// SignInManager is used to manage session - login and logout.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="signInManager"></param>
    public AccountController(ILogger<AccountController> logger,
      SignInManager<StoreUser> signInManager)
    {
      _logger = logger;
      _signInManager = signInManager;
    }

    public IActionResult Login()
    {
      // if someone has already logged in then don't confuse them
      // by showing them the login page again. so do a redirection to Index page of the app. 
      if (this.User.Identity.IsAuthenticated)
      {
        return RedirectToAction("Index", "App");
      }
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
      if (ModelState.IsValid)
      {
        // last boolean is 
        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
        if (result.Succeeded)
        {
          // earlier we saw that clicking on the shop button redirected us to login page with 
          // a return url. thus we can conditionally redirect depending on the presence of the 
          // query string param
          if (Request.Query.Keys.Contains("ReturnUrl"))
          {
            //Query a collection of values. We just want the first of ReturnUrl
            Redirect(Request.Query["ReturnUrl"].First());
          }
          else
          {
            RedirectToAction("Shop", "App");
          }
        }
      }
      ModelState.AddModelError("", "Failed to login");

      return View();
    }

    /// <summary>
    /// Nothing to post here, just log them out
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "App");
    }
  }
}
