using BP.DataBase.Models;

namespace BP.DataBase.Interfaces
{
    public interface IOperationsRepository
    {
        public Task<IEnumerable<Operation>> GetAllOperationByUserIdAsync(long userId);
        public Task AddOperationAsync(Operation operation);
        public Task<Guid> EditOperationAsync(Operation operation);
        public Task<Guid> DeleteOperationAsync(Guid operationId);
    }
}
