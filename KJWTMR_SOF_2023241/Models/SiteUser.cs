using Microsoft.AspNetCore.Identity;

namespace KJWTMR_SOF_2023241.Models
{
    public class SiteUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
