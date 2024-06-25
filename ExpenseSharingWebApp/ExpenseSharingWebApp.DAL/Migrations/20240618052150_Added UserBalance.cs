using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseSharingWebApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "UserBalances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AmountOwed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsSettled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBalances_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserBalances_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserBalances_GroupId",
                table: "UserBalances",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBalances_UserId",
                table: "UserBalances",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBalances");

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
        }
    }
}
