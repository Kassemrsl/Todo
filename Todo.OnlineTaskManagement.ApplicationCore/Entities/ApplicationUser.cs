using Microsoft.AspNetCore.Identity;

namespace Todo.OnlineTaskManagement.ApplicationCore.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
