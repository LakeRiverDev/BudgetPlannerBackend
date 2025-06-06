namespace BP.Core
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    public class User : BaseEntity<long>
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
        /// Дата рождения пользователя
        /// </summary>
        public DateTime BirthDate { get; set; }
    }
}
