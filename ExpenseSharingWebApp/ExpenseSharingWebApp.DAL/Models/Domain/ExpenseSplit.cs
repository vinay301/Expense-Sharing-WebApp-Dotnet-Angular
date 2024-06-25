using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Models.Domain
{
    public class ExpenseSplit
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string ExpenseId { get; set; }
        public Expense Expense { get; set; }
        public decimal Amount { get; set; }
    }
}
