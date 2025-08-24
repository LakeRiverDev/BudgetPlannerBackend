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
        /// Лимит на день
        /// </summary>
        public decimal DailyLimit { get; private set; } = 0;

        /// <summary>
        /// Лимит на месяц
        /// </summary>
        public decimal MonthlyLimit { get; private set; } = 0;

        /// <summary>
        /// Средний чек
        /// </summary>
        public decimal AverageReceipt { get; private set; } = 0;

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

        /// <summary>
        /// Добавить денег на баланс
        /// </summary>
        /// <param name="sum"></param>
        public void AddToBalance(decimal sum)
        {
            Balance += sum;
        }

        /// <summary>
        /// Уменьшить баланс 
        /// </summary>
        /// <param name="sum"></param>
        public void PutToBalance(decimal sum)
        {
            Balance -= sum;
        }

        /// <summary>
        /// Метод установки дневного лимита
        /// </summary>
        /// <param name="limit"></param>
        public void SetDailyLimit(decimal limit)
        {
            DailyLimit = limit;
        }

        /// <summary>
        /// Метод установки месячного лимита
        /// </summary>
        /// <param name="limit"></param>
        public void SetMonthlyLimit(decimal limit)
        {
            MonthlyLimit = limit;
        }
    }
}
