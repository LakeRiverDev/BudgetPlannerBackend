using BP.Application.Interfaces;
using BP.DataBase.Interfaces;
using BP.DataBase.Models;
using Microsoft.Extensions.Logging;

namespace BP.Application.Services
{
    public class OperationService : IOperationService
    {
        private readonly ILogger<OperationService> logger;
        private readonly IOperationsRepository operationsRepository;

        public OperationService(ILogger<OperationService> logger, IOperationsRepository operationsRepository)
        {
            this.logger = logger;
            this.operationsRepository = operationsRepository;
        }

        /// <summary>
        /// Получить все операции пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Operation>> GetAllUserOperationsAsync(long userId)
        {
            try
            {
                var operations = await operationsRepository.GetAllOperationByUserIdAsync(userId);

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
    }
}
