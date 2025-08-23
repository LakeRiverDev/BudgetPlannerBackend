namespace BP.Core.Operations;

/// <summary>
/// Описание операции(расход, приход д/c)
/// </summary>
public class Operation : BaseEntity<Guid>
{
    /// <summary>
    /// Сумма операции
    /// </summary>
    public decimal Sum { get; set; }

    /// <summary>
    /// Причина движения д/с
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// Дата происхождения движения д/с
    /// </summary>
    public DateTime DateOperation { get; set; }

    /// <summary>
    /// Тип операции(расход, приход д/с)
    /// </summary>   
    public OperationType OperationType { get; set; }

    /// <summary>
    /// Тип платежа
    /// </summary>
    public PaymentType? PaymentType { get; set; }

    /// <summary>
    /// Категория операции
    /// </summary>
    public PaymentCategory? PaymentCategory { get; set; }

    /// <summary>
    /// Для связи с оператором
    /// </summary>
    public Guid? OperatorId { get; set; } = Guid.Empty;
}
