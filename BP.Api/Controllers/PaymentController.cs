using BP.Application.Interfaces;
using BP.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BP.Api.Controllers
{
    [Route("api/v1/operations")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> logger;
        private readonly IOperationService operationService;

        public PaymentController(ILogger<PaymentController> logger, IOperationService operationService)
        {
            this.logger = logger;
            this.operationService = operationService;
        }

        [HttpGet("payments/{userId}")]
        public async Task<IEnumerable<Operation>> GetAllPayments(long userId)
        {
            var result = await operationService.GetAllUserOperations(userId);

            return result;
        }
    }
}
