using ExpenseSharingWebApp.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Models.Domain
{
    public class Expense
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public User PaidByUser { get; set; }
        public string PaidByUserId { get; set; }
        public ICollection<ExpenseSplit> ExpenseSplits { get; set; }
        public ICollection<User> SplitAmong { get; set; } //many-many with user
        public bool IsSettled { get; set; }

        public string GroupId { get; set; } // Foreign key for Group
        public Group Group { get; set; } // Navigation property
    }
}
