using Duende.IdentityServer.EntityFramework.Options;
using ExpenseSharingWebApp.DAL.Models.Domain;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Data
{
    public class ExpenseSharingDbContext : ApiAuthorizationDbContext<User>
    {
        public ExpenseSharingDbContext(DbContextOptions<ExpenseSharingDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<ExpenseSplit> ExpenseSplits { get; set; }
        public DbSet<UserBalance> UserBalances { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var userID1 = Guid.NewGuid().ToString();
            var userID2 = Guid.NewGuid().ToString();

            var hasher = new PasswordHasher<User>();

            modelBuilder.Entity<User>().HasData(
                   new User
                   {
                       Name = "Admin",
                       UserName = "admin@expense.com",
                       NormalizedUserName = "admin@expense.com".ToUpper(),
                       Id = userID1,
                       Email = "admin@expense.com",
                       NormalizedEmail = "admin@expense.com".ToUpper(),
                       PasswordHash = hasher.HashPassword(null, "Admin@123")
                   },
                   new User
                   {
                       Name = "User",
                       UserName = "user@expense.com",
                       NormalizedUserName = "user@expense.com".ToUpper(),
                       Id = userID2,
                       Email = "user@expense.com",
                       NormalizedEmail = "user@expense.com".ToUpper(),
                       PasswordHash = hasher.HashPassword(null, "User@123")
                   }
               );
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN".ToUpper() },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER".ToUpper() }
            );

         
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = userID1, RoleId = "1" },
            new IdentityUserRole<string> { UserId = userID2, RoleId = "2" }
            );

            // User - Group Many-to-Many relationship
            modelBuilder.Entity<UserGroup>()
               .HasKey(ug => new { ug.UserId, ug.GroupId });

            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // Expense - User Many-to-Many relationship for SplitAmong
            modelBuilder.Entity<ExpenseSplit>()
                 .HasKey(es => new { es.UserId, es.ExpenseId });

            modelBuilder.Entity<ExpenseSplit>()
                .HasOne(es => es.User)
                .WithMany(u => u.ExpenseSplits)
                .HasForeignKey(es => es.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExpenseSplit>()
                .HasOne(es => es.Expense)
                .WithMany(e => e.ExpenseSplits)
                .HasForeignKey(es => es.ExpenseId)
                .OnDelete(DeleteBehavior.Cascade);

            //User Balance
            modelBuilder.Entity<UserBalance>()
           .HasKey(ub => ub.Id);

            modelBuilder.Entity<UserBalance>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBalances)
                .HasForeignKey(ub => ub.UserId);

            modelBuilder.Entity<UserBalance>()
                .HasOne(ub => ub.Group)
                .WithMany(g => g.UserBalances)
                .HasForeignKey(ub => ub.GroupId);

            // One-to-Many relationship between Group and Expense
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Expenses)
                .WithOne(e => e.Group)
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many relationship between User and Expense (PaidBy)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Expenses)
                .WithOne(e => e.PaidByUser)
                .HasForeignKey(e => e.PaidByUserId)
                .OnDelete(DeleteBehavior.NoAction);


        }

    }
}
