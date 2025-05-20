using Microsoft.AspNetCore.Identity;

namespace AvdoshkaMMM.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pathronomic { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
