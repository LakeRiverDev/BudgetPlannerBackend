using BP.Application.Interfaces;
using BP.Contracts;
using BP.Core.Operations;
using Microsoft.AspNetCore.Mvc;

namespace BP.Api.Controllers
{
    [Route("api/v1/operation")]
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
        public async Task<IEnumerable<Operation>> GetOperations(Guid operatorId)
        {
            var result = await operationService.GetAllOperationsByOperatorIdAsync(operatorId);
            if (result.IsFailure)
                return Enumerable.Empty<Operation>();

            return result.Value;
        }

        [HttpPost("{operatorId}")]
        public async Task<IActionResult> AddOperation(OperationDto operationDto, Guid operatorId)
        {
            var newOperation = Operation.Create(
                null,
                operationDto.Sum, 
                operationDto.Reason,
                operationDto.OperationType,
                operationDto.ReplenishmentType,
                operationDto.PaymentType, 
                operationDto.PaymentCategory, 
                operatorId);

            var result = await operationService.AddOperationAsync(newOperation.Value);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPut("{operationId}/edit")]
        public async Task<IActionResult> EditOperation(EditOperationDto editOperationDto, Guid operationId)
        {           
            var editOperation = await operationService.EditOperationAsync(
                editOperationDto.Sum, 
                editOperationDto.Reason, 
                operationId);

            if (editOperation.IsFailure)
                return BadRequest(editOperation.Error);

            return Ok(editOperation.Value);
        }

        [HttpDelete("{operationId}/delete")]
        public async Task<IActionResult> DeleteOperation(Guid operationId)
        {
            var deleteOperation = await operationService.DeleteOperationAsync(operationId);
            if (deleteOperation.IsFailure)
                return BadRequest(deleteOperation.Error);

            return Ok(deleteOperation.Value);
        }
    }
}
