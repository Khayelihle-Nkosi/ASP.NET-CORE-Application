using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AssessmentSchema");

            migrationBuilder.CreateTable(
                name: "AccountHolders",
                schema: "AssessmentSchema",
                columns: table => new
                {
                    IdNumber = table.Column<long>(type: "bigint", maxLength: 13, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    Dob = table.Column<DateTime>(type: "date", nullable: false),
                    ResidentialAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<long>(type: "bigint", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountHolders", x => x.IdNumber);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrails",
                schema: "AssessmentSchema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<long>(type: "bigint", maxLength: 13, nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Changes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                schema: "AssessmentSchema",
                columns: table => new
                {
                    AccountNumber = table.Column<long>(type: "bigint", maxLength: 25, nullable: false),
                    AccountType = table.Column<int>(type: "int", maxLength: 55, nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AccountStatus = table.Column<bool>(type: "bit", nullable: false),
                    AvailableBalance = table.Column<long>(type: "bigint", precision: 2147483647, scale: 2, nullable: false),
                    AccountHolderId = table.Column<long>(type: "bigint", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.AccountNumber);
                });

            migrationBuilder.CreateTable(
                name: "Withdrawals",
                schema: "AssessmentSchema",
                columns: table => new
                {
                    WithdrawalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WithdrawalAmount = table.Column<long>(type: "bigint", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountNumber = table.Column<long>(type: "bigint", maxLength: 25, nullable: false),
                    AccountType = table.Column<int>(type: "int", maxLength: 55, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdrawals", x => x.WithdrawalId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountHolders_IdNumber",
                schema: "AssessmentSchema",
                table: "AccountHolders",
                column: "IdNumber");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrails_Id",
                schema: "AssessmentSchema",
                table: "AuditTrails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_AccountHolderId",
                schema: "AssessmentSchema",
                table: "BankAccounts",
                column: "AccountHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_AccountNumber",
                schema: "AssessmentSchema",
                table: "BankAccounts",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawals_WithdrawalId",
                schema: "AssessmentSchema",
                table: "Withdrawals",
                column: "WithdrawalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountHolders",
                schema: "AssessmentSchema");

            migrationBuilder.DropTable(
                name: "AuditTrails",
                schema: "AssessmentSchema");

            migrationBuilder.DropTable(
                name: "BankAccounts",
                schema: "AssessmentSchema");

            migrationBuilder.DropTable(
                name: "Withdrawals",
                schema: "AssessmentSchema");
        }
    }
}
