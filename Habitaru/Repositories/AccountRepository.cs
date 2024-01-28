
using Habitaru.Repositories.IRepositories;
using System.Security.Claims;

namespace Habitaru.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetCurrentUserId()
        {
            var strUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var conversionResult = int.TryParse(strUserId, out int userId);
            
            if(conversionResult == false)
                return null;
            return userId;
        }
    }
}
