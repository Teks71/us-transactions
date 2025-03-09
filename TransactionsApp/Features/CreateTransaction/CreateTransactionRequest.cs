using System.ComponentModel.DataAnnotations;

namespace TransactionsApp.Features.CreateTransaction;

/// <summary>
/// Represents a request to create a new transaction.
/// </summary>
public class CreateTransactionRequest
{
    /// <summary>
    ///  The unique identifier for the transaction.
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    ///  The date of the transaction.
    /// </summary>
    [Required]
    public DateTime TransactionDate { get; set; }

    /// <summary>
    ///  The amount of the transaction.
    /// </summary>
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }
}