using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Models.Domain
{
    public class Group
    {
        public Group()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<UserBalance> UserBalances { get; set; }
        public ICollection<UserGroupAdmin> Admins { get; set; } = new List<UserGroupAdmin>(); // Many-to-many relationship with UserGroupAdmin

    }
}
