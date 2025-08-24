using BP.Application.Interfaces;
using BP.Core.Operations;
using BP.Infrastructure.Interfaces;
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
        public IEnumerable<Operation> GetAllOperationsByOperatorIdAsync(Guid operatorId)
        {
            try
            {
                var operations = operationsRepository.GetAllOperationsByOperatorIdAsync(operatorId);

                return operations;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Добавить операцию в бд
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public async Task<Guid> AddOperationAsync(Operation operation)
        {
            await operationsRepository.AddOperationAsync(operation);

            if (operation.OperationType == (int)OperationType.Spending)
            {
                await accountRepository.PutToBalance(operation.Id, operation.Sum);
            }

            if (operation.OperationType == (int)OperationType.Replenishment)
            {
                await accountRepository.AddToBalance(operation.Id, operation.Sum);
            }

            return operation.Id;
        }

        /// <summary>
        /// Изменить операцию
        /// </summary>
        /// <returns></returns>
        public async Task<Guid> EditOperationAsync(decimal sum, string reason, Guid operationId)
        {
            var editOperation = await operationsRepository.EditOperationAsync(sum, reason, operationId);
            var searchEditingOperation = await operationsRepository.GetOperation(editOperation);

            if (searchEditingOperation.OperationType == (int)OperationType.Spending)
            {
                await accountRepository.PutToBalance(searchEditingOperation.Id, sum);
            }

            if (searchEditingOperation.OperationType == (int)OperationType.Replenishment)
            {
                await accountRepository.AddToBalance(searchEditingOperation.Id, sum);
            }

            return editOperation;
        }

        /// <summary>
        /// Удалить операцию в бд
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteOperationAsync(Guid operationId)
        {
            var searchEditingOperation = await operationsRepository.GetOperation(operationId);

            if (searchEditingOperation.OperationType == (int)OperationType.Spending)
            {
                await accountRepository.PutToBalance(searchEditingOperation.Id, searchEditingOperation.Sum);
            }

            if (searchEditingOperation.OperationType == (int)OperationType.Replenishment)
            {
                await accountRepository.AddToBalance(searchEditingOperation.Id, searchEditingOperation.Sum);
            }

            var deleteOperation = await operationsRepository.DeleteOperationAsync(operationId);

            return deleteOperation;
        }
    }
}
