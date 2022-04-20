using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDoc.Helper
{
    public class AppSettingsConfig
    {
        public AppSettingsConfig(Security security)
        {
            Security = security;
        }
        public Security Security { get; }
    }
    public class Security
    {
        public string PasswordSalt { get; set; }
        public string ResetPasswordKey { get; set; }

    }
}
