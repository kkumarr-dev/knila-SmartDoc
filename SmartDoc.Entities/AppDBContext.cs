using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartDoc.Helper;
using SmartDoc.Helper.Auth;
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
        private readonly ISecurePassword _securePassword;
        private readonly AppSettingsConfig _config;
        public AppDBContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor, ISecurePassword securePassword, AppSettingsConfig config)
            : base(options)
        {
            if (httpContextAccessor.HttpContext.User.Claims.Any())
            {
                UserId = Convert.ToInt32(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == "UserId")?.Value);
                RoleId = Convert.ToInt32(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == "RoleId")?.Value);
            }
            _securePassword = securePassword;
            _config = config;
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Appointments> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
            .HasIndex(p => new { p.Email })
            .IsUnique(true);

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

            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    UserId = 1,
                    RoleId = 1,
                    FirstName = "Admin",
                    LastName = "",
                    DateOfBirth = DateTime.Today,
                    Email = "admin@gmail.com",
                    password = _securePassword.Secure(_config.Security.PasswordSalt, "9874563210"),
                    PhoneNumber = "9874563210",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Users
                {
                    UserId = 2,
                    RoleId = 2,
                    FirstName = "Doctor",
                    LastName = "",
                    DateOfBirth = DateTime.Today,
                    Email = "doctor@gmail.com",
                    password = _securePassword.Secure(_config.Security.PasswordSalt, "9874563210"),
                    PhoneNumber = "9874563210",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Users
                {
                    UserId = 3,
                    RoleId = 3,
                    FirstName = "Patient",
                    LastName = "",
                    DateOfBirth = DateTime.Today,
                    Email = "patient@gmail.com",
                    password = _securePassword.Secure(_config.Security.PasswordSalt, "9874563210"),
                    PhoneNumber = "9874563210",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
