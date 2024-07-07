using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseSharingWebApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addMultipleAdminsInGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "UserGroupAdmins",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupAdmins", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_UserGroupAdmins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroupAdmins_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ExpenseId", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1b450005-ad16-441f-b604-a84979c99956", 0, "86a9ca84-7ebe-4f6a-a28a-ee13391923ed", "user3@expense.com", false, null, false, null, "User3", "USER3@EXPENSE.COM", "USER3@EXPENSE.COM", "AQAAAAIAAYagAAAAEHEDmuk9v/4ElZYCc8Ao7W/ysjY+Cgmq1lCPUm6X4fmIDSsKpa8MMjXkmdB9FuDAMg==", null, false, "824c09de-1251-458f-aead-38f6af2478ff", false, "user3@expense.com" },
                    { "1cb09f69-36d0-43ba-a2e4-00200950e87d", 0, "8e80eef4-ffeb-4c37-89db-d3791e008e24", "user5@expense.com", false, null, false, null, "User5", "USER5@EXPENSE.COM", "USER5@EXPENSE.COM", "AQAAAAIAAYagAAAAEA4m2GBHD8he2hEazwF7y6Lvv5Po/lrvaLMsQdNounVyna7/idVsC1yshfFFCyo6eA==", null, false, "261b59f0-93d6-4b7e-b95c-188099db5cd6", false, "user5@expense.com" },
                    { "1ec769fd-905c-4ccd-989c-55700ff55980", 0, "debb1d7b-424c-42a6-97e6-7fe9160a7ca0", "user4@expense.com", false, null, false, null, "User4", "USER4@EXPENSE.COM", "USER4@EXPENSE.COM", "AQAAAAIAAYagAAAAEC/gcOpZ8nhz5bs4MAkQlOr7wrG/OKefOn9nXEmSA3ktBpPrDQm0i3vnHUF37n+yqw==", null, false, "51bf78e6-f517-44e6-ad1d-8ebdcd795b75", false, "user4@expense.com" },
                    { "47ea9d2f-fbc7-40c7-8fcf-58e92e652180", 0, "0f3f91a4-c985-4560-9cd5-05352563ed60", "user2@expense.com", false, null, false, null, "User2", "USER2@EXPENSE.COM", "USER2@EXPENSE.COM", "AQAAAAIAAYagAAAAEGW69W0bXQL2WwEL2DBO61RnGwjlfyBD3KAkEHbgifRjFeZpxWdkxhzjAcKMmKgD0Q==", null, false, "3b5fa552-1143-4b87-809f-4e02610d59f2", false, "user2@expense.com" },
                    { "56dbabb7-da22-406c-9cd8-66577ccaeedf", 0, "05dcc60a-0f7e-4ad1-a919-1a9af3bcfe08", "user7@expense.com", false, null, false, null, "User7", "USER7@EXPENSE.COM", "USER7@EXPENSE.COM", "AQAAAAIAAYagAAAAEGYa+fCrfdSi/mzJixTSwuF/xA4dBoHLsLSKwEQJSDJGpQ1mnaaCS9nbe7S16F+F5g==", null, false, "d3ba4a52-ea7a-4cfb-be4f-c5b89172a291", false, "user7@expense.com" },
                    { "87760168-010b-4714-9988-e045c5e19f38", 0, "88fd72aa-7e05-4bc0-9518-4707bc449efd", "user9@expense.com", false, null, false, null, "User9", "USER9@EXPENSE.COM", "USER9@EXPENSE.COM", "AQAAAAIAAYagAAAAEObg8gVrT1bWEovpA+qffT0W5KY/2iAdM+7LSfEk2uWCqUjWplAmaNkUg1wzLDzbUg==", null, false, "4fa8fe00-d259-43e0-8620-40d8bae2dccb", false, "user9@expense.com" },
                    { "995122fe-da96-4514-8861-6e84c041dcd3", 0, "69d43028-488d-46a1-827d-c5cd76a053e2", "user6@expense.com", false, null, false, null, "User6", "USER6@EXPENSE.COM", "USER6@EXPENSE.COM", "AQAAAAIAAYagAAAAED5BKaYhpLaHVVMU9H/wqmWalACuZZVSp7UaCV9YyvpKcWn2rFgNKkRLGeS9ucbv6w==", null, false, "c65d7845-4c2c-4b45-8a28-b06de574711a", false, "user6@expense.com" },
                    { "9a935770-27cb-4eb4-8662-ed625634dafe", 0, "b35cff99-6a27-444e-abc4-9cfd9df7e3bf", "admin@expense.com", false, null, false, null, "Admin", "ADMIN@EXPENSE.COM", "ADMIN@EXPENSE.COM", "AQAAAAIAAYagAAAAEEhBknNm46tYwLi1U2s3gKuXusWrDgnCrw+z9WJDjNVimn2zB7ixqUAzgGQrLvj3WA==", null, false, "ededbdb1-2de6-434d-afbd-cec2b4ffcb2a", false, "admin@expense.com" },
                    { "b471194d-0b50-4474-bf6e-fd24ca6606ee", 0, "e8cca477-42dd-4449-b7fb-1038dd46d9fd", "user@expense.com", false, null, false, null, "User", "USER@EXPENSE.COM", "USER@EXPENSE.COM", "AQAAAAIAAYagAAAAEN2nrujeLziQbhj0S2cqoaiQYFfPH2UXXPJ0c3xMjZpuO5JbAKgjAKNjg0L7SFraDA==", null, false, "47701e98-e0f5-4a2b-90d0-d4f78c42ef82", false, "user@expense.com" },
                    { "c4f26bea-4365-4d5f-9bc9-a08c52884e8b", 0, "063a380b-57ac-4f37-bee5-de7ec32edc58", "user8@expense.com", false, null, false, null, "User8", "USER8@EXPENSE.COM", "USER8@EXPENSE.COM", "AQAAAAIAAYagAAAAEPr1yv5E/8cPdIkMNNVi17imdkGtSIcosB3ZJXWNSEz3gu72QI0wlV0ej8IrVkWcgg==", null, false, "78575a36-7163-4ed4-9819-f09872d2d029", false, "user8@expense.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "4", "1b450005-ad16-441f-b604-a84979c99956" },
                    { "6", "1cb09f69-36d0-43ba-a2e4-00200950e87d" },
                    { "5", "1ec769fd-905c-4ccd-989c-55700ff55980" },
                    { "3", "47ea9d2f-fbc7-40c7-8fcf-58e92e652180" },
                    { "8", "56dbabb7-da22-406c-9cd8-66577ccaeedf" },
                    { "10", "87760168-010b-4714-9988-e045c5e19f38" },
                    { "7", "995122fe-da96-4514-8861-6e84c041dcd3" },
                    { "1", "9a935770-27cb-4eb4-8662-ed625634dafe" },
                    { "2", "b471194d-0b50-4474-bf6e-fd24ca6606ee" },
                    { "9", "c4f26bea-4365-4d5f-9bc9-a08c52884e8b" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupAdmins_GroupId",
                table: "UserGroupAdmins",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGroupAdmins");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4", "1b450005-ad16-441f-b604-a84979c99956" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6", "1cb09f69-36d0-43ba-a2e4-00200950e87d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5", "1ec769fd-905c-4ccd-989c-55700ff55980" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "47ea9d2f-fbc7-40c7-8fcf-58e92e652180" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8", "56dbabb7-da22-406c-9cd8-66577ccaeedf" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "10", "87760168-010b-4714-9988-e045c5e19f38" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7", "995122fe-da96-4514-8861-6e84c041dcd3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "9a935770-27cb-4eb4-8662-ed625634dafe" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "b471194d-0b50-4474-bf6e-fd24ca6606ee" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9", "c4f26bea-4365-4d5f-9bc9-a08c52884e8b" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1b450005-ad16-441f-b604-a84979c99956");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1cb09f69-36d0-43ba-a2e4-00200950e87d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1ec769fd-905c-4ccd-989c-55700ff55980");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "47ea9d2f-fbc7-40c7-8fcf-58e92e652180");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56dbabb7-da22-406c-9cd8-66577ccaeedf");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87760168-010b-4714-9988-e045c5e19f38");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "995122fe-da96-4514-8861-6e84c041dcd3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9a935770-27cb-4eb4-8662-ed625634dafe");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b471194d-0b50-4474-bf6e-fd24ca6606ee");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c4f26bea-4365-4d5f-9bc9-a08c52884e8b");

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
    }
}
