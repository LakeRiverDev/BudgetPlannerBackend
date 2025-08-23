using BP.Core.Operations;

namespace BP.Application.Interfaces
{
    public interface IOperationService
    {
        Task<IEnumerable<Operation>> GetAllUserOperationsAsync(Guid userId);
        Task<Guid> AddOperationAsync(Operation operation);
    }
}
