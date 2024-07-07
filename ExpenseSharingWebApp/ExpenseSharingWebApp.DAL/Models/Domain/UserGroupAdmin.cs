﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Models.Domain
{
    public class UserGroupAdmin
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string GroupId { get; set; }
        public Group Group { get; set; }
    }
}
