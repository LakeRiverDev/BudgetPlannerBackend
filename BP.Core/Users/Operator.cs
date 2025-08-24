using BP.Core.Operations;

namespace BP.Core.Users
{
    /// <summary>
    /// Сущность оператора
    /// </summary>
    public class Operator : BaseEntity<Guid>
    {
        /// <summary>
        /// Для связи с пользователем
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Настоящее имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Операции пользователя
        /// </summary>
        public List<Operation> Operations { get; set; }

        /// <summary>
        /// Для связи со счетом
        /// </summary>
        public Guid AccountId { get; set; } = Guid.Empty;

        /// <summary>
        /// Конструктор
        /// </summary>
        public Operator(Guid userId, string name)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Name = name;
        }
    }
}
