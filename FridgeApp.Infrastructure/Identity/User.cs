using Microsoft.AspNetCore.Identity;

namespace FridgeApp.Infrastructure.Identity;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}