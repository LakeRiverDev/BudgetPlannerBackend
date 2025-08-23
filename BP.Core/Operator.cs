using BP.Core;
using BP.Core.Operations;

namespace BP.DataBase.Models
{
    public class Operator : BaseEntity<Guid>
    {
        /// <summary>
        /// Для связи с пользователем
        /// </summary>
        public Guid UserId {  get; set; }

        /// <summary>
        /// Операции пользователя
        /// </summary>
        public List<Operation> Operations { get; set; }
    }
}
