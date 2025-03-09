using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TransactionsApp.Common.Data;
using TransactionsApp.Common.Exceptions;

namespace TransactionsApp.Common.Repositories;

public class TransactionRepository(AppDbContext context)
{
    /// <summary>
    ///  Adds a transaction to the database if it doesn't already exist
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="ProblemDetailsException"></exception>
    public async Task<TransactionDb> AddIfNotExistsAsync(TransactionDb transaction)
    {
        try
        {
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();
            return transaction;
        }
        catch (DbUpdateException ex) when (IsDuplicateError(ex)) // We respect concurrency
        {
            var existing = await context.Transactions
                .AsNoTracking()
                .FirstAsync(t => t.Id == transaction.Id);

            return existing;
        }
        catch (DbUpdateException ex) when (IsTransactionLimitExceeded(ex))
        {
            throw new ProblemDetailsException(new ProblemDetails
                {
                    Type = "https://example.com/probs/transaction-limit-exceed",
                    Title = "Specification rule error",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "The limit on the number of stored transactions has been reached.",
                    Extensions = { { "UtcNnow", DateTime.UtcNow } }
                }
            );
        }
    }

    /// <summary>
    ///  Retrieves a transaction from the database by id with no tracking
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TransactionDb?> GetByIdNoTrackingAsync(Guid id)
    {
        return await context.Transactions
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    private bool IsDuplicateError(DbUpdateException ex)
    {
        const int SQLITE_CONSTRAINT_ERROR_CODE = 19;
        const int SQLITE_CONSTRAINT_UNIQUE_ERROR_CODE = 2067;

        return ex.InnerException is SqliteException sqlEx
               && (sqlEx.SqliteErrorCode == SQLITE_CONSTRAINT_ERROR_CODE
                   || sqlEx.SqliteErrorCode == SQLITE_CONSTRAINT_UNIQUE_ERROR_CODE);
    }

    private bool IsTransactionLimitExceeded(DbUpdateException ex)
    {
        return ex.InnerException is SqliteException sqlEx
               && sqlEx.SqliteErrorCode == 19
               && sqlEx.Message.Contains("Transaction limit exceeded");
    }
}