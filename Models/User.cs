using System;
using System.Collections.Generic;

#nullable disable

namespace ECN.Models
{
    public partial class User
    {
        public string Username { get; set; }
        public int EmployeeId { get; set; }
        public string Password { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
