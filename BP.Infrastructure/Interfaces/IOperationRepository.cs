using BP.Core.Operations;

namespace BP.Infrastructure.Interfaces
{
    public interface IOperationRepository
    {
        public IEnumerable<Operation> GetAllOperationsByOperatorIdAsync(Guid operatorId);
        public Task<Operation> GetOperation(Guid operationId);
        public Task AddOperationAsync(Operation operation);
        public Task<Guid> EditOperationAsync(decimal sum, string reason, Guid operationId);
        public Task<Guid> DeleteOperationAsync(Guid operationId);
    }
}
