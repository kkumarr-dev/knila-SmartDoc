using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartDoc.Models;
using SmartDoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDoc.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateUser(int roleid)
        {
            var model = new CreateUserViewModel
            {
                RoleId = roleid
            };
            return PartialView(model);
        }
        public async Task<IActionResult> GetUsers(int roleid)
        {
            HttpContext.Session.SetInt32("roleid", roleid);
            var data = await _adminService.GetAllUsers();
            data = data.Where(x => x.RoleId == roleid).ToList();
            return PartialView(data);
        }
        public async Task<IActionResult> AddUser(CreateUserViewModel user)
        {
            var res = await _adminService.PerformUser(user);
            return Ok(res);
        }
        public async Task<IActionResult> GetUserById(int userId)
        {
            var allUsers = await _adminService.GetAllUsers();
            var user = (from u in allUsers
                        where u.UserId == userId
                        select new CreateUserViewModel
                        {
                            UserId = u.UserId,
                            DateOfBirth = u.DateOfBirth,
                            Email = u.Email,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            PhoneNumber = u.PhoneNumber,
                            RoleId = u.RoleId,
                            IsActive = u.IsActive
                        }).FirstOrDefault();
            return PartialView("CreateUser", user);
        }
        public async Task<IActionResult> DeleteUserById(CreateUserViewModel user)
        {
            user.IsActive = false;  
            var res = await _adminService.PerformUser(user);
            return Ok(res);
        }
    }
}
