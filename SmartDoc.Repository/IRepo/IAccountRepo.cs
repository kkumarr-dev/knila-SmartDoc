using SmartDoc.Models;
using SmartDoc.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDoc.Repository
{
    public interface IAccountRepo
    {
        Task<AccountUserViewModel> GetUserData(LoginViewModel user);
    }
}
