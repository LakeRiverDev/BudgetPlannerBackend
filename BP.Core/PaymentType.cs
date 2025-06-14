﻿namespace BP.Core
{
    /// <summary>
    /// Тип платежа
    /// </summary>
    public class PaymentType : BaseEntity<long>
    {
        /// <summary>
        /// Название типа платежа(наличные, безналичные)
        /// </summary>
        public string Name { get;set; }
    }
}
