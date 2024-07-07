using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Models.DTO
{
    public class AssignAdminsDto
    {
        public string GroupId { get; set; }
        public List<string> AdminIds { get; set; }
    }
}
