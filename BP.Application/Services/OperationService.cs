using BP.Application.Interfaces;
using BP.Core;
using BP.DataBase.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Application.Services
{
    public class OperationService : IOperationService
    {
        private readonly ILogger<OperationService> logger;
        private readonly IOperationsRepository operationsRepository;

        public OperationService(ILogger<OperationService> logger, IOperationsRepository operationsRepository)
        {
            this.logger = logger;
            this.operationsRepository = operationsRepository;
        }

        public async Task<IEnumerable<Operation>> GetAllUserOperations(long userId)
        {
            try
            {
                var operations = await operationsRepository.GetAllOperationByUserIdAsync(userId);

                return operations.Select(o=> new Operation
                {
                    
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }                        
        }        

        //public async Task<long> AddOperation(Operation operation)
        //{
        //    var operation = new Operation
        //    {

        //    };

        //    await operationsRepository.AddOperationAsync(operation);
        //}
    }
}
