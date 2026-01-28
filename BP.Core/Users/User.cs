using BP.Core.Shared;
using CSharpFunctionalExtensions;

namespace BP.Core.Users
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    public sealed class User : BaseEntity<Guid>
    {
        /// <summary>
        /// Хеш пароля пользователя
        /// </summary>
        public string PasswordHash { get; private set; }

        /// <summary>
        /// Почта(email) пользователя
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Для связи с оператором в приложении
        /// </summary>
        public Guid OperatorId { get; private set; }

        /// <summary>
        /// Для управления учетными записями
        /// </summary>
        public bool IsActive { get; private set; } = true;

        /// <summary>
        /// Для проверки, подтверждена почта или нет
        /// </summary>
        public bool IsEmailConfirmed { get; private set; } = false;

        /// <summary>
        /// Последний активный Ip
        /// </summary>
        public string LastAccessIp { get; private set; } = string.Empty;

        /// <summary>
        /// Устройство, с которого заходили
        /// </summary>
        public string LastActiveDevice { get; private set; } = string.Empty;

        /// <summary>
        /// Для ef core
        /// </summary>
        public User() { }
        
        /// <summary>
        /// Конструктор
        /// </summary>
        private User(Guid? id, string email, string password)
        {
            Id = id ?? Guid.NewGuid();
            Email = email;
            PasswordHash = password;
            IsActive = true;
            IsEmailConfirmed = false;
            LastAccessIp = string.Empty;
            LastActiveDevice = string.Empty;
        }

        /// <summary>
        /// Метод создания пользователя
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Result<User, string> Create(Guid? id, string email, string password)
        {
            return new User(id, email, password);
        }
        
        /// <summary>
        /// Метод добавления operatorId
        /// </summary>
        /// <param name="id"></param>
        public void AddToOperatorId(Guid id) => 
            OperatorId = id;
    }
}
