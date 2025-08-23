using BP.Core.Operations;

namespace BP.Infrastructure.Interfaces
{
    public interface IOperationRepository
    {
        public IEnumerable<Operation> GetAllOperationsByOperatorIdAsync(Guid operatorId);
        public Task AddOperationAsync(Operation operation);
        public Task<Guid> EditOperationAsync(Operation operation);
        public Task<Guid> DeleteOperationAsync(Guid operationId);
    }
}
