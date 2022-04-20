using SmartDoc.Models;
using SmartDoc.Models.Account;
using SmartDoc.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDoc.Services
{
    public class AccountService:IAccountService
    {

        private readonly IAccountRepo _accountRepo;
        public AccountService(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }
        public async Task<AccountUserViewModel> GetUserData(LoginViewModel user)
        {
            return await _accountRepo.GetUserData(user);
        }
    }
}
