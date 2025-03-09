using Microsoft.EntityFrameworkCore;

namespace TransactionsApp.Common.Data;
/// <summary>
/// Represents the application's database context.
/// </summary>
/// <remarks>
/// This context is used to interact with the database using Entity Framework Core.
/// It includes DbSet properties for each entity type.
/// </remarks>
public class AppDbContext: DbContext{
    public DbSet<TransactionDb> Transactions { get; set; }

    /// <summary>
    /// Constructor for <see cref="AppDbContext"/>.
    /// </summary>
    /// <param name="options">The options for the context.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionDb>()
            .HasIndex(t => t.Id)
            .IsUnique();
    }
}