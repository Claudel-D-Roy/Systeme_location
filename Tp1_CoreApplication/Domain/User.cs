using Microsoft.AspNetCore.Identity;

namespace Tp1_CoreApplication.Domain;

public class User : IdentityUser<Guid>
{
    public User(string userName) :
           base(userName)
    { }
}
