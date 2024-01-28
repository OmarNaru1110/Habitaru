using Habitaru.Repositories.IRepositories;
using Habitaru.Services.IServices;

namespace Habitaru.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        public int? GetCurrentUserId()
        {
            return _accountRepository.GetCurrentUserId();
        }
    }
}
