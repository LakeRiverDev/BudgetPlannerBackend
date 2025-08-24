using BP.Core.Operations;

namespace BP.Application.Interfaces
{
    public interface IOperationService
    {
        IEnumerable<Operation> GetAllOperationsByOperatorIdAsync(Guid operatorId);
        Task<Guid> AddOperationAsync(Operation operation);
        Task<Guid> EditOperationAsync(decimal sum, string reason, Guid operationId);
        Task<Guid> DeleteOperationAsync(Guid operationId);
    }
}
