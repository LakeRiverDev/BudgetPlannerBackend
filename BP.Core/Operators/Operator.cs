using BP.Core.Operations;
using BP.Core.Shared;
using CSharpFunctionalExtensions;

namespace BP.Core.Operators
{
    /// <summary>
    /// Сущность оператора
    /// </summary>
    public sealed class Operator : BaseEntity<Guid>
    {
        /// <summary>
        /// Для связи с пользователем
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Настоящее имя пользователя
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Операции пользователя
        /// </summary>
        public IReadOnlyCollection<Operation> Operations { get; private set; } = [];

        /// <summary>
        /// Для связи со счетом
        /// </summary>
        public Guid AccountId { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        private Operator(Guid? id, Guid userId, string name)
        {
            Id = id ?? Guid.NewGuid();
            UserId = userId;
            Name = name;
        }
        
        //EFCore
        public Operator(){}

        /// <summary>
        /// Метод создания оператора
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Result<Operator, string> Create(Guid? id, Guid userId, string name)
        {
            return new Operator(id, userId, name);
        }
        
        /// <summary>
        /// Метод добавления accountId
        /// </summary>
        /// <param name="id"></param>
        public void AddToAccountId(Guid id) => 
            AccountId = id;
    }
}
