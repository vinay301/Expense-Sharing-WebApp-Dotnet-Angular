﻿// <auto-generated />
using System;
using ExpenseSharingWebApp.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExpenseSharingWebApp.DAL.Migrations
{
    [DbContext(typeof(ExpenseSharingDbContext))]
    [Migration("20240706160045_addMultipleAdminsInGroup")]
    partial class addMultipleAdminsInGroup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes", (string)null);
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.Key", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Algorithm")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DataProtected")
                        .HasColumnType("bit");

                    b.Property<bool>("IsX509Certificate")
                        .HasColumnType("bit");

                    b.Property<string>("Use")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Use");

                    b.ToTable("Keys", (string)null);
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Key");

                    b.HasIndex("ConsumedTime");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.HasIndex("SubjectId", "SessionId", "Type");

                    b.ToTable("PersistedGrants", (string)null);
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.Expense", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsSettled")
                        .HasColumnType("bit");

                    b.Property<string>("PaidByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("PaidByUserId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.ExpenseSplit", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ExpenseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("AmountOwed")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSettled")
                        .HasColumnType("bit");

                    b.Property<string>("PaidToUserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "ExpenseId");

                    b.HasIndex("ExpenseId");

                    b.ToTable("ExpenseSplits");
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.Group", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ExpenseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "9a935770-27cb-4eb4-8662-ed625634dafe",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b35cff99-6a27-444e-abc4-9cfd9df7e3bf",
                            Email = "admin@expense.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Admin",
                            NormalizedEmail = "ADMIN@EXPENSE.COM",
                            NormalizedUserName = "ADMIN@EXPENSE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEEhBknNm46tYwLi1U2s3gKuXusWrDgnCrw+z9WJDjNVimn2zB7ixqUAzgGQrLvj3WA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ededbdb1-2de6-434d-afbd-cec2b4ffcb2a",
                            TwoFactorEnabled = false,
                            UserName = "admin@expense.com"
                        },
                        new
                        {
                            Id = "b471194d-0b50-4474-bf6e-fd24ca6606ee",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e8cca477-42dd-4449-b7fb-1038dd46d9fd",
                            Email = "user@expense.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "User",
                            NormalizedEmail = "USER@EXPENSE.COM",
                            NormalizedUserName = "USER@EXPENSE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEN2nrujeLziQbhj0S2cqoaiQYFfPH2UXXPJ0c3xMjZpuO5JbAKgjAKNjg0L7SFraDA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "47701e98-e0f5-4a2b-90d0-d4f78c42ef82",
                            TwoFactorEnabled = false,
                            UserName = "user@expense.com"
                        },
                        new
                        {
                            Id = "47ea9d2f-fbc7-40c7-8fcf-58e92e652180",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "0f3f91a4-c985-4560-9cd5-05352563ed60",
                            Email = "user2@expense.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "User2",
                            NormalizedEmail = "USER2@EXPENSE.COM",
                            NormalizedUserName = "USER2@EXPENSE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEGW69W0bXQL2WwEL2DBO61RnGwjlfyBD3KAkEHbgifRjFeZpxWdkxhzjAcKMmKgD0Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3b5fa552-1143-4b87-809f-4e02610d59f2",
                            TwoFactorEnabled = false,
                            UserName = "user2@expense.com"
                        },
                        new
                        {
                            Id = "1b450005-ad16-441f-b604-a84979c99956",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "86a9ca84-7ebe-4f6a-a28a-ee13391923ed",
                            Email = "user3@expense.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "User3",
                            NormalizedEmail = "USER3@EXPENSE.COM",
                            NormalizedUserName = "USER3@EXPENSE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEHEDmuk9v/4ElZYCc8Ao7W/ysjY+Cgmq1lCPUm6X4fmIDSsKpa8MMjXkmdB9FuDAMg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "824c09de-1251-458f-aead-38f6af2478ff",
                            TwoFactorEnabled = false,
                            UserName = "user3@expense.com"
                        },
                        new
                        {
                            Id = "1ec769fd-905c-4ccd-989c-55700ff55980",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "debb1d7b-424c-42a6-97e6-7fe9160a7ca0",
                            Email = "user4@expense.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "User4",
                            NormalizedEmail = "USER4@EXPENSE.COM",
                            NormalizedUserName = "USER4@EXPENSE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEC/gcOpZ8nhz5bs4MAkQlOr7wrG/OKefOn9nXEmSA3ktBpPrDQm0i3vnHUF37n+yqw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "51bf78e6-f517-44e6-ad1d-8ebdcd795b75",
                            TwoFactorEnabled = false,
                            UserName = "user4@expense.com"
                        },
                        new
                        {
                            Id = "1cb09f69-36d0-43ba-a2e4-00200950e87d",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "8e80eef4-ffeb-4c37-89db-d3791e008e24",
                            Email = "user5@expense.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "User5",
                            NormalizedEmail = "USER5@EXPENSE.COM",
                            NormalizedUserName = "USER5@EXPENSE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEA4m2GBHD8he2hEazwF7y6Lvv5Po/lrvaLMsQdNounVyna7/idVsC1yshfFFCyo6eA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "261b59f0-93d6-4b7e-b95c-188099db5cd6",
                            TwoFactorEnabled = false,
                            UserName = "user5@expense.com"
                        },
                        new
                        {
                            Id = "995122fe-da96-4514-8861-6e84c041dcd3",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "69d43028-488d-46a1-827d-c5cd76a053e2",
                            Email = "user6@expense.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "User6",
                            NormalizedEmail = "USER6@EXPENSE.COM",
                            NormalizedUserName = "USER6@EXPENSE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAED5BKaYhpLaHVVMU9H/wqmWalACuZZVSp7UaCV9YyvpKcWn2rFgNKkRLGeS9ucbv6w==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "c65d7845-4c2c-4b45-8a28-b06de574711a",
                            TwoFactorEnabled = false,
                            UserName = "user6@expense.com"
                        },
                        new
                        {
                            Id = "56dbabb7-da22-406c-9cd8-66577ccaeedf",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "05dcc60a-0f7e-4ad1-a919-1a9af3bcfe08",
                            Email = "user7@expense.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "User7",
                            NormalizedEmail = "USER7@EXPENSE.COM",
                            NormalizedUserName = "USER7@EXPENSE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEGYa+fCrfdSi/mzJixTSwuF/xA4dBoHLsLSKwEQJSDJGpQ1mnaaCS9nbe7S16F+F5g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d3ba4a52-ea7a-4cfb-be4f-c5b89172a291",
                            TwoFactorEnabled = false,
                            UserName = "user7@expense.com"
                        },
                        new
                        {
                            Id = "c4f26bea-4365-4d5f-9bc9-a08c52884e8b",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "063a380b-57ac-4f37-bee5-de7ec32edc58",
                            Email = "user8@expense.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "User8",
                            NormalizedEmail = "USER8@EXPENSE.COM",
                            NormalizedUserName = "USER8@EXPENSE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEPr1yv5E/8cPdIkMNNVi17imdkGtSIcosB3ZJXWNSEz3gu72QI0wlV0ej8IrVkWcgg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "78575a36-7163-4ed4-9819-f09872d2d029",
                            TwoFactorEnabled = false,
                            UserName = "user8@expense.com"
                        },
                        new
                        {
                            Id = "87760168-010b-4714-9988-e045c5e19f38",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "88fd72aa-7e05-4bc0-9518-4707bc449efd",
                            Email = "user9@expense.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "User9",
                            NormalizedEmail = "USER9@EXPENSE.COM",
                            NormalizedUserName = "USER9@EXPENSE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEObg8gVrT1bWEovpA+qffT0W5KY/2iAdM+7LSfEk2uWCqUjWplAmaNkUg1wzLDzbUg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "4fa8fe00-d259-43e0-8620-40d8bae2dccb",
                            TwoFactorEnabled = false,
                            UserName = "user9@expense.com"
                        });
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.UserBalance", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("AmountOwed")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsSettled")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserBalances");
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.UserGroup", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.UserGroupAdmin", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserGroupAdmins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "2",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "3",
                            Name = "User2",
                            NormalizedName = "USER2"
                        },
                        new
                        {
                            Id = "4",
                            Name = "User3",
                            NormalizedName = "USER3"
                        },
                        new
                        {
                            Id = "5",
                            Name = "User4",
                            NormalizedName = "USER4"
                        },
                        new
                        {
                            Id = "6",
                            Name = "User5",
                            NormalizedName = "USER5"
                        },
                        new
                        {
                            Id = "7",
                            Name = "User6",
                            NormalizedName = "USER6"
                        },
                        new
                        {
                            Id = "8",
                            Name = "User7",
                            NormalizedName = "USER7"
                        },
                        new
                        {
                            Id = "9",
                            Name = "User8",
                            NormalizedName = "USER8"
                        },
                        new
                        {
                            Id = "10",
                            Name = "User9",
                            NormalizedName = "USER9"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "9a935770-27cb-4eb4-8662-ed625634dafe",
                            RoleId = "1"
                        },
                        new
                        {
                            UserId = "b471194d-0b50-4474-bf6e-fd24ca6606ee",
                            RoleId = "2"
                        },
                        new
                        {
                            UserId = "47ea9d2f-fbc7-40c7-8fcf-58e92e652180",
                            RoleId = "3"
                        },
                        new
                        {
                            UserId = "1b450005-ad16-441f-b604-a84979c99956",
                            RoleId = "4"
                        },
                        new
                        {
                            UserId = "1ec769fd-905c-4ccd-989c-55700ff55980",
                            RoleId = "5"
                        },
                        new
                        {
                            UserId = "1cb09f69-36d0-43ba-a2e4-00200950e87d",
                            RoleId = "6"
                        },
                        new
                        {
                            UserId = "995122fe-da96-4514-8861-6e84c041dcd3",
                            RoleId = "7"
                        },
                        new
                        {
                            UserId = "56dbabb7-da22-406c-9cd8-66577ccaeedf",
                            RoleId = "8"
                        },
                        new
                        {
                            UserId = "c4f26bea-4365-4d5f-9bc9-a08c52884e8b",
                            RoleId = "9"
                        },
                        new
                        {
                            UserId = "87760168-010b-4714-9988-e045c5e19f38",
                            RoleId = "10"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.Expense", b =>
                {
                    b.HasOne("ExpenseSharingWebApp.DAL.Models.Domain.Group", "Group")
                        .WithMany("Expenses")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ExpenseSharingWebApp.DAL.Models.Domain.User", "PaidByUser")
                        .WithMany("Expenses")
                        .HasForeignKey("PaidByUserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Group");

                    b.Navigation("PaidByUser");
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.ExpenseSplit", b =>
                {
                    b.HasOne("ExpenseSharingWebApp.DAL.Models.Domain.Expense", "Expense")
                        .WithMany("ExpenseSplits")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseSharingWebApp.DAL.Models.Domain.User", "User")
                        .WithMany("ExpenseSplits")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expense");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.User", b =>
                {
                    b.HasOne("ExpenseSharingWebApp.DAL.Models.Domain.Expense", null)
                        .WithMany("SplitAmong")
                        .HasForeignKey("ExpenseId");
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.UserBalance", b =>
                {
                    b.HasOne("ExpenseSharingWebApp.DAL.Models.Domain.Group", "Group")
                        .WithMany("UserBalances")
                        .HasForeignKey("GroupId");

                    b.HasOne("ExpenseSharingWebApp.DAL.Models.Domain.User", "User")
                        .WithMany("UserBalances")
                        .HasForeignKey("UserId");

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.UserGroup", b =>
                {
                    b.HasOne("ExpenseSharingWebApp.DAL.Models.Domain.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseSharingWebApp.DAL.Models.Domain.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.UserGroupAdmin", b =>
                {
                    b.HasOne("ExpenseSharingWebApp.DAL.Models.Domain.Group", "Group")
                        .WithMany("Admins")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseSharingWebApp.DAL.Models.Domain.User", "User")
                        .WithMany("GroupsAsAdmin")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ExpenseSharingWebApp.DAL.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ExpenseSharingWebApp.DAL.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseSharingWebApp.DAL.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ExpenseSharingWebApp.DAL.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.Expense", b =>
                {
                    b.Navigation("ExpenseSplits");

                    b.Navigation("SplitAmong");
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.Group", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Expenses");

                    b.Navigation("UserBalances");

                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("ExpenseSharingWebApp.DAL.Models.Domain.User", b =>
                {
                    b.Navigation("ExpenseSplits");

                    b.Navigation("Expenses");

                    b.Navigation("GroupsAsAdmin");

                    b.Navigation("UserBalances");

                    b.Navigation("UserGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
