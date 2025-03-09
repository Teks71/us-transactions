using FluentValidation;

namespace TransactionsApp.Features.CreateTransaction;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionRequest>
{
    public CreateTransactionValidator()
    {
        RuleFor(t => t.Amount)
            .GreaterThan(0)
            .WithName("Wrong amount")
            .WithMessage("Amount must be positive.");

        RuleFor(t => t.TransactionDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithName("Wrong TransactionDate")
            .WithMessage("Transaction date cannot be in the future.");
    }
}