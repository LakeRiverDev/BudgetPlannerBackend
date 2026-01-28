using BP.Application.Interfaces;
using BP.Core.Operations;
using CSharpFunctionalExtensions;
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
        public async Task<Result<IEnumerable<Operation>>> GetAllOperationsByOperatorIdAsync(Guid operatorId)
        {
            var operations = await dbContext.Operations
                .AsNoTracking()
                .Where(o => o.OperatorId == operatorId)
                .ToListAsync();
            
            logger.LogInformation("Get all operations by operator {operatorId}", operatorId);
            
            return operations;
        }

        /// <summary>
        /// Получить одну операцию
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public async Task<Result<Operation, string>> GetOperation(Guid operationId)
        {
            var operation = await dbContext.Operations
                .Where(o => o.Id == operationId)
                .FirstOrDefaultAsync();
            
            if (operation == null)
                return Result.Failure<Operation, string>("Operation not found");
            
            logger.LogInformation("Get operation {operationId}", operationId);

            return operation;
        }

        /// <summary>
        /// Добавить операцию в бд
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public async Task<UnitResult<string>> AddOperationAsync(Operation operation)
        {
            await dbContext.Operations.AddAsync(operation);
            
            try
            {
                await dbContext.SaveChangesAsync();

                logger.LogInformation("Add operation");
                
                return "Operation added";
            }
            catch (Exception ex)
            {
                return  Result.Failure<string>(ex.Message);
            }
        }

        /// <summary>
        /// Изменить операцию
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public async Task<Result<Guid, string>> EditOperationAsync(decimal sum, string reason, Guid operationId)
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
                return  Result.Failure<Guid, string>(ex.Message);
            }
        }

        /// <summary>
        /// Удалить операцию из базы
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public async Task<Result<Guid, string>> DeleteOperationAsync(Guid operationId)
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
                return  Result.Failure<Guid, string>(ex.Message);
            }
        }
    }
}
