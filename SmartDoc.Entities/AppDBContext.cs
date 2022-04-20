using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartDoc.Entities
{
    public class AppDBContext : IdentityDbContext
    {
        public int UserId;
        public int RoleId;
        public AppDBContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) 
            : base(options)
        {
            if (httpContextAccessor.HttpContext.User.Claims.Any())
            {
                UserId = Convert.ToInt32(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == "UserId")?.Value);
                RoleId = Convert.ToInt32(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == "RoleId")?.Value);
            }
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Appointments> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().HasData(new Roles
            {
                RoleId = 1,
                RoleName = "Admin",
                IsActive = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            },
            new Roles
            {
                RoleId = 2,
                RoleName = "Doctor",
                IsActive = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            },
            new Roles
            {
                RoleId = 3,
                RoleName = "Patient",
                IsActive = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            modelBuilder.Entity<Users>().HasData(new Users
            {
                UserId = 1,
                RoleId = 1,
                FirstName = "Admin",
                LastName = "",
                DateOfBirth = DateTime.Now,
                Email = "kumar@gmail.com",
                password = "test",
                PhoneNumber = "8989898989",
                IsActive = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
