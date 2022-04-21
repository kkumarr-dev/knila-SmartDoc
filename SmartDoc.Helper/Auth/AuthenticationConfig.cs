using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SmartDoc.Models.Account;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartDoc.Helper.Auth
{
    public class AuthenticationConfig
    {
        public static void SmartDocAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie();
        }
        public static Task AddClaims(HttpContext httpContext, AccountUserViewModel user)
        {
            var claims = new List<Claim>
                {
                    new Claim("UserId", user.UserId+""),
                    new Claim("RoleId", user.RoleId+""),
                    new Claim("Email", user.Email+""),
                    new Claim("PhoneNumber", user.PhoneNumber+""),
                    new Claim("UserName", user.UserName+""),
                    new Claim(ClaimTypes.Role,user.RoleName)
                };
            
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
            };
            return httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
        }
    }
}
