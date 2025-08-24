namespace BP.Core.Users
{
    /// <summary>
    /// Описание сущности аккаунта(счета)
    /// </summary>
    public class Account : BaseEntity<Guid>
    {
        /// <summary>
        /// Для связи с другой таблицей
        /// </summary>
        public Guid OperatorId { get; private set; }

        /// <summary>
        /// Поле баланса
        /// </summary>
        public decimal Balance { get; private set; } = 0;

        /// <summary>
        /// Конструктор
        /// </summary>
        private Account(Guid operatorId)
        {
            Id = Guid.NewGuid();
            OperatorId = operatorId;
        }

        /// <summary>
        /// Метод создания аккаунта(счета)
        /// </summary>
        /// <param name="operatorId"></param>
        /// <returns></returns>
        public static Account Create(Guid operatorId)
        {
            return new Account(operatorId);
        }
    }
}
