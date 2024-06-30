using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseSharingWebApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingUserBalanceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "7e8ac24b-1a8b-455e-9c18-a99be82757c1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "f04c348f-011d-47d1-a446-c64c66150786" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e8ac24b-1a8b-455e-9c18-a99be82757c1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f04c348f-011d-47d1-a446-c64c66150786");

            migrationBuilder.AddColumn<decimal>(
                name: "AmountPaid",
                table: "UserBalances",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ExpenseId", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "35677dd4-45c9-47a9-8390-439d36192434", 0, "4b23948c-612b-4d19-9975-77b10ff23458", "user@expense.com", false, null, false, null, "User", "USER@EXPENSE.COM", "USER@EXPENSE.COM", "AQAAAAIAAYagAAAAEG0lqyExUeHpWpi99/pLFZ5+va52ntk2amIjWayPWgQ2CRImged0eRKencKoXRHebw==", null, false, "c69ef728-0b6e-4f4c-a6b7-f92cdc61d510", false, "user@expense.com" },
                    { "9a432b32-e146-4a88-8620-a428f88dfea0", 0, "aca6d413-3863-406d-b8e7-62d0e8bf93b8", "admin@expense.com", false, null, false, null, "Admin", "ADMIN@EXPENSE.COM", "ADMIN@EXPENSE.COM", "AQAAAAIAAYagAAAAEOfL/7FA662Z24qPS7LjrJNUWTR3Fij+FKglgvjhu0WkUI0ELmqgcnE1Gv8yiqcmiQ==", null, false, "a2f29044-e1a2-4586-9198-fe10a10b4882", false, "admin@expense.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "35677dd4-45c9-47a9-8390-439d36192434" },
                    { "1", "9a432b32-e146-4a88-8620-a428f88dfea0" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "35677dd4-45c9-47a9-8390-439d36192434" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "9a432b32-e146-4a88-8620-a428f88dfea0" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35677dd4-45c9-47a9-8390-439d36192434");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9a432b32-e146-4a88-8620-a428f88dfea0");

            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "UserBalances");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ExpenseId", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7e8ac24b-1a8b-455e-9c18-a99be82757c1", 0, "f5ec3f9a-2dc6-48d3-bfcc-a14dc1ecc6b4", "user@expense.com", false, null, false, null, "User", "USER@EXPENSE.COM", "USER@EXPENSE.COM", "AQAAAAIAAYagAAAAECPqZVWXL77AASGLhIg5gaBJ5C+OSzWFj6Rka/L7BMJsPjgk0u7+1V+ICJsL9aTqwg==", null, false, "5b7358b6-c34a-4c79-91ea-7f7944a7bbab", false, "user@expense.com" },
                    { "f04c348f-011d-47d1-a446-c64c66150786", 0, "f2346886-7f78-4eb2-b704-59c2ac03f109", "admin@expense.com", false, null, false, null, "Admin", "ADMIN@EXPENSE.COM", "ADMIN@EXPENSE.COM", "AQAAAAIAAYagAAAAEG23Xc8JJS0OF8yEJqv51rA7RllmMZhMPMhx9gLv8OvtunAIPDn8Mni2Eta2LnJQDw==", null, false, "82756569-5e6a-4a2c-ab4d-bf3e6c906f7a", false, "admin@expense.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "7e8ac24b-1a8b-455e-9c18-a99be82757c1" },
                    { "1", "f04c348f-011d-47d1-a446-c64c66150786" }
                });
        }
    }
}
