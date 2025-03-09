using TransactionsApp.Features.CreateTransaction;
using TransactionsApp.Features.GetTransaction;

namespace TransactionsApp.Endpoints;

public static class TransactionEndpointsV1
{
    /// <summary>
    /// Maps endpoints for transactions.
    /// </summary>
    /// <remarks>
    /// This extension method maps the following endpoints:
    /// <list>
    /// <item><see cref="CreateTransactionEndpoint"/></item>
    /// <item><see cref="GetTransactionEndpoint"/></item>
    /// </list>
    /// </remarks>
    /// <param name="app">The application builder.</param>
    public static void MapTransactionEndpoints(this IEndpointRouteBuilder app)
    {
        CreateTransactionEndpoint.Map(app);
        GetTransactionEndpoint.Map(app);
    }
}