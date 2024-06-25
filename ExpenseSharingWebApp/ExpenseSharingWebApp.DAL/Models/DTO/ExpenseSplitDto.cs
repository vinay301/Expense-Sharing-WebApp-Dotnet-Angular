using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Models.DTO
{
    public class ExpenseSplitDto
    {
        public string UserId { get; set; }
        public UserDto User { get; set; }
        public decimal Amount { get; set; }
    }
}
