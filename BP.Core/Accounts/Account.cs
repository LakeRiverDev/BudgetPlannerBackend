using System.Runtime.ConstrainedExecution;

namespace BP.Core.Accounts
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
        public decimal LimitPerDay { get; private set; } = 0;

        /// <summary>
        /// Лимит на месяц
        /// </summary>
        public decimal LimitPerMonth { get; private set; } = 0;

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
        public void AddToBalance(decimal sum) => Balance += sum;

        /// <summary>
        /// Уменьшить баланс 
        /// </summary>
        /// <param name="sum"></param>
        public void PutToBalance(decimal sum) => Balance -= sum;

        /// <summary>
        /// Установить лимит на день
        /// </summary>
        /// <param name="limit"></param>
        public void SetLimitPerDay(decimal limit) => LimitPerDay = limit;

        /// <summary>
        /// Установить лимит на месяц
        /// </summary>
        /// <param name="limit"></param>
        public void SetLimitPerMonth(decimal limit) => LimitPerMonth = limit;
    }
}
