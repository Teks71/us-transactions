using TransactionsApp.Common.Data;
using TransactionsApp.Common.Repositories;

namespace TransactionsApp.Features.GetTransaction;

public class GetTransactionEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/Transaction", async (Guid id, TransactionRepository repository) =>
            {
                TransactionDb? transaction = await repository.GetByIdNoTrackingAsync(id);
                var response = new GetTransactionResponse();
                if (transaction != null)
                {
                    response.Id = transaction.Id;
                    response.TransactionDate = transaction.TransactionDate;
                    response.Amount = transaction.Amount;
                }
                return transaction is not null ? Results.Ok(response) : Results.NotFound();
            })
            .WithName("GetTransaction")
            .WithOpenApi();
    }
}