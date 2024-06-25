using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Models.DTO.Request
{
    public class UpdateExpenseRequestDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }

        [Required]
        public string PaidByUserId { get; set; }

        [Required]
        public List<string> SplitWithUserIds { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
