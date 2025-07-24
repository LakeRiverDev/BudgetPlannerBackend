using BP.Core;

namespace BP.Application.Interfaces
{
    public interface IOperationService
    {
        Task<IEnumerable<Operation>> GetAllUserOperations(long userId);
    }
}
