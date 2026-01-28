using BP.Core.Operations;
using CSharpFunctionalExtensions;

namespace BP.Application.Interfaces
{
    public interface IOperationRepository
    {
        Task <Result<IEnumerable<Operation>>> GetAllOperationsByOperatorIdAsync(Guid operatorId);

        Task<Result<Operation, string>> GetOperation(Guid operationId);

        Task<UnitResult<string>> AddOperationAsync(Operation operation);
        
        Task<Result<Guid, string>> EditOperationAsync(decimal sum, string reason, Guid operationId);

        Task<Result<Guid, string>> DeleteOperationAsync(Guid operationId);
    }
}
