using System;
using System.Collections.Generic;
using System.Text;

namespace BAI.Adir.Mobile.Models
{
    public class AppUser
    {
        public int AppUserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? LastLoggedIn { get; set; }
        public int AccessFailedCount { get; set; }
        public string PasswordHash { get; set; }
    }
}
