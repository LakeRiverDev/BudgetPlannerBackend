using BP.Core.Shared;
using CSharpFunctionalExtensions;

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
        private Account(Guid? id, Guid operatorId)
        {
            Id = id ?? Guid.NewGuid();
            OperatorId = operatorId;
        }

        /// <summary>
        /// Метод создания аккаунта(счета)
        /// </summary>
        /// <param name="operatorId"></param>
        /// <returns></returns>
        public static Result<Account, string> Create(Guid? id, Guid operatorId)
        {
            return new Account(id, operatorId);
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
