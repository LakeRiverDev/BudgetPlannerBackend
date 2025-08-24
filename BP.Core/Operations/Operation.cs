namespace BP.Core.Operations
{
    /// <summary>
    /// Описание операции(расход, приход д/c)
    /// </summary>
    public class Operation : BaseEntity<Guid>
    {
        /// <summary>
        /// Сумма операции
        /// </summary>
        public decimal Sum { get; private set; }

        /// <summary>
        /// Причина движения д/с
        /// </summary>
        public string Reason { get; private set; }

        /// <summary>
        /// Тип операции(расход, приход д/с)
        /// </summary>   
        public int OperationType { get; private set; }

        /// <summary>
        /// Тип пополнения(зарплата, отпускные, долг, и т.д)
        /// </summary>
        public int ReplenishmentType { get; private set; }

        /// <summary>
        /// Тип платежа(наличные,безналичные)
        /// </summary>
        public int PaymentType { get; private set; }

        /// <summary>
        /// Категория платежа(куда потрачены средства)
        /// </summary>
        public int PaymentCategory { get; private set; }

        /// <summary>
        /// Для связи с оператором
        /// </summary>
        public Guid OperatorId { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="reason"></param>
        /// <param name="operationType"></param>
        /// <param name="paymentType"></param>
        /// <param name="paymentCategory"></param>
        /// <param name="operatorId"></param>
        private Operation(decimal sum, string reason, int operationType, int replenishmentType, int paymentType, int paymentCategory, Guid operatorId)
        {
            Id = Guid.NewGuid();
            Sum = sum;
            Reason = reason;
            OperationType = operationType;
            ReplenishmentType = replenishmentType;
            PaymentType = paymentType;
            PaymentCategory = paymentCategory;
            OperatorId = operatorId;
        }

        /// <summary>
        /// Метод создания операции
        /// </summary>
        /// <returns></returns>
        public static Operation Create(decimal sum, string reason, int operationType, int replenishmentType, int paymentType, int paymentCategory, Guid operatorId)
        {
            if (sum == 0)
            {
                throw new Exception("Sum does not 0 or null");
            }

            return new Operation(sum, reason, operationType, replenishmentType, paymentType, paymentCategory, operatorId);
        }

    }
}
