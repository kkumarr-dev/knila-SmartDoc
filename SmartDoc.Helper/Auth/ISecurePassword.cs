using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDoc.Helper.Auth
{
    public interface ISecurePassword
    {
        string Secure(string salt, string password);
        PasswordVerificationResult Verify(string salt, string hashedPassword, string providedPassword);
    }
}
