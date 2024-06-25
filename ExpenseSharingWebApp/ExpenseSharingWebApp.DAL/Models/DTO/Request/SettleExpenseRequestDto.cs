using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Models.DTO.Request
{
    public class SettleExpenseRequestDto
    {
        [Required]
        public string ExpenseId { get; set; }

        [Required]
        public string SettledByUserId { get; set; }
    }
}
