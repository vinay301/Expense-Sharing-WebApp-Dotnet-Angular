using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Models.DTO.Request
{
    public class CreateGroupRequestDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public List<string> MemberIds { get; set; }
        public List<string> AdminIds { get; set; }
    }
}
