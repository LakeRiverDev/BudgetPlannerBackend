using BP.DataBase.Models;

namespace BP.Application.Interfaces
{
    public interface IOperationService
    {
        Task<IEnumerable<Operation>> GetAllUserOperationsAsync(long userId);
        Task<Guid> AddOperationAsync(Operation operation);
    }
}
