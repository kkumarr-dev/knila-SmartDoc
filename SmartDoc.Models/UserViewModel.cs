using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDoc.Models
{
    public class UserViewModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
