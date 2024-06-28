using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Models.DTO.Response
{
    public class ExpenseResponseDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public UserDto PaidByUser { get; set; }
        public bool IsSettled { get; set; }
        public string GroupId { get; set; }
        public ICollection<ExpenseSplitResponeDto> ExpenseSplits { get; set; }
    }
}
