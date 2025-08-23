using BP.Api.Requests;
using BP.Application.Interfaces;
using BP.Core.Operations;
using Microsoft.AspNetCore.Mvc;

namespace BP.Api.Controllers
{
    [Route("api/v1/operations")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly ILogger<OperationController> logger;
        private readonly IOperationService operationService;

        public OperationController(ILogger<OperationController> logger, IOperationService operationService)
        {
            this.logger = logger;
            this.operationService = operationService;
        }

        [HttpGet("payments/{operatorId}")]
        public async Task<IEnumerable<Operation>> GetOperations(Guid operatorId)
        {
            var result = await operationService.GetAllOperationsByUserIdAsync(operatorId);

            return result;
        }

        [HttpPost("payments/{operatorId}")]
        public async Task<Guid> AddOperation(OperationDto operationDto, Guid operatorId)
        {
            var newOperation = new Operation
            {
                Id = Guid.NewGuid(),
                DateOperation = DateTime.UtcNow,
                OperationType = operationDto.OperationType,
                PaymentType = operationDto.PaymentType,
                Reason = operationDto.Reason,
                Sum = operationDto.Sum,
                OperatorId = operatorId
            };

            var result = await operationService.AddOperationAsync(newOperation);

            return result;
        }
    }
}
