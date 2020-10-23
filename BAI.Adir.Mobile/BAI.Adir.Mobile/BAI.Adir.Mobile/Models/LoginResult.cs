using System;
using System.Collections.Generic;
using System.Text;

namespace BAI.Adir.Mobile.Models
{
    public class LoginResult
    {
        public string Token { get; set; }
        public string appuserid { get; set; }

        public string fullname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
