using SmartDoc.Models;
using SmartDoc.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDoc.Services
{
    public interface IAccountService
    {
        Task<AccountUserViewModel> GetUserData(LoginViewModel user);
    }
}
