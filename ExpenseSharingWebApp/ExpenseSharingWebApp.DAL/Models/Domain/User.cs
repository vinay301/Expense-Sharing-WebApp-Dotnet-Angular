using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Models.Domain
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<ExpenseSplit> ExpenseSplits { get; set; }

        //public ICollection<Group> Groups { get; set; } // Many-to-many with Group
        public ICollection<Expense> Expenses { get; set; } // Many-to-many with Expense
        public ICollection<UserBalance> UserBalances { get; set; }

    }
}
