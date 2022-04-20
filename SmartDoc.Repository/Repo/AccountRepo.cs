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
    public class AccountRepo:IAccountRepo
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
            if (user!=null)
            {
                user.Password = _securePassword.Secure(_config.Security.PasswordSalt, user.Password);
                var checkUser = await _dbContext.Users.Where(x => x.Email == user.Email && x.password == user.Password)
                    .Select(x=> new AccountUserViewModel { 
                        Email = x.Email,
                        UserId = x.UserId,
                        PhoneNumber = x.PhoneNumber,
                        RoleId = x.RoleId,
                        UserName = $"{x.FirstName} {x.LastName}"
                    })
                    .FirstOrDefaultAsync();
                return checkUser;
            }
            else
            {
                return null;
            }
        }
    }
}
