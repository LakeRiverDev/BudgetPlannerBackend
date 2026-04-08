using BP.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BP.Api.Controllers
{
    /// <summary>
    /// Контроллер для получения информации по балансу
    /// </summary>
    [Route("api/v1/account")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> GetBalance(Guid accountId)
        {
            var balanceResult = await accountService.GetBalance(accountId);
            if (balanceResult.IsFailure)
                return BadRequest(balanceResult.Error);

            return Ok(balanceResult.Value);
        }

        [HttpPost("limit-day")]
        public async Task<IActionResult> SetLimitPerDay(Guid accountId, decimal limit)
        {
            var newLimitResult = await accountService.SetLimitPerDay(accountId, limit);
            if (newLimitResult.IsFailure)
                return BadRequest(newLimitResult.Error);

            return Ok(newLimitResult.Value);
        }

        [HttpPost("limit-month")]
        public async Task<IActionResult> SetLimitPerMonth(Guid accountId, decimal limit)
        {
            var newLimitResult = await accountService.SetLimitPerMonth(accountId, limit);
            if (newLimitResult.IsFailure)
                return BadRequest(newLimitResult.Error);

            return Ok(newLimitResult.Value);
        }
    }
}
