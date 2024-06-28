using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseSharingWebApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedExpenseSplit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseSplits_AspNetUsers_UserId",
                table: "ExpenseSplits");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseSplits_Expenses_ExpenseId",
                table: "ExpenseSplits");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "05c29998-4bbb-45e4-82e4-3420f9f1415f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "8173f823-a99b-4113-af33-3a21ddac67af" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05c29998-4bbb-45e4-82e4-3420f9f1415f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8173f823-a99b-4113-af33-3a21ddac67af");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ExpenseSplits");

            migrationBuilder.AddColumn<bool>(
                name: "IsSettled",
                table: "ExpenseSplits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PaidToUserId",
                table: "ExpenseSplits",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseSplits_AspNetUsers_UserId",
                table: "ExpenseSplits",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseSplits_Expenses_ExpenseId",
                table: "ExpenseSplits",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseSplits_AspNetUsers_UserId",
                table: "ExpenseSplits");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseSplits_Expenses_ExpenseId",
                table: "ExpenseSplits");

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

            migrationBuilder.DropColumn(
                name: "IsSettled",
                table: "ExpenseSplits");

            migrationBuilder.DropColumn(
                name: "PaidToUserId",
                table: "ExpenseSplits");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "ExpenseSplits",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ExpenseId", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "05c29998-4bbb-45e4-82e4-3420f9f1415f", 0, "22d5819f-bd5b-4fac-9018-36d783376d51", "user@expense.com", false, null, false, null, "User", "USER@EXPENSE.COM", "USER@EXPENSE.COM", "AQAAAAIAAYagAAAAEGuCcY7nS0KIKaZtGitxrFf21zg/0Ue2NesN2JGgZK1DCUapWFN4dmFHc/k/EBs5MQ==", null, false, "579dec46-7263-44e0-a925-a5fe80c8a608", false, "user@expense.com" },
                    { "8173f823-a99b-4113-af33-3a21ddac67af", 0, "7a0937f2-1466-46ba-a785-48802d3d6fb2", "admin@expense.com", false, null, false, null, "Admin", "ADMIN@EXPENSE.COM", "ADMIN@EXPENSE.COM", "AQAAAAIAAYagAAAAEAvyN/43cJ5f0tsvaZq8+u77cZfACJboXghmS3SShxaO9z44U++WI0C2YZTd5XsYPQ==", null, false, "333f5cc5-baa7-43bf-a28f-c0cfac1357c3", false, "admin@expense.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "05c29998-4bbb-45e4-82e4-3420f9f1415f" },
                    { "1", "8173f823-a99b-4113-af33-3a21ddac67af" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseSplits_AspNetUsers_UserId",
                table: "ExpenseSplits",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseSplits_Expenses_ExpenseId",
                table: "ExpenseSplits",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
