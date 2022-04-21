using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDoc.Models.Account
{
    public class AccountUserViewModel
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
