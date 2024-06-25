using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Models.Domain
{
    public class UserBalance
    {
        public string Id { get; set; } // Primary key
        public string UserId { get; set; } // Foreign key for User
        public User User { get; set; } // Navigation property for User
        public string GroupId { get; set; } // Foreign key for Group
        public Group Group { get; set; } // Navigation property for Group
        public decimal AmountOwed { get; set; } // Balance amount (positive means owed, negative means owes)
        public bool IsSettled { get; set; } // Indicates if the balance is settled
    }
}
