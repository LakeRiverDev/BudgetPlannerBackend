using BP.Api.Requests;
using BP.Application.Interfaces;
using BP.Core.Operations;
using BP.Core.Users;
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

        [HttpGet("{operatorId}")]
        public IEnumerable<Operation> GetOperations(Guid operatorId)
        {
            var result = operationService.GetAllOperationsByOperatorIdAsync(operatorId);

            return result;
        }

        [HttpPost("{operatorId}")]
        public async Task<Guid> AddOperation(OperationDto operationDto, Guid operatorId)
        {
            var newOperation = Operation.Create(operationDto.Sum, operationDto.Reason,
                operationDto.OperationType, operationDto.PaymentType, operationDto.PaymentCategory, operatorId);

            var result = await operationService.AddOperationAsync(newOperation);

            return result;
        }

        [HttpPost("{operationId}/edit")]
        public async Task<Guid> EditOperation(EditOperationDto editOperationDto, Guid operationId)
        {           
            var editOperation = await operationService.EditOperationAsync(editOperationDto.Sum, editOperationDto.Reason, operationId);

            return editOperation;
        }

        [HttpDelete("{operationId}/delete")]
        public async Task<Guid> DeleteOperation(Guid operationId)
        {
            var deleteOperation = await operationService.DeleteOperationAsync(operationId);

            return deleteOperation;
        }
    }
}
