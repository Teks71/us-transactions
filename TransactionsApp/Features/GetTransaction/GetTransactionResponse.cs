namespace TransactionsApp.Features.GetTransaction;

public class GetTransactionResponse
{
    /// <summary>
    ///  The unique identifier of the transaction.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///  The date of the transaction.
    /// </summary>
    public DateTime TransactionDate { get; set; }

    /// <summary>
    ///  The amount of the transaction.
    /// </summary>
    public decimal Amount { get; set; }
}