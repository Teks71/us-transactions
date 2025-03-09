using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionsApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionLimitTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // We respect concurrency
            migrationBuilder.Sql(@"
            CREATE TRIGGER EnforceTransactionLimit
            BEFORE INSERT ON Transactions
            BEGIN
                SELECT CASE
                    WHEN (SELECT COUNT(*) FROM Transactions) > 100
                    THEN RAISE(ABORT, 'Transaction limit exceeded')
                END;
            END;
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER EnforceTransactionLimit;");
        }
    }
}
