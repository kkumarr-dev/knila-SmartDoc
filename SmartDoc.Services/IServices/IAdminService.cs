using SmartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDoc.Services
{
    public interface IAdminService
    {
        Task<bool> PerformUser(CreateUserViewModel user);
        Task<List<UserViewModel>> GetAllUsers();
    }
}
