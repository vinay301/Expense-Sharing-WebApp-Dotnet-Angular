using ExpenseSharingWebApp.DAL.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Data.Seed
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //using (var context = new ExpenseSharingDbContext(serviceProvider.GetRequiredService<DbContextOptions<ExpenseSharingDbContext>>))
            //{
            //    var hasher = new PasswordHasher<User>();

            //    // Add roles if not already present
            //    if (!context.Roles.Any())
            //    {
            //        context.Roles.AddRange(
            //            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            //            new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
            //        );
            //    }

            //    // Add users if not already present
            //    if (!context.Users.Any())
            //    {
            //        var userID1 = Guid.NewGuid().ToString();
            //        var userID2 = Guid.NewGuid().ToString();

            //        context.Users.AddRange(
            //            new User
            //            {
            //                Name = "Admin",
            //                UserName = "admin@expense.com",
            //                NormalizedUserName = "ADMIN@EXPENSE.COM",
            //                Id = userID1,
            //                Email = "admin@expense.com",
            //                NormalizedEmail = "ADMIN@EXPENSE.COM",
            //                PasswordHash = hasher.HashPassword(null, "Admin@123")
            //            },
            //            new User
            //            {
            //                Name = "User",
            //                UserName = "user@expense.com",
            //                NormalizedUserName = "USER@EXPENSE.COM",
            //                Id = userID2,
            //                Email = "user@expense.com",
            //                NormalizedEmail = "USER@EXPENSE.COM",
            //                PasswordHash = hasher.HashPassword(null, "User@123")
            //            }
            //        );

            //        // Assign roles to users
            //        context.UserRoles.AddRange(
            //            new IdentityUserRole<string> { UserId = userID1, RoleId = "1" },
            //            new IdentityUserRole<string> { UserId = userID2, RoleId = "2" }
            //        );
            //    }

            //    context.SaveChanges();
            //}
        }
    }
}
