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

        public OperationService(ILogger<OperationService> logger, IOperationRepository operationsRepository)
        {
            this.logger = logger;
            this.operationsRepository = operationsRepository;
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

            return operation.Id;
        }

        /// <summary>
        /// Удалить операцию в бд
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteOperationAsync(Guid operationId)
        {
            var deleteOperation = await operationsRepository.DeleteOperationAsync(operationId);

            return deleteOperation;
        }
    }
}
