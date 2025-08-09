namespace BP.DataBase.Models
{
    /// <summary>
    /// Тип платежа
    /// </summary>
    public class PaymentType : BaseEntity<Guid>
    {
        /// <summary>
        /// Название типа платежа(наличные, безналичные)
        /// </summary>
        public string Name { get; set; }
    }
}
