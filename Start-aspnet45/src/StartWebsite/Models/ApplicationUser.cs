using Khulnasoft.AspNet.Identity.EntityFramework;

namespace Start.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}