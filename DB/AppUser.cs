using Microsoft.AspNetCore.Identity;

namespace ComplantApi.DB
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }    
}
