namespace BP.Core
{
    /// <summary>
    /// Базовая сущность, от отороый все другие сущности наследуются
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseEntity<T>
    {
        /// <summary>
        /// ИД каждой сущности
        /// </summary>
        public T Id { get; set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public BaseEntity()
        {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }
    }
}
