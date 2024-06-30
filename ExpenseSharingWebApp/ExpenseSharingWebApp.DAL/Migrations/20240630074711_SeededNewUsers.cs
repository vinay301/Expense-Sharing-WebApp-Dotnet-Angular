using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseSharingWebApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeededNewUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "10", null, "User9", "USER9" },
                    { "3", null, "User2", "USER2" },
                    { "4", null, "User3", "USER3" },
                    { "5", null, "User4", "USER4" },
                    { "6", null, "User5", "USER5" },
                    { "7", null, "User6", "USER6" },
                    { "8", null, "User7", "USER7" },
                    { "9", null, "User8", "USER8" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ExpenseId", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2d421861-5a7a-47e9-a42a-82e290b54787", 0, "9c3a1b96-eb92-4971-b096-58dd53332717", "user7@expense.com", false, null, false, null, "User7", "USER7@EXPENSE.COM", "USER7@EXPENSE.COM", "AQAAAAIAAYagAAAAEBliMIuhly9kPTcZq3CBbZUfQ8NivbBRb/0HZ1T0VcgNyuHJp1bOtixeC65j14r4mQ==", null, false, "b62653be-a8aa-4942-8427-393d2d14cf44", false, "user7@expense.com" },
                    { "6683d7b5-e6db-41d8-b824-9f571281fd93", 0, "7b066392-3a3b-494e-8d0a-9915ee48812b", "user8@expense.com", false, null, false, null, "User8", "USER8@EXPENSE.COM", "USER8@EXPENSE.COM", "AQAAAAIAAYagAAAAEFfDF5+Cp0DoKqrCE+AM5U22RAwb0tr6K9lwlOBGiCNu4kprCD6+Vl7vLhXrUADpOA==", null, false, "488f1e9d-cad2-4093-8913-e8563c18cb8f", false, "user8@expense.com" },
                    { "6aaecb55-2f49-408b-9802-017a935f5a4b", 0, "0a2824df-866b-4677-a3f3-7aadb90833a2", "user6@expense.com", false, null, false, null, "User6", "USER6@EXPENSE.COM", "USER6@EXPENSE.COM", "AQAAAAIAAYagAAAAEP8jiTYMwwEG3csgvAWICcG5XtehDFXtpHoPzmkIb9QWmjjoU+nB5tPnAm3tScMpcg==", null, false, "4a47a2a9-607c-4b11-9f00-dec5a9d49261", false, "user6@expense.com" },
                    { "93dd512a-603d-425d-9b0c-b7180b054163", 0, "c639ef07-a55d-45b0-8130-84574b531920", "admin@expense.com", false, null, false, null, "Admin", "ADMIN@EXPENSE.COM", "ADMIN@EXPENSE.COM", "AQAAAAIAAYagAAAAEF2IzMsoBA9nbeVonAIxvE60T73W1HO0/TtJwYCb6s8dEzFCRKWZlKfoeGes5jnMvw==", null, false, "d96d7a33-dfa5-41bb-bde8-3510142ba939", false, "admin@expense.com" },
                    { "bc7000b9-8cbf-4129-9aa2-e7a7156def1b", 0, "b60fddeb-e86e-412d-a509-dde3c93d6425", "user5@expense.com", false, null, false, null, "User5", "USER5@EXPENSE.COM", "USER5@EXPENSE.COM", "AQAAAAIAAYagAAAAEJAfmpjQ0DZHECIA/3Zji1h+X+pkBdHB32OJc7ST2ZSiXyzg2PSJ8rGpvPIVSuXOpQ==", null, false, "b77c294b-aedc-4e96-8127-525b77c123ce", false, "user5@expense.com" },
                    { "c6695c36-d7b3-4b34-bea9-680c584c222f", 0, "48d8e08a-355b-49d6-a76d-d405bfd9ccc6", "user9@expense.com", false, null, false, null, "User9", "USER9@EXPENSE.COM", "USER9@EXPENSE.COM", "AQAAAAIAAYagAAAAECGh66IljAcMbWqEMyWvNlQ+GCsXhbzEdlkbU5b4mE6laM6dWvtjBAmEtmBGX3hb4g==", null, false, "3235cde8-a155-4a91-9283-2cd49861c057", false, "user9@expense.com" },
                    { "daecf59e-a22c-4851-9f0e-d087e0ae6d6d", 0, "7e464bf3-310d-4c5a-b2b2-c2a1b1073191", "user3@expense.com", false, null, false, null, "User3", "USER3@EXPENSE.COM", "USER3@EXPENSE.COM", "AQAAAAIAAYagAAAAENyzpys9mC1AFS/gnt4wxVbMffNO5bphEnPRMgG4npwAscub6yxupbon/kOPxx7LPw==", null, false, "1b6a57da-d5a9-4dcb-8979-b63519d52827", false, "user3@expense.com" },
                    { "e0d979ea-e393-444c-aaf1-03184175a857", 0, "aed86861-ba2e-4d96-9d24-b15914d5f88e", "user4@expense.com", false, null, false, null, "User4", "USER4@EXPENSE.COM", "USER4@EXPENSE.COM", "AQAAAAIAAYagAAAAEOKYoDTr0Mp3hbJzExWB8EJfWGYIcLAM1hlHGdVwIiUKY+IAJAemq6bZwxCAoQkYAw==", null, false, "6aafcbee-0b4c-4d8c-8a90-2bf4b36bb3fe", false, "user4@expense.com" },
                    { "f5368239-3d17-4195-bb6b-606f7c5a70c3", 0, "ccbfcdb8-9327-42e5-9fc0-5eb8359f3156", "user2@expense.com", false, null, false, null, "User2", "USER2@EXPENSE.COM", "USER2@EXPENSE.COM", "AQAAAAIAAYagAAAAEN0CcfBrVd5qkCcKRrJ+bdOMpC2pxVY8dpjy84LcnGo0ooD3TOgbQO2zhP4t3d5SMQ==", null, false, "6cd7e1f2-48ea-45a7-94b9-e89052442ee4", false, "user2@expense.com" },
                    { "f6fc62d8-ff1f-40a1-8c00-595fc5ac4b02", 0, "03a90066-3a71-4f9d-8da1-c168be9c3d8b", "user@expense.com", false, null, false, null, "User", "USER@EXPENSE.COM", "USER@EXPENSE.COM", "AQAAAAIAAYagAAAAEA+yBcwECMsQ1ZRj601sAAooi8aVwf2dEs0AkFHsEK3a6P3Vwgh3OT5nxsBTd0CyFg==", null, false, "9ecf3029-532d-4398-a914-c96e45d90d37", false, "user@expense.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "8", "2d421861-5a7a-47e9-a42a-82e290b54787" },
                    { "9", "6683d7b5-e6db-41d8-b824-9f571281fd93" },
                    { "7", "6aaecb55-2f49-408b-9802-017a935f5a4b" },
                    { "1", "93dd512a-603d-425d-9b0c-b7180b054163" },
                    { "6", "bc7000b9-8cbf-4129-9aa2-e7a7156def1b" },
                    { "10", "c6695c36-d7b3-4b34-bea9-680c584c222f" },
                    { "4", "daecf59e-a22c-4851-9f0e-d087e0ae6d6d" },
                    { "5", "e0d979ea-e393-444c-aaf1-03184175a857" },
                    { "3", "f5368239-3d17-4195-bb6b-606f7c5a70c3" },
                    { "2", "f6fc62d8-ff1f-40a1-8c00-595fc5ac4b02" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8", "2d421861-5a7a-47e9-a42a-82e290b54787" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9", "6683d7b5-e6db-41d8-b824-9f571281fd93" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7", "6aaecb55-2f49-408b-9802-017a935f5a4b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "93dd512a-603d-425d-9b0c-b7180b054163" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6", "bc7000b9-8cbf-4129-9aa2-e7a7156def1b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "10", "c6695c36-d7b3-4b34-bea9-680c584c222f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4", "daecf59e-a22c-4851-9f0e-d087e0ae6d6d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5", "e0d979ea-e393-444c-aaf1-03184175a857" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "f5368239-3d17-4195-bb6b-606f7c5a70c3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "f6fc62d8-ff1f-40a1-8c00-595fc5ac4b02" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d421861-5a7a-47e9-a42a-82e290b54787");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6683d7b5-e6db-41d8-b824-9f571281fd93");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6aaecb55-2f49-408b-9802-017a935f5a4b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93dd512a-603d-425d-9b0c-b7180b054163");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc7000b9-8cbf-4129-9aa2-e7a7156def1b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6695c36-d7b3-4b34-bea9-680c584c222f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "daecf59e-a22c-4851-9f0e-d087e0ae6d6d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e0d979ea-e393-444c-aaf1-03184175a857");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5368239-3d17-4195-bb6b-606f7c5a70c3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f6fc62d8-ff1f-40a1-8c00-595fc5ac4b02");

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
    }
}
