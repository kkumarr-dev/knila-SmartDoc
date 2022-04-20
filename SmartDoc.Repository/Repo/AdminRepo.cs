using Microsoft.EntityFrameworkCore;
using SmartDoc.Entities;
using SmartDoc.Helper;
using SmartDoc.Helper.Auth;
using SmartDoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDoc.Repository
{
    public class AdminRepo : IAdminRepo
    {
        private readonly AppDBContext _dbContext;
        private readonly AppSettingsConfig _config;
        private readonly ISecurePassword _securePassword;
        public AdminRepo(AppDBContext dbContext, AppSettingsConfig config, ISecurePassword securePassword)
        {
            _dbContext = dbContext;
            _config = config;
            _securePassword = securePassword;
        }
        public async Task<List<UserViewModel>> GetAllUsers()
        {
            var data = await (from u in _dbContext.Users
                              join r in _dbContext.Roles on u.RoleId equals r.RoleId
                              select new UserViewModel
                              {
                                  RoleId = r.RoleId,
                                  UserId = u.UserId,
                                  CreatedDate = u.CreatedDate,
                                  DateOfBirth = u.DateOfBirth,
                                  Email = u.Email,
                                  FirstName = u.FirstName,
                                  IsActive = u.IsActive,
                                  LastName = u.LastName,
                                  PhoneNumber = u.PhoneNumber,
                                  RoleName = r.RoleName,
                                  UpdatedDate = u.UpdatedDate
                              }).ToListAsync();
            return data;
        }
        public async Task<bool> PerformUser(CreateUserViewModel user)
        {
            var res = false;
            if (user != null)
            {
                var checkdata = await _dbContext.Users.Where(x => x.UserId == user.UserId).FirstOrDefaultAsync();
                var passwordHash = _securePassword.Secure(_config.Security.PasswordSalt, user.PhoneNumber);
                if (checkdata == null)
                {
                    var checkEmail = await _dbContext.Users.AnyAsync(x => x.Email == user.Email);
                    if (checkEmail) return false;
                    var dbObj = new Users
                    {
                        CreatedDate = DateTime.Now,
                        DateOfBirth = user.DateOfBirth,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        IsActive = true,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        password = passwordHash,
                        RoleId = user.RoleId,
                        UpdatedDate = DateTime.Now
                    };
                    await _dbContext.Users.AddAsync(dbObj);
                    res = await _dbContext.SaveChangesAsync() > 0;
                }
                else
                {
                    checkdata.DateOfBirth = DateTime.Now;
                    checkdata.Email = user.Email;
                    checkdata.FirstName = user.FirstName;
                    checkdata.IsActive = user.IsActive;
                    checkdata.LastName = user.LastName;
                    checkdata.PhoneNumber = user.PhoneNumber;
                    checkdata.UpdatedDate = DateTime.Now;
                    checkdata.password = passwordHash;

                    _dbContext.Users.Update(checkdata);
                    res = await _dbContext.SaveChangesAsync() > 0;
                }
            }
            return res;
        }
    }
}
