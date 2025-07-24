using BP.DataBase.Models;

namespace BP.DataBase.Interfaces
{
    public interface IOperationsRepository
    {
        public Task<IEnumerable<Operation>> GetAllOperationByUserIdAsync(long userId);
        public Task AddOperationAsync(Operation operation);
        public Task<long> EditOperationAsync(Operation operation);
        public Task<long> DeleteOperationAsync(long operationId);
    }
}
