using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Models.DTO.Response
{
    public class GroupResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<UserGroupDto> UserGroups { get; set; }
        public List<ExpenseDto> Expenses { get; set; }
        public List<string> MemberIds { get; set; }
        public List<UserDto> Admins { get; set; }
    }
}
