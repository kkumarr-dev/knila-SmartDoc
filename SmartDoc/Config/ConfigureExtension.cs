using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartDoc.Entities;
using SmartDoc.Helper;
using SmartDoc.Helper.Auth;
using SmartDoc.Repository;
using SmartDoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDoc
{
    public class ConfigureExtension
    {
        public static void AddDatabase(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("SmartDocDb");
            services.AddDbContext<AppDBContext>(c => c.UseSqlServer(connectionString));
            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();

        }
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AppSettingsConfig, AppSettingsConfig>();
            var passwordConfig = new Security();
            configuration.Bind("Security", passwordConfig);
            services.AddSingleton(passwordConfig);
            services.AddTransient<ISecurePassword, SecurePassword>();

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAccountRepo, AccountRepo>();
            services.AddTransient<IAppoinmentService, AppoinmentService>();
            services.AddTransient<IAppoinmentRepo, AppoinmentRepo>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IAdminRepo, AdminRepo>();
        }
    }
}
