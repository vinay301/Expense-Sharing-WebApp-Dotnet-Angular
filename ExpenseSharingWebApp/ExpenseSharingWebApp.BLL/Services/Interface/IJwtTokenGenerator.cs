using ExpenseSharingWebApp.DAL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.BLL.Services.Interface
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User applicationUser, IEnumerable<string> roles);
    }
}
