using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using TransactionsApp.Common.Data;
using TransactionsApp.Common.Middleware;
using TransactionsApp.Common.Repositories;
using TransactionsApp.Endpoints;
using TransactionsApp.Features.CreateTransaction;

const string SERVICE_NAME = "TransactionsApi";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<TransactionRepository>();

builder.Services.AddFluentValidationAutoValidation();
InitOpenTelemetry(builder, SERVICE_NAME);

builder.Services.AddScoped<IValidator<CreateTransactionRequest>, CreateTransactionValidator>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapTransactionEndpoints();
app.UseMiddleware<ExceptionHandlingMiddleware>();

MigrateDb(app);
app.Run();

void MigrateDb(WebApplication webApplication)
{
    using var scope = webApplication.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

void InitOpenTelemetry(WebApplicationBuilder webApplicationBuilder, string s)
{
    webApplicationBuilder.Logging.AddOpenTelemetry(options =>
    {
        options
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService(s))
            .AddConsoleExporter();
    });
    webApplicationBuilder.Services.AddOpenTelemetry()
        .ConfigureResource(resource => resource.AddService(s))
        .WithTracing(tracing => tracing
            .AddAspNetCoreInstrumentation()
            .AddConsoleExporter())
        .WithMetrics(metrics => metrics
            .AddAspNetCoreInstrumentation()
            .AddConsoleExporter());
}
