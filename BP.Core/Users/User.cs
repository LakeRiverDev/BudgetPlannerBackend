namespace BP.Core.Users
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    public class User : BaseEntity<Guid>
    {
        /// <summary>
        /// Настоящее имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Хеш пароля пользователя
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Почта(email) пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Для связи с оператором в приложении
        /// </summary>
        public Guid OperatorId { get; set; }
    }
}
