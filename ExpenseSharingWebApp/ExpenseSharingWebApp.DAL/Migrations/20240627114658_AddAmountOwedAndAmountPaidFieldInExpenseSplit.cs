using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseSharingWebApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAmountOwedAndAmountPaidFieldInExpenseSplit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "1bd73b97-baaf-48bd-b8c1-2524d0363e8b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "25c7ff13-c64f-48b7-b5b5-742decee58d8" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1bd73b97-baaf-48bd-b8c1-2524d0363e8b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "25c7ff13-c64f-48b7-b5b5-742decee58d8");

            migrationBuilder.AddColumn<decimal>(
                name: "AmountOwed",
                table: "ExpenseSplits",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountPaid",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "AmountOwed",
                table: "ExpenseSplits");

            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "ExpenseSplits");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ExpenseId", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1bd73b97-baaf-48bd-b8c1-2524d0363e8b", 0, "c92c2d62-b965-4e60-9355-f37f7e71a40f", "user@expense.com", false, null, false, null, "User", "USER@EXPENSE.COM", "USER@EXPENSE.COM", "AQAAAAIAAYagAAAAEHJdgzmp/nvjasq8AzuWArw6UOllv/9+mDti1u6m30vIl/P74Hgm8qY7MeWr2I3sjg==", null, false, "7a62dc14-c548-4087-bcd6-be01284e44d6", false, "user@expense.com" },
                    { "25c7ff13-c64f-48b7-b5b5-742decee58d8", 0, "0a42bf25-91f4-4998-b95a-ddb33ef24969", "admin@expense.com", false, null, false, null, "Admin", "ADMIN@EXPENSE.COM", "ADMIN@EXPENSE.COM", "AQAAAAIAAYagAAAAENmdfs7DoaxkThJYmwNb7WAZhqPtxnPtlg5RhdQ8vkBJn93RLWhI9Pz0wT7Cwhe35g==", null, false, "be5d40f5-3cd8-4803-813f-b8531e1b4e38", false, "admin@expense.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "1bd73b97-baaf-48bd-b8c1-2524d0363e8b" },
                    { "1", "25c7ff13-c64f-48b7-b5b5-742decee58d8" }
                });
        }
    }
}
