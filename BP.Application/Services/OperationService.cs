using BP.Application.Interfaces;
using BP.Core.Operations;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace BP.Application.Services
{
    public class OperationService : IOperationService
    {
        private readonly ILogger<OperationService> logger;
        private readonly IOperationRepository operationsRepository;
        private readonly IAccountRepository accountRepository;

        public OperationService(ILogger<OperationService> logger, IOperationRepository operationsRepository, IAccountRepository accountRepository)
        {
            this.logger = logger;
            this.operationsRepository = operationsRepository;
            this.accountRepository = accountRepository;
        }

        /// <summary>
        /// Получить все операции пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Result<IEnumerable<Operation>, string>> GetAllOperationsByOperatorIdAsync(Guid operatorId)
        {
            var operations = await operationsRepository.GetAllOperationsByOperatorIdAsync(operatorId);
            if (operations.IsFailure)
                return Result.Failure<IEnumerable<Operation>, string>(operations.Error);
            
            logger.LogInformation("Get all operations by operator {operatorId}", operatorId);
            
            return Result.Success<IEnumerable<Operation>, string>(operations.Value);
        }

        /// <summary>
        /// Добавить операцию в бд
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public async Task<Result<Guid, string>> AddOperationAsync(Operation operation)
        {
            await operationsRepository.AddOperationAsync(operation);

            if (operation.OperationType == (int)OperationType.Spending)
                await accountRepository.PutToBalance(operation.Id, operation.Sum);

            if (operation.OperationType == (int)OperationType.Replenishment)
                await accountRepository.AddToBalance(operation.Id, operation.Sum);
            
            logger.LogInformation("Add operation {operationId}", operation.Id);

            return operation.Id;
        }

        /// <summary>
        /// Изменить операцию
        /// </summary>
        /// <returns></returns>
        public async Task<Result<Guid, string>> EditOperationAsync(decimal sum, string reason, Guid operationId)
        {
            var editOperation = await operationsRepository.EditOperationAsync(sum, reason, operationId);
            if(editOperation.IsFailure)
                return Result.Failure<Guid, string>(editOperation.Error);
            
            var searchEditingOperation = await operationsRepository.GetOperation(editOperation.Value);
            if(searchEditingOperation.IsFailure)
                return Result.Failure<Guid, string>(searchEditingOperation.Error);

            if (searchEditingOperation.Value.OperationType == (int)OperationType.Spending)
                await accountRepository.PutToBalance(searchEditingOperation.Value.Id, sum);

            if (searchEditingOperation.Value.OperationType == (int)OperationType.Replenishment)
                await accountRepository.AddToBalance(searchEditingOperation.Value.Id, sum);
            
            logger.LogInformation("Edit operation {operationId}", editOperation.Value);

            return editOperation;
        }

        /// <summary>
        /// Удалить операцию в бд
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public async Task<Result<Guid, string>> DeleteOperationAsync(Guid operationId)
        {
            var searchEditingOperation = await operationsRepository.GetOperation(operationId);
            if (searchEditingOperation.IsFailure)
                return Result.Failure<Guid, string>(searchEditingOperation.Error);

            if (searchEditingOperation.Value.OperationType == (int)OperationType.Spending)
                await accountRepository.PutToBalance(searchEditingOperation.Value.Id, searchEditingOperation.Value.Sum);

            if (searchEditingOperation.Value.OperationType == (int)OperationType.Replenishment)
                await accountRepository.AddToBalance(searchEditingOperation.Value.Id, searchEditingOperation.Value.Sum);

            var deleteOperation = await operationsRepository.DeleteOperationAsync(operationId);
            if (deleteOperation.IsFailure)
                return Result.Failure<Guid, string>(deleteOperation.Error);
            
            logger.LogInformation("Delete operation {operationId}", deleteOperation.Value);

            return deleteOperation;
        }        
    }
}
