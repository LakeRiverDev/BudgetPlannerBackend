using BP.DataBase.Interfaces;
using BP.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BP.DataBase.Repositories
{
    /// <summary>
    /// Репозиторий для работы с операциями денежных средств
    /// </summary>
    public class OperationsRepository : IOperationsRepository
    {
        private readonly ILogger<OperationsRepository> logger;
        private readonly BPlannerDbContext dbContext;
        public OperationsRepository(ILogger<OperationsRepository> logger, BPlannerDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Получить все операции пользователя
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Operation>> GetAllOperationByUserIdAsync(long userId)
        {
            try
            {
                var operations = await dbContext.Operations.ToListAsync();                

                logger.LogInformation("Repository OK");

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

                logger.LogInformation("Repository OK");
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
        public async Task<Guid> EditOperationAsync(Operation operation)
        {
            try
            {
                await dbContext.Operations
                .Where(o => o.Id == operation.Id)
                .ExecuteUpdateAsync(o => o
                .SetProperty(o => o.Sum, o => operation.Sum)
                .SetProperty(o => o.PaymentTypeId, o => operation.PaymentTypeId)
                .SetProperty(o => o.Reason, o => operation.Reason)
                .SetProperty(o => o.DateOperation, o => operation.DateOperation));

                logger.LogInformation("Repository OK");

                return operation.Id;
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

                logger.LogInformation("Repository OK");

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
