using BP.DataBase.Models;

namespace BP.Core
{
    public class Operation
    {
        /// <summary>
        /// Сумма операции
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Причина движения д/с
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Дата происхождения движения д/с
        /// </summary>
        public DateTime DateOperation { get; set; }

        /// <summary>
        /// Тип операции(расход, приход д/с)
        /// </summary>
        public long OperationTypeId { get; set; }

        public OperationType OperationType { get; set; }

        /// <summary>
        /// Тип платежа
        /// </summary>
        public long PaymentTypeId { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}
