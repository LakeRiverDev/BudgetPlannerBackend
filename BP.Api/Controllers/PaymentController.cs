using BP.Api.Requests;
using BP.Application.Interfaces;
using BP.DataBase.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IEnumerable<Operation>> GetPayments(long userId)
        {
            var result = await operationService.GetAllUserOperationsAsync(userId);

            return result;
        }

        [HttpPost("payments")]
        public async Task<Guid> AddPaymentOperation(OperationDto operationDto)
        {
            var newOperation = new Operation
            {             
                Id = Guid.NewGuid(),
                DateOperation = DateTime.UtcNow,
                OperationType = operationDto.OperationType,
                OperationTypeId = operationDto.OperationTypeId,
                PaymentType = operationDto.PaymentType,
                PaymentTypeId = operationDto.PaymentTypeId,
                Reason = operationDto.Reason,
                Sum = operationDto.Sum,
            };

            var result = await operationService.AddOperationAsync(newOperation);

            return result;
        }
    }
}
