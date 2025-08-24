namespace BP.Core.Operations
{
    /// <summary>
    /// Тип операции (расход, приход д/c)
    /// </summary>    
    public enum OperationType
    {
        /// <summary>
        /// Приход
        /// </summary>
        Replenishment = 0,

        /// <summary>
        /// Расход
        /// </summary>
        Spending = 1,
    }
}
