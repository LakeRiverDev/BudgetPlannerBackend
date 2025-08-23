using BP.Core.Operations;

namespace BP.Api.Requests
{
    public class OperationDto
    {
        public decimal Sum { get; set; }

        public string Reason { get; set; }

        public long OperationTypeId { get; set; }

        public OperationType OperationType { get; set; }

        public long PaymentTypeId { get; set; }

        public PaymentType PaymentType { get; set; }        
    }
}
