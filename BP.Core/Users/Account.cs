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
        public Guid OperatorId { get; set; }

        /// <summary>
        /// Поле баланса
        /// </summary>
        public decimal Balance {  get; set; } = 0;
    }
}
