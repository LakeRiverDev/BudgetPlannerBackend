namespace BP.Core.Users
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    public class User : BaseEntity<Guid>
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
        public Guid OperatorId { get; set; }

        /// <summary>
        /// Для управления учетными записями
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Для проверки, подтверждена почта или нет
        /// </summary>
        public bool IsEmailConfirmed { get; private set; } = false;

        /// <summary>
        /// Последний активный Ip
        /// </summary>
        public string LastAccessIp { get; set; } = string.Empty;

        /// <summary>
        /// Устройство, с которого заходили
        /// </summary>
        public string LastActiveDevice { get; set; } = string.Empty;

        /// <summary>
        /// Конструктор
        /// </summary>
        private User(string email, string password)
        {
            Id = Guid.NewGuid();
            Email = email;
            PasswordHash = password;
            IsActive = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User Create(string email, string password)
        {
            return new User(email, password);
        }

        /// <summary>
        /// Для ef core
        /// </summary>
        public User() { }
    }
}
