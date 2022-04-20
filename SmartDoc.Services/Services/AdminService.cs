using SmartDoc.Models;
using SmartDoc.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDoc.Services
{
    public class AdminService:IAdminService
    {
        private readonly IAdminRepo _adminRepo;
        public AdminService(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            return await _adminRepo.GetAllUsers();
        }

        public async Task<bool> PerformUser(CreateUserViewModel user)
        {
            return await _adminRepo.PerformUser(user);
        }
    }
}
