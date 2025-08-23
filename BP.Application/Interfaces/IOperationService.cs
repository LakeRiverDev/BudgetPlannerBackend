using BP.Core.Operations;

namespace BP.Application.Interfaces
{
    public interface IOperationService
    {
        IEnumerable<Operation> GetAllOperationsByOperatorIdAsync(Guid operatorId);
        Task<Guid> AddOperationAsync(Operation operation);
        Task<Guid> DeleteOperationAsync(Guid operationId);
    }
}
