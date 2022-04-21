using Microsoft.EntityFrameworkCore;
using SmartDoc.Entities;
using SmartDoc.Helper;
using SmartDoc.Helper.Auth;
using SmartDoc.Models;
using SmartDoc.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDoc.Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly AppDBContext _dbContext;
        private readonly ISecurePassword _securePassword;
        private readonly AppSettingsConfig _config;
        public AccountRepo(AppDBContext dbContext, ISecurePassword securePassword, AppSettingsConfig config)
        {
            _dbContext = dbContext;
            _securePassword = securePassword;
            _config = config;
        }
        public async Task<AccountUserViewModel> GetUserData(LoginViewModel user)
        {
            if (user != null)
            {
                user.Password = _securePassword.Secure(_config.Security.PasswordSalt, user.Password);
                var checkUser = await (from u in _dbContext.Users
                                       join r in _dbContext.Roles on u.RoleId equals r.RoleId
                                       where u.Email == user.Email && u.password == user.Password
                                       select new AccountUserViewModel
                                       {
                                           Email = u.Email,
                                           UserId = u.UserId,
                                           PhoneNumber = u.PhoneNumber,
                                           RoleId = u.RoleId,
                                           UserName = $"{u.FirstName} {u.LastName}",
                                           RoleName = r.RoleName
                                       }).FirstOrDefaultAsync();
                return checkUser;
            }
            else
            {
                return null;
            }
        }
    }
}
