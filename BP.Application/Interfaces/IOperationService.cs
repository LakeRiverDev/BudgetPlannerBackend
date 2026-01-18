using BP.Core.Operations;
using CSharpFunctionalExtensions;

namespace BP.Application.Interfaces
{
    public interface IOperationService
    {
        Task<Result<IEnumerable<Operation>, string>> GetAllOperationsByOperatorIdAsync(Guid operatorId);
        
        Task<Result<Guid, string>> AddOperationAsync(Operation operation);

        Task<Result<Guid, string>> EditOperationAsync(decimal sum, string reason, Guid operationId);
        Task<Result<Guid, string>> DeleteOperationAsync(Guid operationId);
    }
}
