using Duende.IdentityServer.EntityFramework.Options;
using ExpenseSharingWebApp.DAL.Data.Seed;
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
        private Func<DbContextOptions<ExpenseSharingDbContext>> getRequiredService;



        public ExpenseSharingDbContext(DbContextOptions<ExpenseSharingDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<ExpenseSplit> ExpenseSplits { get; set; }
        public DbSet<UserBalance> UserBalances { get; set; }
        public DbSet<UserGroupAdmin> UserGroupAdmins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var userID1 = Guid.NewGuid().ToString();
            var userID2 = Guid.NewGuid().ToString();
            var userID3 = Guid.NewGuid().ToString();
            var userID4 = Guid.NewGuid().ToString();
            var userID5 = Guid.NewGuid().ToString();
            var userID6 = Guid.NewGuid().ToString();
            var userID7 = Guid.NewGuid().ToString();
            var userID8 = Guid.NewGuid().ToString();
            var userID9 = Guid.NewGuid().ToString();
            var userID10 = Guid.NewGuid().ToString();

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
                   },
                    new User
                    {
                        Name = "User2",
                        UserName = "user2@expense.com",
                        NormalizedUserName = "user2@expense.com".ToUpper(),
                        Id = userID3,
                        Email = "user2@expense.com",
                        NormalizedEmail = "user2@expense.com".ToUpper(),
                        PasswordHash = hasher.HashPassword(null, "User@123")
                    },
                     new User
                     {
                         Name = "User3",
                         UserName = "user3@expense.com",
                         NormalizedUserName = "user3@expense.com".ToUpper(),
                         Id = userID4,
                         Email = "user3@expense.com",
                         NormalizedEmail = "user3@expense.com".ToUpper(),
                         PasswordHash = hasher.HashPassword(null, "User@123")
                     },
                      new User
                      {
                          Name = "User4",
                          UserName = "user4@expense.com",
                          NormalizedUserName = "user4@expense.com".ToUpper(),
                          Id = userID5,
                          Email = "user4@expense.com",
                          NormalizedEmail = "user4@expense.com".ToUpper(),
                          PasswordHash = hasher.HashPassword(null, "User@123")
                      },
                       new User
                       {
                           Name = "User5",
                           UserName = "user5@expense.com",
                           NormalizedUserName = "user5@expense.com".ToUpper(),
                           Id = userID6,
                           Email = "user5@expense.com",
                           NormalizedEmail = "user5@expense.com".ToUpper(),
                           PasswordHash = hasher.HashPassword(null, "User@123")
                       },
                        new User
                        {
                            Name = "User6",
                            UserName = "user6@expense.com",
                            NormalizedUserName = "user6@expense.com".ToUpper(),
                            Id = userID7,
                            Email = "user6@expense.com",
                            NormalizedEmail = "user6@expense.com".ToUpper(),
                            PasswordHash = hasher.HashPassword(null, "User@123")
                        },
                         new User
                         {
                             Name = "User7",
                             UserName = "user7@expense.com",
                             NormalizedUserName = "user7@expense.com".ToUpper(),
                             Id = userID8,
                             Email = "user7@expense.com",
                             NormalizedEmail = "user7@expense.com".ToUpper(),
                             PasswordHash = hasher.HashPassword(null, "User@123")
                         },
                          new User
                          {
                              Name = "User8",
                              UserName = "user8@expense.com",
                              NormalizedUserName = "user8@expense.com".ToUpper(),
                              Id = userID9,
                              Email = "user8@expense.com",
                              NormalizedEmail = "user8@expense.com".ToUpper(),
                              PasswordHash = hasher.HashPassword(null, "User@123")
                          },
                           new User
                           {
                               Name = "User9",
                               UserName = "user9@expense.com",
                               NormalizedUserName = "user9@expense.com".ToUpper(),
                               Id = userID10,
                               Email = "user9@expense.com",
                               NormalizedEmail = "user9@expense.com".ToUpper(),
                               PasswordHash = hasher.HashPassword(null, "User@123")
                           }
               );
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN".ToUpper() },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER".ToUpper() },
                new IdentityRole { Id = "3", Name = "User2", NormalizedName = "USER2".ToUpper() },
                new IdentityRole { Id = "4", Name = "User3", NormalizedName = "USER3".ToUpper() },
                new IdentityRole { Id = "5", Name = "User4", NormalizedName = "USER4".ToUpper() },
                new IdentityRole { Id = "6", Name = "User5", NormalizedName = "USER5".ToUpper() },
                new IdentityRole { Id = "7", Name = "User6", NormalizedName = "USER6".ToUpper() },
                new IdentityRole { Id = "8", Name = "User7", NormalizedName = "USER7".ToUpper() },
                new IdentityRole { Id = "9", Name = "User8", NormalizedName = "USER8".ToUpper() },
                new IdentityRole { Id = "10", Name = "User9", NormalizedName = "USER9".ToUpper() }
            );


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = userID1, RoleId = "1" },
            new IdentityUserRole<string> { UserId = userID2, RoleId = "2" },
            new IdentityUserRole<string> { UserId = userID3, RoleId = "3" },
            new IdentityUserRole<string> { UserId = userID4, RoleId = "4" },
            new IdentityUserRole<string> { UserId = userID5, RoleId = "5" },
            new IdentityUserRole<string> { UserId = userID6, RoleId = "6" },
            new IdentityUserRole<string> { UserId = userID7, RoleId = "7" },
            new IdentityUserRole<string> { UserId = userID8, RoleId = "8" },
            new IdentityUserRole<string> { UserId = userID9, RoleId = "9" },
            new IdentityUserRole<string> { UserId = userID10, RoleId = "10" }
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

            //UserGroupAdmins
            modelBuilder.Entity<UserGroupAdmin>()
           .HasKey(uga => new { uga.UserId, uga.GroupId });

            modelBuilder.Entity<UserGroupAdmin>()
                .HasOne(uga => uga.User)
                .WithMany(u => u.GroupsAsAdmin)
                .HasForeignKey(uga => uga.UserId);

            modelBuilder.Entity<UserGroupAdmin>()
                .HasOne(uga => uga.Group)
                .WithMany(g => g.Admins)
                .HasForeignKey(uga => uga.GroupId);

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
