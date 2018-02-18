using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
  public class AccountController : Controller
  {
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<StoreUser> _signInManager;
    private readonly UserManager<StoreUser> _userManager;
    private readonly IConfiguration _config;

    /// <summary>
    /// SignInManager is used to manage session - login and logout.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="signInManager"></param>
    public AccountController(ILogger<AccountController> logger,
      SignInManager<StoreUser> signInManager, UserManager<StoreUser> userManager, IConfiguration config)
    {
      _logger = logger;
      _signInManager = signInManager;
      _userManager = userManager;
      this._config = config;
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
            return RedirectToAction("Shop", "App");
          }
        }
      }
      else
      {
        ModelState.AddModelError("", "Failed to login");
      }
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

    /// <summary>
    /// An API to create a JWTtoken as we use JsonWebToken based authentication.
    /// To learn more head here:https://jwt.io/introduction/
    /// For a concise description:
    /// It is an open standard that defines a fast, lightweight way of securely transmitting information. 
    /// Primarily used for authentication. 
    /// Contains three parts: Header, payload and signature. 
    /// Header says, what type of token and what hashing algo is used. 
    /// Payload contains claims, which are statements about the user to be authenticated and they are of three types.
    /// Signature is created by using the encoded (header and payload), a secret key and the algorithm to sign the jwt.
    /// 
    /// </summary>
    /// <param name="loginViewModel"></param>
    /// <returns></returns>

    [HttpPost]
    public async Task<IActionResult> CreateToken([FromBody] LoginViewModel loginViewModel)
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
        if (user != null)
        {
          var result = await _signInManager.CheckPasswordSignInAsync(user, loginViewModel.Password, lockoutOnFailure: false);
          if (result.Succeeded)
          {
            // create the token here
            // Claims-based identity is a common way for applications to acquire the identity information they need about users inside their organization, in other organizations, 
            // and on the Internet.[1] It also provides a consistent approach for applications running on-premises or in the cloud. 
            // Claims -based identity abstracts the individual elements of identity and access control into two parts: 
            // a notion of claims, and the concept of an issuer or an authority
            // to create a claim you need a time and a value!
            var claims = new[]
            {
              // Sub - name of the subject - which is user email here.
              new Claim(JwtRegisteredClaimNames.Sub, user.Email),
              // jti - unique string that is representative of each token so using a guid
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              // unque name - username of the user mapped to the identity inside the user object 
              // that is available on every controller and view
              new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            // key is the secret used to encrypt the token. some parts of the token aren't encrypted but other parts are. 
            // credentials, who it is tied to and exploration etc are encrypted. 
            // information about the claims, about the individual etc aren't encrypted. 
            // use a natural string for a string and encode it to bytes. 
            // read from configuration json - keep changing/or fetch from another source. 
            // the trick here is that the key needs to be accessible for the application
            // also needs to be replaceable by the people setting up your system. 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

            // new credentials required. create it using the key you just created in combination with a
            // security algorithm.
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"], // the creator of the token
              _config["Tokens:Audience"], // who can use the token
              claims,
              expires: DateTime.UtcNow.AddMinutes(20),
              signingCredentials: credentials);

            var results = new
            {
              token = new JwtSecurityTokenHandler().WriteToken(token),
              expiration = token.ValidTo
            };

            // empty quotes to say no source for this resource, just give a new object
            return Created("", results);
          }
        }
      }
      return BadRequest();
    }
  }
}
