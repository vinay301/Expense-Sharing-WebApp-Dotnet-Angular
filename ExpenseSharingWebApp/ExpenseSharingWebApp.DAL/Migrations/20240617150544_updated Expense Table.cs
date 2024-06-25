using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseSharingWebApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedExpenseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_AspNetUsers_PaidById",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_AspNetUsers_UserId",
                table: "UserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "c5166efd-5a6b-4c38-9796-117d1a0cbf9c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "d8494d34-c635-478f-8bac-2d9836380e3b" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c5166efd-5a6b-4c38-9796-117d1a0cbf9c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d8494d34-c635-478f-8bac-2d9836380e3b");

            migrationBuilder.RenameColumn(
                name: "PaidById",
                table: "Expenses",
                newName: "PaidByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_PaidById",
                table: "Expenses",
                newName: "IX_Expenses_PaidByUserId");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "ExpenseSplits",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ExpenseSplits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ExpenseId", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "69c22871-5870-489d-afef-ca9e20e20bde", 0, "cf81dd50-d1fc-489e-b2fa-1156dc6f9ef5", "admin@expense.com", false, null, false, null, "Admin", "ADMIN@EXPENSE.COM", "ADMIN@EXPENSE.COM", "AQAAAAIAAYagAAAAEPs734gMNehPrxzNsdwgykG/iQ5YosE82p8D3SaHo3soJbinbcavL6uPTyga+ULIrA==", null, false, "eeb2ba6e-b4de-4ab7-ba77-cbb7b72c2779", false, "admin@expense.com" },
                    { "de66378c-7c48-4865-976f-75a99857f446", 0, "c14d3444-f5a1-48cc-aba2-42bde7455aa5", "user@expense.com", false, null, false, null, "User", "USER@EXPENSE.COM", "USER@EXPENSE.COM", "AQAAAAIAAYagAAAAEIGCI4f//pGRFoWLnvO5QJz9CcVwR17OijpdnXOboolnamaLO/WQ/wShB+reRb9WSw==", null, false, "8cac44e8-32c3-4e62-a3a1-d708b46a928d", false, "user@expense.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "69c22871-5870-489d-afef-ca9e20e20bde" },
                    { "2", "de66378c-7c48-4865-976f-75a99857f446" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_AspNetUsers_PaidByUserId",
                table: "Expenses",
                column: "PaidByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_AspNetUsers_UserId",
                table: "UserGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_AspNetUsers_PaidByUserId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_AspNetUsers_UserId",
                table: "UserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "69c22871-5870-489d-afef-ca9e20e20bde" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "de66378c-7c48-4865-976f-75a99857f446" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69c22871-5870-489d-afef-ca9e20e20bde");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "de66378c-7c48-4865-976f-75a99857f446");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ExpenseSplits");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ExpenseSplits");

            migrationBuilder.RenameColumn(
                name: "PaidByUserId",
                table: "Expenses",
                newName: "PaidById");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_PaidByUserId",
                table: "Expenses",
                newName: "IX_Expenses_PaidById");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ExpenseId", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "c5166efd-5a6b-4c38-9796-117d1a0cbf9c", 0, "d122be22-8f93-431a-9c02-75cbf86095ff", "user@expense.com", false, null, false, null, "User", "USER@EXPENSE.COM", "USER@EXPENSE.COM", "AQAAAAIAAYagAAAAEIKGzBAxIrv3nlYxrGKe2cY8tvFan21Mr1zi5HbrXQYH6CRspznZWjkmXUAUI6to4g==", null, false, "2ea8bd5c-26bf-497b-be5a-c36cca68ce3a", false, "user@expense.com" },
                    { "d8494d34-c635-478f-8bac-2d9836380e3b", 0, "cad09c64-604a-4ed6-b64e-4ea1559fff2d", "admin@expense.com", false, null, false, null, "Admin", "ADMIN@EXPENSE.COM", "ADMIN@EXPENSE.COM", "AQAAAAIAAYagAAAAEHV/SfYDSKP98pZwexvQ9qI0T1QcWG15l3qHCXcYW/LO9IvmVByqwMBv8lOCUdxmxg==", null, false, "48810f4d-d364-4861-92ec-3788783d842b", false, "admin@expense.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "c5166efd-5a6b-4c38-9796-117d1a0cbf9c" },
                    { "1", "d8494d34-c635-478f-8bac-2d9836380e3b" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_AspNetUsers_PaidById",
                table: "Expenses",
                column: "PaidById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_AspNetUsers_UserId",
                table: "UserGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
