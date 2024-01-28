using Microsoft.AspNetCore.Identity;

namespace Habitaru.Models
{
    public class ApplicationUser:IdentityUser<int>
    {
        public virtual ICollection<Habit> Habits { get; set; }
    }
}
