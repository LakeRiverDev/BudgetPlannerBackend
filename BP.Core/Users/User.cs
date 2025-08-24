namespace BP.Core.Users
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    public class User : BaseEntity<Guid>
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; private set; }

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
        public Guid OperatorId { get; set; }

        /// <summary>
        /// Для управления учетными записями
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Конструктор
        /// </summary>
        private User(string login, string password, string email)
        {
            Id = Guid.NewGuid();
            Login = login;
            PasswordHash = password;
            Email = email;
            IsActive = true;
        }

        /// <summary>
        /// Метод создания пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static User Create(string login, string password, string email)
        {
            return new User(login, password, email);
        }

        /// <summary>
        /// Для ef core
        /// </summary>
        public User() { }
    }
}
