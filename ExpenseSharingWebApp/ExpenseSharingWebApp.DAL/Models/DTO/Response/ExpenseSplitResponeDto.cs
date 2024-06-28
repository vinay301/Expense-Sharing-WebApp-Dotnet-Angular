using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Models.DTO.Response
{
    public class ExpenseSplitResponeDto
    {
        public string ExpenseId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string PaidByUserId { get; set; }
        public decimal UserShare { get; set; }
    }
}
