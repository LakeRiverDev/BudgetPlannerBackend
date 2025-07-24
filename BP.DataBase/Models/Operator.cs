namespace BP.DataBase.Models
{
    public class Operator : BaseEntity<long>
    {
        /// <summary>
        /// Для связи с пользователем
        /// </summary>
        public long UserId {  get; set; }

        /// <summary>
        /// Операции пользователя
        /// </summary>
        public List<Operation> Operations { get; set; }
    }
}
