using BP.Core.Operations;

namespace BP.DataBase.Interfaces
{
    public interface IOperationRepository
    {
        public Task<IEnumerable<Operation>> GetAllOperationsByUserIdAsync(Guid operatorId);
        public Task AddOperationAsync(Operation operation);
        public Task<Guid> EditOperationAsync(Operation operation);
        public Task<Guid> DeleteOperationAsync(Guid operationId);
    }
}
