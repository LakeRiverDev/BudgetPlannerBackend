using BP.Core.Operations;
using BP.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BP.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий для работы с операциями денежных средств
    /// </summary>
    public class OperationRepository : IOperationRepository
    {
        private readonly ILogger<OperationRepository> logger;
        private readonly BPlannerDbContext dbContext;
        public OperationRepository(ILogger<OperationRepository> logger, BPlannerDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Получить все операции пользователя
        /// </summary>
        /// <param name="operatorId"></param>
        /// <returns></returns>
        public IEnumerable<Operation> GetAllOperationsByOperatorIdAsync(Guid operatorId)
        {
            try
            {
                var operations = dbContext.Operations.Where(o => o.OperatorId == operatorId);

                logger.LogInformation("Getting all operation on operatorId");

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
        public async Task AddOperationAsync(Operation operation)
        {
            try
            {
                await dbContext.Operations.AddAsync(operation);
                await dbContext.SaveChangesAsync();

                logger.LogInformation("Add operation");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Изменить операцию
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public async Task<Guid> EditOperationAsync(decimal sum, string reason, Guid operationId)
        {
            try
            {
                await dbContext.Operations
                    .Where(o => o.Id == operationId)
                    .ExecuteUpdateAsync(o => o
                    .SetProperty(o => o.Sum, o => sum)
                    //.SetProperty(o => o.PaymentType, o => operation.PaymentType)
                    .SetProperty(o => o.Reason, o => reason));

                logger.LogInformation("Edit operation");

                return operationId;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Удалить операцию из базы
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteOperationAsync(Guid operationId)
        {
            try
            {
                await dbContext.Operations
                    .Where(o => o.Id == operationId)
                .ExecuteDeleteAsync();

                logger.LogInformation("Delete operation");

                return operationId;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
