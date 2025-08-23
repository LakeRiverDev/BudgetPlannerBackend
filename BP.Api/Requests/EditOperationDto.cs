using BP.Core.Operations;

namespace BP.Api.Requests
{
    public class EditOperationDto
    {
        public decimal Sum { get; set; }
        public string Reason { get; set; }
        public OperationType OperationType { get; set; }
        public PaymentCategory PaymentCategory { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
