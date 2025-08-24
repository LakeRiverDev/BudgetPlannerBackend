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
        private Operator(Guid userId, string name)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Name = name;
        }

        /// <summary>
        /// Метод создания оператора
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Operator Create(Guid userId, string name)
        {
            return new Operator(userId, name);
        }
    }
}
