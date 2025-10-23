namespace BP.Contracts
{
    public class OperationDto
    {
        public decimal Sum { get; set; }
        public string Reason { get; set; }
        public int OperationType { get; set; }  
        public int ReplenishmentType { get; set; }
        public int PaymentType { get; set; }               
        public int PaymentCategory { get; set; }        
    }
}
