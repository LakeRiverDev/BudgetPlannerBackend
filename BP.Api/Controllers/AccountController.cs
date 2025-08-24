using BP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BP.Api.Controllers
{
    /// <summary>
    /// Контроллер для получения информации по балансу
    /// </summary>
    [Route("api/v1/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> logger;
        private readonly IAccountService accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            this.logger = logger;
            this.accountService = accountService;
        }

        [HttpGet("balance")]
        public async Task<decimal> GetBalance(Guid accountId)
        {
            var balance = await accountService.GetBalance(accountId);

            return balance;
        }
    }
}
