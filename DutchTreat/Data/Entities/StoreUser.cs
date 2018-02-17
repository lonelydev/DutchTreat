using Microsoft.AspNetCore.Identity;

namespace DutchTreat.Data.Entities
{
  /// <summary>
  /// This is a user that knows what kind of user name there is. 
  /// The generic IdentityUser has a lot of built in properties 
  /// that can be used for identities. 
  /// By using IdentityUser to inherit we are saying that we want to add on to it.
  /// </summary>
  public class StoreUser : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}
