using System.ComponentModel.DataAnnotations;

namespace TransactionsApp.Common.Data;

/// <summary>
/// Represents a financial transaction.
/// </summary>
/// <remarks>
/// Each transaction has a unique identifier, a date, and an amount.
/// </remarks>
public record TransactionDb
{
    /// <summary>
    /// Unique identifier for the transaction.
    /// </summary>
    /// <value>A <see cref="Guid"/>.</value>
    [Key] 
    public Guid Id { get; set; }

    /// <summary>
    /// Date of the transaction.
    /// </summary>
    /// <value>A <see cref="DateTime"/>.</value>
    [Required] 
    public DateTime TransactionDate { get; set; }

    /// <summary>
    /// Amount of the transaction.
    /// </summary>
    /// <value>A <see cref="decimal"/>.</value>
    [Required] 
    public decimal Amount { get; set; }

    /// <summary>
    /// Date and time when the transaction was inserted into the database.
    /// </summary>
    /// <value>A <see cref="DateTime"/>.</value>
    [Required] 
    public DateTime InsertDateTime { get; set; }
}