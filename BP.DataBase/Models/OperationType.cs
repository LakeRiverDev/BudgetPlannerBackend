namespace BP.DataBase.Models
{
    /// <summary>
    /// Тип операции (расход, приход д/c)
    /// </summary>
    public class OperationType : BaseEntity<Guid>
    {
        /// <summary>
        /// Название операции
        /// </summary>
        public string Name { get; set; }
    }
}
