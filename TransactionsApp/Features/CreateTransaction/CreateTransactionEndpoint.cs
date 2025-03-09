using System.Runtime.InteropServices;
using FluentValidation;
using TransactionsApp.Common.Data;
using TransactionsApp.Common.Repositories;

namespace TransactionsApp.Features.CreateTransaction;

public class CreateTransactionEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/Transaction",
                async (CreateTransactionRequest request, TransactionRepository repository,
                    IValidator<CreateTransactionRequest> validator, HttpContext context) =>
                {   
                    var validationResult = await validator.ValidateAsync(request);
                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToArray());

                        return Results.ValidationProblem(errors, instance: context.Request.Path);
                    }
                    var transaction = new TransactionDb
                    {
                        Id = request.Id,
                        TransactionDate = request.TransactionDate,
                        Amount = request.Amount,
                        InsertDateTime = DateTime.UtcNow // job for insert trigger on database
                    };
                    var result = await repository.GetByIdNoTrackingAsync(request.Id)
                                 ?? await repository.AddIfNotExistsAsync(transaction);
                    var response = new CreateTransactionResponse { InsertDateTime = result.InsertDateTime };
                    return Results.Ok(response);
                })
            .WithName("CreateTransaction")
            .WithOpenApi();
    }
}