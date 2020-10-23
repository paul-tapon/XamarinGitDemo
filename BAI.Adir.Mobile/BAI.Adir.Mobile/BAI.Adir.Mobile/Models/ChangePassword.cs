using System;
using System.Collections.Generic;
using System.Text;

namespace BAI.Adir.Mobile.Models
{
    public class ChangePassword
    {
        public int AppUserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
